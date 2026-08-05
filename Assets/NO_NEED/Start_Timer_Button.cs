using Unity.VisualScripting;
using UnityEngine;

public class Start_Timer_Button : MonoBehaviour
{
    public GameObject Idle;
    public GameObject Attack;

    public void Switch_Attack()
    {
        Debug.Log("Button Clicked");
        Idle.SetActive(true);
        Attack.SetActive(false);
    }

}