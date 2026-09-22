using UnityEngine;

public class SpeedUpTool : MonoBehaviour
{

    public float SPDUP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = SPDUP;
    }
}
