using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniGame : MonoBehaviour
{
    [SerializeField] private Button butterflyPrefab;
    [SerializeField] private RectTransform spawnArea1;
    [SerializeField] private RectTransform spawnArea2;
    [SerializeField] private RectTransform flyArea;
    [SerializeField] private float flyingSpeed = 500f;


    private bool isButterflyAlive = false;
    private bool isMiniGameRunning = false;

    public static MiniGame Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    // Start the mini game and start spawning butterfly
    public void StartMiniGame()
    {
        // Prevent start mini game twice
        if (isMiniGameRunning) return;

        isMiniGameRunning = true;
        StartCoroutine(SpawnLoop());
    }

    // Stop the mini game
    public void StopMiniGame()
    {
        isMiniGameRunning = false;
    }

    private void SpawnButterfly()
    {
        isButterflyAlive = true;

        bool spawnLeftArea = Random.Range(0, 2) == 0;

        RectTransform spawnArea;

        // Spawn butterfly from left or right side
        if (spawnLeftArea)
        {
            spawnArea = spawnArea1;
        }
        else
        {
            spawnArea = spawnArea2;
        }

        Button butterfly = Instantiate(butterflyPrefab, flyArea);
        RectTransform butterflyPosition = butterfly.GetComponent<RectTransform>();

        // Get animator from prefab itself
        Animator butterflyAnim = butterfly.GetComponentInChildren<Animator>();

        // Random assign butterfly color
        Image butterflyImage = butterfly.GetComponent<Image>();
        if( butterflyImage != null)
        {
            butterflyImage.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.8f, 1f);
        }

        // Randomly spawn within the spawn area
        float randomX = Random.Range(spawnArea.rect.xMin, spawnArea.rect.xMax);
        float randomY = Random.Range(spawnArea.rect.yMin, spawnArea.rect.yMax);

        butterflyPosition.anchoredPosition = spawnArea.anchoredPosition + new Vector2(randomX, randomY);

        StartCoroutine(ButterflyMovement(butterflyPosition, spawnLeftArea, butterflyAnim));

        butterfly.onClick.AddListener(() => { ButterflyClicked(butterfly); });
    }

    // Butterfly moves from spawn area to opposite side with random movement
    IEnumerator ButterflyMovement(RectTransform butterflyRect, bool startedLeft, Animator anim)
    {
        RectTransform targetArea;

        if (startedLeft)
        {
            targetArea = spawnArea2;
            anim.Play("ButterflyLeft");
        }
        else
        {
            targetArea = spawnArea1;
            anim.Play("ButterflyRight");
        }

        float totalFlyArea = 0f;
        float changeDirectionTimer = 0f;

        while(butterflyRect != null && Vector2.Distance(butterflyRect.anchoredPosition, targetArea.anchoredPosition) > 1f)
        {
            changeDirectionTimer -= Time.deltaTime;

            if (changeDirectionTimer <= 0f)
            {
                totalFlyArea = Random.Range(-300f, 300f);
                changeDirectionTimer = Random.Range(1f, 5f);
            }

            Vector2 targetPosition = targetArea.anchoredPosition;

            targetPosition.y += totalFlyArea;

            Vector2 currentPosition = butterflyRect.anchoredPosition;

            float speedX = Mathf.MoveTowards(currentPosition.x, targetArea.anchoredPosition.x, flyingSpeed * Time.deltaTime);
            float speedY = Mathf.MoveTowards(currentPosition.y, targetPosition.y, (flyingSpeed * 1.5f) * Time.deltaTime);

            butterflyRect.anchoredPosition = new Vector2(speedX, speedY);

            yield return null;
        }

        if (butterflyRect != null)
        {
            isButterflyAlive = false;
            Destroy(butterflyRect.gameObject);

            yield return new WaitForSeconds(0.1f);
        }
    }

    // Check if butterfly needs to be spawned
    IEnumerator SpawnLoop()
    {
        while (isMiniGameRunning)
        {
            // Random assign delay time spawning butterfly 5 - 30 secs
            float delay = Random.Range(5f, 30f);
            yield return new WaitForSeconds(delay);

            // If mini game is stopped while waiting
            if (!isMiniGameRunning) yield break;

            SpawnButterfly();

            // Wait until the butterfly is clicked or destroyed
            yield return new WaitUntil(() => !isButterflyAlive);
        }
    }

    // Called when butterfly is clicked
    private void ButterflyClicked(Button butterfly)
    {
        isButterflyAlive = false;
        Destroy(butterfly.gameObject);
        int ButterflyClick = PlayerPrefs.GetInt("Butterfly Click", 0);
        PlayerPrefs.SetInt("Butterfly Click", ButterflyClick + 1);
    }
}
