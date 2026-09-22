using UnityEngine;

public class SpeedUpTool : MonoBehaviour
{

    private bool IsSpeedUpActive = false;
    //public float SPDMulti;
    public void ActivateSpeedUp()
    {
        //x4 progress speed
        if (!IsSpeedUpActive)
        {
            Time.timeScale = 4f;
            IsSpeedUpActive = true;
        }

        else
        {
            Time.timeScale = 1f;
            IsSpeedUpActive = false;
        }
    }
}
