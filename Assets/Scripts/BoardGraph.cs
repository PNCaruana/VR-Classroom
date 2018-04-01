using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGraph : MonoBehaviour {
    public Tools.Complex[,] matrix = null;
    public GameObject barPrefab;
    public float scale;
    public float colorScale;
    public bool is_init = false;

    private GameObject[,] bars = null;
    private Vector3 cornerPos;
    // Use this for initialization
    void Start()
    {
        cornerPos = new Vector3(transform.position.x - transform.localScale.x,
            transform.position.y + transform.localScale.y,
            transform.position.z - 10.25f);
    }

    void InitGraph()
    {
        if (matrix != null)
        {
            cornerPos = new Vector3(transform.position.x - transform.localScale.x * (1f / 2 - 1f / (float)matrix.GetLength(0) / 2),
            transform.position.y + 25.5f + transform.localScale.y * (1f / 2 - 1f / (float)matrix.GetLength(1) / 2),
            transform.position.z - 10.25f);
            bars = new GameObject[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Vector3 pos = new Vector3(cornerPos.x + i * transform.localScale.x / matrix.GetLength(0),
                        cornerPos.y + j * transform.localScale.y / matrix.GetLength(1)*10,
                        cornerPos.z - 0.25f);
                    Quaternion rotation = new Quaternion(0, 0, 0, 0);

                    GameObject clone = Instantiate(barPrefab, pos, rotation);

                    bars[i, j] = clone;

                    clone.transform.localScale = new Vector3(transform.localScale.x / matrix.GetLength(0),
                        transform.localScale.y / matrix.GetLength(1)*10,
                        0.1f);
                    float mCol = System.Math.Min(System.Math.Abs((float)matrix[i, j].real)*colorScale, 1);
                    // So we can properly see the average weight
                    if (i == 0 && j == 0) mCol = System.Math.Min(System.Math.Abs((float)matrix[i, j].real)/256, 1);
                    //Debug.Log("mCol[" + i + "," + j + "] = " + mCol);
                    clone.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(mCol, mCol, mCol));
                    /*// Get the current value of the material properties in the renderer.
                    Renderer _renderer = GetComponent<Renderer>();
                    MaterialPropertyBlock _propBlock = new MaterialPropertyBlock();
                    _renderer.GetPropertyBlock(_propBlock);
                    // Assign our new value.
                    
                    _propBlock.SetColor("_Color", new Color(mCol,mCol,mCol));
                    // Apply the edited values to the renderer.
                    _renderer.SetPropertyBlock(_propBlock);*/

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
    void Update()
    {
        if (is_init == false && matrix != null)
        {
            is_init = true;
            ClearGraph();
            InitGraph();
        }
    }
}
