using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioVisual : MonoBehaviour
{
    public Slider slider;
    public Button btn;
    public AudioSource audioSource;
    public static int count = 0;
    public float[] samples = new float[512];
    //code for Unity
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Pause();

    }
    void Update()
    {
        getSpectrumAudioSource();
        float freq = slider.value;
        audioSource.pitch = 1 + 2 * freq;

    }
    void getSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }
}
