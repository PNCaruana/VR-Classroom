using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script for button clicking at start menu
public class ButtonScript : MonoBehaviour {
    public GameObject slider1, slider2, slider3, btn;
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
        Destroy(GameObject.Find("Panel"));
        slider1.active = true;
        slider2.active = true;
        slider3.active = true;
        btn.active = true;
    }



    // Update is called once per frame
    void Update () {

        
       //




        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //transform.Translate(new Vector3(0, 5, 0));
        }
	}
}
