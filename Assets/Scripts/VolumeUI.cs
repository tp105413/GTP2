using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeUI : MonoBehaviour
{
	public AudioMixer mixer;

	public Slider musicSlider;
	public TMP_Text musicPercentText;

	public Slider sfxSlider;
	public TMP_Text sfxPercentText;

	private void Start()
	{
		float musicValue = PlayerPrefs.GetFloat("MusicVolume", 100f);
		float sfxValue = PlayerPrefs.GetFloat("SFXGroup", 100f);

		musicSlider.value = musicValue;
		sfxSlider.value = sfxValue;

		UpdateMusicVolume(musicValue);
        UpdateSFXGroup(sfxValue);

		musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
		sfxSlider.onValueChanged.AddListener(UpdateSFXGroup);
	}

	private void UpdateMusicVolume(float value)
	{
		musicPercentText.text = value.ToString("0") + "%";

		float normalizedValue = Mathf.Max(value / 100f, 0.0001f);

		mixer.SetFloat(
			"MusicVolume",
			Mathf.Log10(normalizedValue) * 20f
		);

		PlayerPrefs.SetFloat("MusicVolume", value);
		PlayerPrefs.Save();
	}

	private void UpdateSFXGroup(float value)
	{
		sfxPercentText.text = value.ToString("0") + "%";

		float normalizedValue = Mathf.Max(value / 100f, 0.0001f);

		mixer.SetFloat(
            "SFXGroup",
			Mathf.Log10(normalizedValue) * 20f
		);

		PlayerPrefs.SetFloat("SFXGroup", value);
        PlayerPrefs.Save();

    }
}