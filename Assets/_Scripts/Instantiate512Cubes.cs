using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    public GameObject source;
    //Create an array of cubes to model the sound
    GameObject[] sampleCube = new GameObject[512];
    public float _MAXscale;
    //Initiaization
    AudioVisual AVobj;
    void Start()
    {
        for (int i = 0; i < 512; i++)
        {
            GameObject instanceSampleCube = (GameObject)Instantiate(sampleCubePrefab);
            instanceSampleCube.transform.position = this.transform.position;
            instanceSampleCube.transform.parent = this.transform;
            instanceSampleCube.name = "SampleCube" + i;
            this.transform.localPosition = new Vector3((float)-0.70312 * i, 0, 0);
            this.transform.eulerAngles = new Vector3(0,-(float)0.05*i,0);
            
            //(float)-0.70312 * i
            instanceSampleCube.transform.position = Vector3.forward * 100;
            sampleCube[i] = instanceSampleCube;
        }
        this.transform.position = new Vector3(50, 0, 0);
        AVobj = source.GetComponent<AudioVisual>();
    }
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (sampleCube != null)
            {
                //sampleCube[i].transform.localScale = new Vector3(10, normalize(AudioVisual.samples[i]), 100);
                sampleCube[i].transform.localScale = new Vector3(10, normalize(AVobj.samples[i]), 100);
            }
        }
    } 
    float normalize(float sample)
    {
        double normal = sample * _MAXscale + 10;
        if(normal > _MAXscale/11)
        {
            normal = _MAXscale/12;
        }
        return (float)normal;
    }
}
