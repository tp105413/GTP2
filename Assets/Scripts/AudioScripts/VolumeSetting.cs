using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSetting : MonoBehaviour
{

    [SerializeField] private AudioMixer audiomixer;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private TMP_Text musicPercentText;

    [SerializeField] private TMP_Text sfxPercentText;


    private void Awake()
    {

        BGMSlider.onValueChanged.AddListener(delegate { SetBGMVolume(); });
        SFXSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });

        if (PlayerPrefs.HasKey("BGM"))
        {
            LoadVolume();
        }
        else
        {
            SetBGMVolume();
            SetSFXVolume();
        }
    }
    public void SetBGMVolume()
    {
        float volume = BGMSlider.value;
        audiomixer.SetFloat("BGM", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("BGM", volume);

        if (musicPercentText != null)
        {
            musicPercentText.text = Mathf.RoundToInt(volume * 100f).ToString() + "%";
        }
    }
    public void SetSFXVolume()
    {


        float volume = SFXSlider.value;
        audiomixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", volume);

        if (sfxPercentText != null)
        {
            sfxPercentText.text = Mathf.RoundToInt(volume * 100f).ToString() + "%";
        }
    }
    public void LoadVolume()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGM");
        SFXSlider.value = PlayerPrefs.GetFloat("SFX");

        SetBGMVolume();
        SetSFXVolume();
    }
}