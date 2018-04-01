using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class ManageTables : MonoBehaviour {
    public float a;
    public string[] filesPaths;

    private int imgSel = 0;
    private Complex[,] origMatrix;
    private GameObject origTable;

    private Complex[,] fftMatrix;
    private GameObject fftTable;

    // Use this for initialization
    private void Start()
    {
        Reset();
    }

    void Reset () {
        origMatrix = Complex.FileToMatrix(filesPaths[imgSel]);
        UpdateForward();
    }

    void UpdateForward()
    {
        UpdateTable("OrigTable", origMatrix);
        UpdateBoard("OrigTable", origMatrix);
        fftMatrix = FFT.FFT2D(origMatrix, origMatrix.GetLength(0), origMatrix.GetLength(1), 1);
        UpdateTable("FourierTable", fftMatrix);
        UpdateBoard("FourierTable", fftMatrix);
    }

    void UpdateBackward()
    {
        UpdateTable("FourierTable", fftMatrix);
        UpdateBoard("FourierTable", fftMatrix);
        origMatrix = FFT.FFT2D(fftMatrix, origMatrix.GetLength(0), origMatrix.GetLength(1), -1);
        UpdateTable("OrigTable", origMatrix);
        UpdateBoard("OrigTable", origMatrix);
    }
	
    void UpdateTable(string tableName, Complex[,] tableMat)
    {
        GameObject.Find(tableName).GetComponent<TableGraph>().matrix = tableMat;
        GameObject.Find(tableName).GetComponent<TableGraph>().is_init = false;
    }

    void UpdateBoard(string tableName, Complex[,] tableMat)
    {
        GameObject.Find(tableName).GetComponent<BoardGraph>().matrix = tableMat;
        GameObject.Find(tableName).GetComponent<BoardGraph>().is_init = false;
    }

    // Update is called once per frame
    void Update () {
        /*//Shuffle the table
        if (Input.GetKeyDown("r"))
        {
            Debug.Log("GetKeyDown r");
            System.Random rnd = new System.Random();
            origMatrix = Tools.Complex.Shuffle<Complex>(rnd, origMatrix);
            UpdateTable("OrigTable", origMatrix);
            fftMatrix = FFT.FFT2D(origMatrix, origMatrix.GetLength(0), origMatrix.GetLength(1), 1);
            UpdateTable("FourierTable", fftMatrix);
        }*/
        //Mutate the image/add noise
        if (Input.GetKeyDown("m"))
        {
            Debug.Log("Mutate");
            // This doesn't actual permute. It mutates (adds noise)
            Tools.Complex.Permute(origMatrix);
            UpdateForward();
        }
        //Sharpen the image
        if (Input.GetKeyDown("s"))
        {
            // compute edge sharpened fft
            Debug.Log("Sharpen");
            for (int i = 0; i < fftMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < fftMatrix.GetLength(1); j++)
                {
                    fftMatrix[i, j] = fftMatrix[i, j] *
                        (1 + a * System.Math.Pow((2 * System.Math.PI * i / fftMatrix.GetLength(0)), 2)
                           + a * System.Math.Pow((2 * System.Math.PI * j / fftMatrix.GetLength(1)), 2))/(1+a);
                }
            }
            UpdateBackward();
        }
        //Reset the image
        if (Input.GetKeyDown("z"))
        {
            Debug.Log("Reset");
            Reset();
        }
        //Change images
        if (Input.GetKeyDown("c"))
        {
            Debug.Log("Change img");
            imgSel = (imgSel+1)%filesPaths.Length;
            Reset();
        }

	}
}
