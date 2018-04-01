using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPause : MonoBehaviour {

    // Use this for initialization
    public GameObject source1, source2, source3;
    AudioVisual av1, av2, av3;
    static bool isPress = false;


    public UnityEngine.UI.Text textObj;
	void Start () {
        av1 = source1.GetComponent<AudioVisual>();
        av2 = source2.GetComponent<AudioVisual>();
        av3 = source3.GetComponent<AudioVisual>();
        textObj.text = "Play";
           
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Press()
    {
        if (!isPress)
        {
            av1.audioSource.UnPause();
            av2.audioSource.UnPause();
            av3.audioSource.UnPause();
            textObj.text = "Pause";
            isPress = true;
        }
        else
        {
            av1.audioSource.Pause();
            av2.audioSource.Pause();
            av3.audioSource.Pause();
            textObj.text = "Play";
            isPress = false;
        }
    }
}
