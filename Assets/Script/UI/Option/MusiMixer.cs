using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusiMixer : MonoBehaviour
{
    [SerializeField,Header("オーディオミキサー")]
    AudioMixer audioMixer;

    [SerializeField,Header("BGMを調整するスライダー")]
    private Slider BGMSlider;

    [SerializeField,Header("SEを調整するスライダー")]
    private Slider SESlider;

    private void Start()
    {
        //BGM
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = bgmVolume;
        //SE
        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = seVolume;
    }

    public void SetBGM(float volume)
    {
        //受け取ったボリュームをセット
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSE(float volume)
    {
        //受け取ったボリュームをセット
        audioMixer.SetFloat("SE", volume);
    }
}
