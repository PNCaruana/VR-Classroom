using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioVisual : MonoBehaviour
{
    public Slider slider;
    public Button btn;
    AudioSource audioSource;
    public static int count = 0;
    public float[] samples = new float[512];
    private bool isPause;
    //code for Unity
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Pause();
        isPause = true;
        btn.onClick.AddListener(Pause);
        
    }
    void Update()
    {
        btn.onClick.AddListener(Pause);
        getSpectrumAudioSource();
        float freq = slider.value;
        audioSource.pitch = 1 + 2 * freq;
        
    }
    void getSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

public void Pause()
    {
        if (isPause)
        {
            audioSource.UnPause();
            isPause = false;
        }
        else
        {
            audioSource.Pause();
            isPause = true;
        }
          
    }
}
