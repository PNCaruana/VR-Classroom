using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableGraph : MonoBehaviour {
    public Tools.Complex[,] matrix = null;
    public GameObject barPrefab;
    public float heightScale;
    public bool is_init = false;

    private GameObject[,] bars = null;
    private Vector3 cornerPos;
	// Use this for initialization
	void Start () {
        cornerPos = new Vector3(transform.position.x - transform.localScale.x,
            1f,
            transform.position.z - transform.localScale.z);
	}
	
    void InitGraph()
    {
        if (matrix != null)
        {
            cornerPos = new Vector3(transform.position.x - transform.localScale.x * (1f/2 - 1f / (float)matrix.GetLength(0)/2),
            transform.position.y + transform.localScale.y + 2f,
            transform.position.z - transform.localScale.z * (1f/2 - 1f / (float)matrix.GetLength(1)/2));
            bars = new GameObject[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Vector3 pos = new Vector3(cornerPos.x + i * transform.localScale.x / matrix.GetLength(0),
                        cornerPos.y + System.Math.Abs(((float)matrix[i, j].real) * heightScale/2),
                        cornerPos.z + j * transform.localScale.x / matrix.GetLength(1));
                    Quaternion rotation = new Quaternion(0, 0, 0, 0);

                    GameObject clone = Instantiate(barPrefab, pos, rotation);

                    bars[i, j] = clone;

                    clone.transform.localScale = new Vector3(transform.localScale.x/matrix.GetLength(0), 
                        System.Math.Abs((float)matrix[i, j].real * heightScale), 
                        transform.localScale.z/matrix.GetLength(1));
                }
            }
        }
    }

    void ClearGraph()
    {
        if (bars == null) return;
        for (int i = 0; i < bars.GetLength(0); i++)
        {
            for (int j = 0; j < bars.GetLength(1); j++)
            {
                Object.Destroy(bars[i, j]);
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if (is_init == false && matrix != null)
        {
            is_init = true;
            ClearGraph();
            InitGraph();
        }
	}
}
