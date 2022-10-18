using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumenSonido : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void ControlMusica(float sliderMusica)
    {
        audioMixer.SetFloat("VolumenMusica", Mathf.Log10(sliderMusica) * 20);
    }
}
