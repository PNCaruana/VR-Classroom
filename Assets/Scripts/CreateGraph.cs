using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class CreateGraph : MonoBehaviour {
    public GameObject barPrefab;
    public GameObject fftBarPrefab;
    public GameObject iftBarPrefab;
    public Vector3 startPos;
    public Vector3 ftStartPos;
    public Vector3 iftStartPos;

    // Create a test matrix
    private Complex[,] testMatrix = new Complex[,] { { 1f, 2f, 3f, 4f }, { 0.5f, 0.5f, 0.5f, 1f }, { 4f, 5f, 6f, 9f }, { 1f, 2f, 3f, 3f } };
    private int n;
    private int m;

    private Complex[,] ftTestMatrix;
    private Complex[,] iftTestMatrix;

    private GameObject[,] cloneArr;
    private GameObject[,] ftCloneArr;
    private GameObject[,] iftCloneArr;

    // Use this for initialization
    void Start() {
        n = testMatrix.GetLength(0);
        m = testMatrix.GetLength(1);


        cloneArr = new GameObject[n, m];
        ftCloneArr = new GameObject[n, m];
        iftCloneArr = new GameObject[n, m];
        ftTestMatrix = FFT.FFT2D(testMatrix, n, m, 1);
        iftTestMatrix = FFT.FFT2D(ftTestMatrix, n, m, -1);

        //Display the test matrix and its FFT
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Vector3 pos = new Vector3(startPos.x+i, startPos.y+((float)testMatrix[i,j].real)/2, startPos.z+j);
                Vector3 ftPos = new Vector3(ftStartPos.x + i, 
                    ftStartPos.y + (System.Math.Abs((float)ftTestMatrix[i, j].real)) / 2, 
                    ftStartPos.z + j);
                Vector3 iftPos = new Vector3(iftStartPos.x + i, iftStartPos.y + ((float)iftTestMatrix[i, j].real) / 2, iftStartPos.z + j);
                Quaternion rotation = new Quaternion(0, 0, 0, 0);

                GameObject clone = Instantiate(barPrefab, pos, rotation);
                GameObject ftClone = Instantiate(fftBarPrefab, ftPos, rotation);
                GameObject iftClone = Instantiate(iftBarPrefab, iftPos, rotation);

                cloneArr[i, j] = clone;
                ftCloneArr[i, j] = ftClone;
                iftCloneArr[i, j] = iftClone;

                clone.transform.localScale = new Vector3(1f, (float)testMatrix[i,j].real, 1f);
                ftClone.transform.localScale = new Vector3(1f, System.Math.Abs((float)ftTestMatrix[i, j].real), 1f);
                iftClone.transform.localScale = new Vector3(1f, (float)iftTestMatrix[i, j].real, 1f);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        // u for update
        if (Input.GetKeyDown("u"))
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Vector3 pos = new Vector3(startPos.x + i, startPos.y + ((float)testMatrix[i, j].real) / 2, startPos.z + j);
                    Vector3 ftPos = new Vector3(ftStartPos.x + i,
                        ftStartPos.y + (System.Math.Abs((float)ftTestMatrix[i, j].real)) / 2,
                        ftStartPos.z + j);
                    Vector3 iftPos = new Vector3(iftStartPos.x + i, iftStartPos.y + ((float)iftTestMatrix[i, j].real) / 2, iftStartPos.z + j);

                    cloneArr[i, j].transform.position = pos;
                    ftCloneArr[i, j].transform.position = ftPos;
                    iftCloneArr[i, j].transform.position = iftPos;

                    cloneArr[i,j].transform.localScale = new Vector3(1f, (float)testMatrix[i, j].real, 1f);
                    ftCloneArr[i,j].transform.localScale = new Vector3(1f, System.Math.Abs((float)ftTestMatrix[i, j].real), 1f);
                    iftCloneArr[i,j].transform.localScale = new Vector3(1f, (float)iftTestMatrix[i, j].real, 1f);
                }
            }
        }
        // s for edge sharpening
        if (Input.GetKeyDown("s"))
        {
            // compute edge sharpened fft
            float a = 0.01f;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    ftTestMatrix[i, j] = ftTestMatrix[i, j] * 
                        (1 + a * System.Math.Pow((2 * System.Math.PI * i / n),2) 
                           + a * System.Math.Pow((2 * System.Math.PI * j / m),2));
                }
            }
        }

        iftTestMatrix = FFT.FFT2D(ftTestMatrix, n, m, -1);

        //re-calculate positions and scales
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Vector3 pos = new Vector3(startPos.x + i, startPos.y + ((float)testMatrix[i, j].real) / 2, startPos.z + j);
                Vector3 ftPos = new Vector3(ftStartPos.x + i,
                    ftStartPos.y + (System.Math.Abs((float)ftTestMatrix[i, j].real)) / 2,
                    ftStartPos.z + j);
                Vector3 iftPos = new Vector3(iftStartPos.x + i, iftStartPos.y + ((float)iftTestMatrix[i, j].real) / 2, iftStartPos.z + j);

                cloneArr[i, j].transform.position = pos;
                ftCloneArr[i, j].transform.position = ftPos;
                iftCloneArr[i, j].transform.position = iftPos;

                cloneArr[i, j].transform.localScale = new Vector3(1f, (float)testMatrix[i, j].real, 1f);
                ftCloneArr[i, j].transform.localScale = new Vector3(1f, System.Math.Abs((float)ftTestMatrix[i, j].real), 1f);
                iftCloneArr[i, j].transform.localScale = new Vector3(1f, (float)iftTestMatrix[i, j].real, 1f);
            }
        }
    }
}
