using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public struct Complex
    {
        public double real, imag;
        public Complex(double x, double y)
        {
            real = x;
            imag = y;
        }
        public static implicit operator Complex(float x)
        {
            return new Complex(x, 0);
        }
        public static implicit operator Complex(double x)
        {
            return new Complex(x, 0);
        }
        public static Complex operator *(Complex c, double d)
        {
            return new Complex(c.real * d, c.imag * d);
        }
        public static Complex operator /(Complex c, double d)
        {
            return new Complex(c.real / d, c.imag / d);
        }
        private static float[] SaToFa(string[] sa)
        {
            float[] fa = new float[sa.Length];
            for (int i = 0; i < sa.Length; i++)
            {
                fa[i] = float.Parse(sa[i], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            }
            return fa;
        }
        // File MUST have an n*m matrix (not jagged)
        public static Complex[,] FileToMatrix(string filePath)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(filePath);
            Debug.Log("Trying to read the file");
            var lines = new List<float[]>();
            int Rows = 0;
            int Columns = -1;
            while (!sr.EndOfStream)
            {
                float[] Line = SaToFa(sr.ReadLine().Split(','));
                if (Columns == -1 || Columns == Line.Length)
                {
                    Columns = Line.Length;
                }
                else
                {
                    return null;
                }
                lines.Add(Line);
                Rows++;
            }

            var data = lines.ToArray();
            var dataFa = new Complex[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    dataFa[i, j] = new Complex(data[i][j],0f);
                }
            }
            return dataFa;
        }
        public float Magnitude()
        {
            return ((float)System.Math.Sqrt(real * real + imag * imag));
        }
        public float Phase()
        {
            return ((float)System.Math.Atan(imag / real));
        }
        private static Complex Mutate(Complex c)
        {
            float randNum = Random.value;
            float lowBound = c.real < 10d ? 1f : 0.2f;
            float highBound = c.real > 180d ? .3f : .8f;
            if (randNum < lowBound)
            {
                return c.real + Random.value * 10;
            } else if(highBound < randNum)
            {
                return c.real - Random.value * 10;
            } else
            {
                return c.real + Random.value * 10 * (Random.value > 0.5 ? -1 : 1);
            }
        }
        // Not actually permuting. I'm just tired
        public static void Permute(Complex[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = Mutate(array[i, j]);
                }
            }
        }
        public static T[,] Shuffle<T>(System.Random random, T[,] array)
        {
            int lengthRow = array.GetLength(1);

            for (int i = array.GetLength(0) - 1; i >= 0; i--)
            {
                Debug.Log("Shufflin'...");
                int i0 = i / lengthRow;
                int i1 = i % lengthRow;

                int j = random.Next(i, i + 1);
                int j0 = j / lengthRow;
                int j1 = j % lengthRow;

                T temp = array[i0, i1];
                array[i0, i1] = array[j0, j1];
                array[j0, j1] = temp;
            }
            return array;
        }
    }

    public class FFT
    {
        // Courtesy of https://www.codeproject.com/articles/44166/2d-fft-of-an-image-in-c
        // and http://ieeexplore.ieee.org/stamp/stamp.jsp?arnumber=7884163
        public static Complex[,] FFT2D(Complex[,] c, int nx, int ny, int dir)
        {
            int i, j;
            int m;//Power of 2 for current number of points
            double[] real;
            double[] imag;
            Complex[,] output = new Complex [nx,ny];
            real = new double[nx];
            imag = new double[nx];

            // copy c
            for (i = 0; i < nx; i++)
            {
                for (j = 0; j < ny; j++)
                {
                    output[i, j].real = c[i,j].real;
                    output[i, j].imag = c[i, j].imag;
                }
            }

            for (j = 0; j < ny; j++)
            {
                for (i = 0; i < nx; i++)
                {
                    real[i] = c[i, j].real;
                    imag[i] = c[i, j].imag;
                }
                // Calling 1D FFT Function for Rows
                m = (int)System.Math.Log((double)nx, 2);//Finding power of 2 for current number of points e.g. for nx=512 m=9
                FFT1D(dir, m, ref real, ref imag);

                for (i = 0; i < nx; i++)
                {
                    //  c[i,j].real = real[i];
                    //  c[i,j].imag = imag[i];
                    output[i, j].real = real[i];
                    output[i, j].imag = imag[i];
                }
            }
            // Transform the columns  
            real = new double[ny];
            imag = new double[ny];

            for (i = 0; i < nx; i++)
            {
                for (j = 0; j < ny; j++)
                {
                    //real[j] = c[i,j].real;
                    //imag[j] = c[i,j].imag;
                    real[j] = output[i, j].real;
                    imag[j] = output[i, j].imag;
                }
                // Calling 1D FFT Function for Columns
                m = (int)System.Math.Log((double)ny, 2);//Finding power of 2 for current number of points e.g. for nx=512 m=9
                FFT1D(dir, m, ref real, ref imag);
                for (j = 0; j < ny; j++)
                {
                    //c[i,j].real = real[j];
                    //c[i,j].imag = imag[j];
                    output[i, j].real = real[j];
                    output[i, j].imag = imag[j];
                }
            }

            // return(true);
            return (output);
        }

        private static void FFT1D(int dir, int m, ref double[] x, ref double[] y)
        {
            long nn, i, i1, j, k, i2, l, l1, l2;
            double c1, c2, tx, ty, t1, t2, u1, u2, z;
            /* Calculate the number of points */
            nn = 1;
            for (i = 0; i < m; i++)
                nn *= 2;
            /* Do the bit reversal */
            i2 = nn >> 1;
            j = 0;
            for (i = 0; i < nn - 1; i++)
            {
                if (i < j)
                {
                    tx = x[i];
                    ty = y[i];
                    x[i] = x[j];
                    y[i] = y[j];
                    x[j] = tx;
                    y[j] = ty;
                }
                k = i2;
                while (k <= j)
                {
                    j -= k;
                    k >>= 1;
                }
                j += k;
            }
            /* Compute the FFT */
            c1 = -1.0;
            c2 = 0.0;
            l2 = 1;
            for (l = 0; l < m; l++)
            {
                l1 = l2;
                l2 <<= 1;
                u1 = 1.0;
                u2 = 0.0;
                for (j = 0; j < l1; j++)
                {
                    for (i = j; i < nn; i += l2)
                    {
                        i1 = i + l1;
                        t1 = u1 * x[i1] - u2 * y[i1];
                        t2 = u1 * y[i1] + u2 * x[i1];
                        x[i1] = x[i] - t1;
                        y[i1] = y[i] - t2;
                        x[i] += t1;
                        y[i] += t2;
                    }
                    z = u1 * c1 - u2 * c2;
                    u2 = u1 * c2 + u2 * c1;
                    u1 = z;
                }
                c2 = System.Math.Sqrt((1.0 - c1) / 2.0);
                if (dir == 1)
                    c2 = -c2;
                c1 = System.Math.Sqrt((1.0 + c1) / 2.0);
            }
            /* Scaling for forward transform */
            if (dir == 1)
            {
                for (i = 0; i < nn; i++)
                {
                    x[i] /= (double)nn;
                    y[i] /= (double)nn;

                }
            }

            return;
        }
    };
}