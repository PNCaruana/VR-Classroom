using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 10, 10), "hello");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
