using Unity.VisualScripting;
using UnityEngine;

public class Stop_Timer_Button : MonoBehaviour
{
    public GameObject Idle;
    public GameObject Attack;

    public void Switch_Idle()
    {
        Debug.Log("Button Clicked");
        Idle.SetActive(false);
        Attack.SetActive(true);
    }

}