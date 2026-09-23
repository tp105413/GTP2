using System;
using UnityEngine;

public class EachMenuForBTN : MonoBehaviour
{
    public GameObject[] PopMenu;

    public int MenuNumber;
    private bool isSettingOpen = false;
    public GameObject Setting;

    public bool isGuideBookOpen = false;
    public GameObject GuideBookBTN;
    AudioManager audiomanager;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Close all menus and open the selected menu
    public void OpenMenu()
    {
        for (int i = 0; i < PopMenu.Length; i++)
        {
            if (PopMenu[i] != null)
            {
                PopMenu[i].SetActive(false);

            }
        }
        if (MenuNumber >= 0 && MenuNumber < PopMenu.Length)
        {
            if (PopMenu[MenuNumber] != null)
            {
                PopMenu[MenuNumber].SetActive(true);
            }
        }
    }

    public void SettingButton()
    {
        if (Setting != null)
        {
            isSettingOpen = Setting.activeSelf;
        }

        { 
            isSettingOpen = !isSettingOpen;
            if (Setting != null)
            {
                Setting.SetActive(isSettingOpen);
            }
        }
    }

    public void GuideBook()
    {
        if (GuideBookBTN != null)
        {
            isGuideBookOpen = GuideBookBTN.activeSelf;
        }

        {
            isGuideBookOpen = !isGuideBookOpen;
            if (GuideBookBTN != null)
            {
                GuideBookBTN.SetActive(isGuideBookOpen);
            }
        }
    }

    public void OnMyButtonClick()
    {
        audiomanager.PlaySFX(audiomanager.ButtonClick);
    }

    public void OnClickUpgradeButton()
    {
        audiomanager.PlaySFX(audiomanager.Upgrade);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
