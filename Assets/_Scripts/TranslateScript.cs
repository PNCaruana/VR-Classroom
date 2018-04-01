using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateScript : MonoBehaviour {

    // Use this for initialization
    public GameObject obj;
    Vector3 velocity = new Vector3(0,0,0);
    Vector3 destination;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        obj.transform.Translate(velocity * Time.deltaTime);
        if(destination == null)
        {
            //...do nothing
        }else if (destination.Equals(obj.transform.position))
        {
            velocity = new Vector3(0, 0, 0);
        }
        else
        {
            moveTo(destination);
        }
	}
    void moveTo(Vector3 mv)
    {
            destination = mv;
            float xDir = mv.x - obj.transform.position.x;
            float yDir = mv.y - obj.transform.position.y;
            float zDir = mv.z - obj.transform.position.z;
            velocity = new Vector3(xDir, yDir, zDir);
        
    }
}
