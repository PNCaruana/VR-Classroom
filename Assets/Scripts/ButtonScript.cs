using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {


    public GameObject player;
    public GameObject cam;
    float v = 10;


	// Use this for initialization
	void Start () {
		
	}

   



    // Update is called once per frame
    void Update () {

        
       




        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            transform.Translate(new Vector3(0, 5, 0));
        }
	}
}
