using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script for button clicking at start menu
public class ButtonScript : MonoBehaviour {




    


	// Use this for initialization
	void Start () {
		
	}

    public void Visual()
    {
        DeleteCanvas();
        //Start Visual fourier

    }

    public void Audio()
    {
        DeleteCanvas();
        //Start Audio fourier

    }

    public void DeleteCanvas()
    {
        Destroy(GameObject.Find("Canvas"));
    }



    // Update is called once per frame
    void Update () {

        
       //




        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            transform.Translate(new Vector3(0, 5, 0));
        }
	}
}
