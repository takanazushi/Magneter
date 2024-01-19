using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusiMixer : MonoBehaviour
{
    [SerializeField,Header("�I�[�f�B�I�~�L�T�[")]
    AudioMixer audioMixer;

    [SerializeField,Header("BGM�𒲐�����X���C�_�[")]
    private Slider BGMSlider;

    [SerializeField,Header("SE�𒲐�����X���C�_�[")]
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
        //�󂯎�����{�����[�����Z�b�g
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSE(float volume)
    {
        //�󂯎�����{�����[�����Z�b�g
        audioMixer.SetFloat("SE", volume);
    }
}
