using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source------")]
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------Audio Clip------")]
    public AudioClip BGM;
    public AudioClip HitSound; 
    public AudioClip ButtonClick;
    public AudioClip Upgrade;
    //public AudioClip BGM;

    public void Start()
    {
        BGMSource.clip = BGM;
        BGMSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
