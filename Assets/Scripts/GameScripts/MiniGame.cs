using System.Collections;
using UnityEngine;
using TMPro;

public class MiniGame : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip beatSound;

    public float beatTime = 0.25f;
    public TMP_Text correctCountText;
    public GameObject beatPrefab;
    public RectTransform targetCircle;
    public Canvas canvas;
    public float moveSpeed;
    public float hitRange;
    public float missDistance;

    int[] rhythm = new int[4];
    int correctCount = 0;

    void Start()
    {
        StartCoroutine(PlayRhythm());
    }

    void Update()
    {
        // Find all Beat objects
        GameObject[] beats = GameObject.FindGameObjectsWithTag("Beat");

        foreach(GameObject beat in beats)
        {
            RectTransform beatTransform = beat.GetComponent<RectTransform>();
            beatTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

            if(beatTransform.anchoredPosition.x<targetCircle.anchoredPosition.x - missDistance)
            {
                Destroy(beat);
                MissBeat();
            }
        }
    }

    // Generate random types of 4 beats
    public void GenerateRhythm()
    {
        for(int i = 0; i < 4; i++)
        {
            rhythm[i] = Random.Range(0, 2);
        }
    }

    IEnumerator PlayRhythm()
    {
        while (true)
        {
            GenerateRhythm();

            // Play beats rhythm audio
            for (int i = 0; i < 4; i++)
            {
                if (rhythm[i] == 1)
                {
                    audioSource.PlayOneShot(beatSound);
                }

                yield return new WaitForSeconds(beatTime);
            }

            // After the beats rhythm audio finish, spawn the beat visual
            for (int j = 0; j < 4; j++)
            {
                if (rhythm[j] == 1)
                {
                    SpawnBeat(j);
                }
            }

            // Wait for 3 seconds and spawn next round of beats
            yield return new WaitForSeconds(3f);
        }
    }

    // Spawn a beat at a position based on its index
    public void SpawnBeat(int index)
    {
        GameObject beat = Instantiate(beatPrefab, canvas.transform);
        RectTransform beatTransform = beat.GetComponent<RectTransform>();

        float spacing = moveSpeed * beatTime;
        float SpawnX = 1000 + (index * spacing);

        beatTransform.anchoredPosition = new Vector2(SpawnX, 0);
    }

    public void ClickBeat()
    {
        correctCount++;
        correctCountText.text = "x" + correctCount;
    }

    public void MissBeat()
    {
        correctCount--;
        if (correctCount <= 0) correctCount = 0;
        correctCountText.text = "x" + correctCount;
    }

    // Check if clicked a beat within the hit range
    public void PlayerClick()
    {
        GameObject[] beats = GameObject.FindGameObjectsWithTag("Beat");

        foreach(GameObject beat in beats)
        {
            RectTransform beatTransform = beat.GetComponent<RectTransform>();

            float distance = Mathf.Abs(beatTransform.anchoredPosition.x - targetCircle.anchoredPosition.x);

            if (distance <= hitRange)
            {
                ClickBeat();
                Destroy(beat);
                return;
            }
        }

        // If click without beat will miss also
        MissBeat();
    }
}
