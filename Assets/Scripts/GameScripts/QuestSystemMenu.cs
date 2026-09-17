using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuestSystemMenu : MonoBehaviour
{   
    public GameManager gameManager;
    public CountdownTimer countdownTimer;
    public GameObject TimerMenu;
    public GameObject EditBoard;
    public Slider slider;

    [Header("Quest 1")]
    public string Quest1Name;
    public TextMeshProUGUI QuestDuration1;
    public float QuestTime1inTicks;
    public bool EditingQuest1 = false;

    [Header("Quest 2")]
    public string Quest2Name;
    public TextMeshProUGUI QuestDuration2;
    public float QuestTime2inTicks;
    public bool EditingQuest2 = false;

    [Header("Quest 3")]
    public string Quest3Name;
    public TextMeshProUGUI QuestDuration3;
    public float QuestTime3inTicks;
    public bool EditingQuest3 = false;

    TimeSpan time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EditBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        QuestDuration1.text = TimeSpan.FromSeconds(10f * QuestTime1inTicks).ToString(@"hh\:mm\:ss");
        QuestDuration2.text = TimeSpan.FromSeconds(10f * QuestTime2inTicks).ToString(@"hh\:mm\:ss");
        QuestDuration3.text = TimeSpan.FromSeconds(10f * QuestTime3inTicks).ToString(@"hh\:mm\:ss");
    }

    public void OnEditButton1Press() {
        Debug.Log("EditBoardOpening for Quest1");
        EditingQuest1 = true;
        EditingQuest2 = false;
        EditingQuest3 = false;
        countdownTimer.SetDuration(QuestTime1inTicks);
        slider.value = QuestTime1inTicks;
        EditBoard.SetActive(true);
    }

    public void OnEditButton2Press() {
        Debug.Log("EditBoardOpening for Quest2");
        EditingQuest1 = false;
        EditingQuest2 = true;
        EditingQuest3 = false;
        countdownTimer.SetDuration(QuestTime2inTicks);
        slider.value = QuestTime2inTicks;
        EditBoard.SetActive(true);
    }

    public void OnEditButton3Press() {
        Debug.Log("EditBoardOpening for Quest3");
        EditingQuest1 = false;
        EditingQuest2 = false;
        EditingQuest3 = true;
        countdownTimer.SetDuration(QuestTime3inTicks);
        slider.value = QuestTime3inTicks;
        EditBoard.SetActive(true);
    }

    public void OnStartButton1Press() {
        countdownTimer.SetDuration(QuestTime1inTicks);
        gameManager.OnStartButtonClick();
        Debug.Log("test");
    }

    public void OnStartButton2Press() {
        countdownTimer.SetDuration(QuestTime1inTicks);
        gameManager.OnStartButtonClick();
        Debug.Log("test");
    }

    public void OnStartButton3Press() {
        countdownTimer.SetDuration(QuestTime3inTicks);
        gameManager.OnStartButtonClick();
        Debug.Log("test");
    }

    public void OnCloseButtonPress() {
        Debug.Log("CloseEditBoard");
        EditBoard.SetActive(false);
    }

}
