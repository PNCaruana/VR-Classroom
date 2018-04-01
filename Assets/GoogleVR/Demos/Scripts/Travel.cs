using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : MonoBehaviour {

    public float distance = 5;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       
        


        

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameObject.Find("Canvas"))
        {
            transform.position = transform.position + Camera.main.transform.forward * distance * Time.deltaTime;

        }
    }
}
