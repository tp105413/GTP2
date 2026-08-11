using UnityEngine;

public class EachMenuForBTN : MonoBehaviour
{
    public GameObject [] PopMenu;

    public int MenuNumber;

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
}