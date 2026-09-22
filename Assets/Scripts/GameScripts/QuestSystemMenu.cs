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
    public TMP_InputField EditQuestName;

    [Header("Quest 1")]
    public string Quest1Name = "Quest 1";
    public TextMeshProUGUI QuestDuration1;
    public float QuestTime1inTicks;
    private int QuestTime1inSeconds;
    public bool EditingQuest1 = false;

    [Header("Quest 2")]
    public string Quest2Name = "Quest 2";
    public TextMeshProUGUI QuestDuration2;
    public float QuestTime2inTicks;
    private int QuestTime2inSeconds;
    public bool EditingQuest2 = false;

    [Header("Quest 3")]
    public string Quest3Name = "Quest 3";
    public TextMeshProUGUI QuestDuration3;
    public float QuestTime3inTicks;
    private int QuestTime3inSeconds;
    public bool EditingQuest3 = false;

    [Header("Quest Name References")]
    public TextMeshProUGUI Quest1NameRef;
    public TextMeshProUGUI Quest2NameRef;
    public TextMeshProUGUI Quest3NameRef;

    TimeSpan time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EditBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        QuestTime1inSeconds = Mathf.RoundToInt(600f * QuestTime1inTicks);
        QuestTime2inSeconds = Mathf.RoundToInt(600f * QuestTime2inTicks);
        QuestTime3inSeconds = Mathf.RoundToInt(600f * QuestTime3inTicks);
        QuestDuration1.text = TimeSpan.FromSeconds(QuestTime1inSeconds).ToString(@"hh\:mm\:ss");
        QuestDuration2.text = TimeSpan.FromSeconds(QuestTime2inSeconds).ToString(@"hh\:mm\:ss");
        QuestDuration3.text = TimeSpan.FromSeconds(QuestTime3inSeconds).ToString(@"hh\:mm\:ss");
        Quest1NameRef.text = Quest1Name;
        Quest2NameRef.text = Quest2Name;
        Quest3NameRef.text = Quest3Name;
    }

    public void OnEditButton1Press() {
        Debug.Log("EditBoardOpening for Quest1");
        EditingQuest1 = true;
        EditingQuest2 = false;
        EditingQuest3 = false;
        countdownTimer.SetDuration(QuestTime1inTicks);
        slider.value = QuestTime1inTicks;
        EditQuestName.text = Quest1Name;
        EditBoard.SetActive(true);
    }

    public void OnEditButton2Press() {
        Debug.Log("EditBoardOpening for Quest2");
        EditingQuest1 = false;
        EditingQuest2 = true;
        EditingQuest3 = false;
        countdownTimer.SetDuration(QuestTime2inTicks);
        slider.value = QuestTime2inTicks;
        EditQuestName.text = Quest2Name;
        EditBoard.SetActive(true);
    }

    public void OnEditButton3Press() {
        Debug.Log("EditBoardOpening for Quest3");
        EditingQuest1 = false;
        EditingQuest2 = false;
        EditingQuest3 = true;
        countdownTimer.SetDuration(QuestTime3inTicks);
        slider.value = QuestTime3inTicks;
        EditQuestName.text = Quest3Name;
        EditBoard.SetActive(true);
    }

    public void OnStartButton1Press() {
        countdownTimer.SetDuration(QuestTime1inTicks);
        gameManager.isQuestRunning = true;
        gameManager.CurrentRunningQuest = 1;
        gameManager.OnStartButtonClick();
    }

    public void OnStartButton2Press() {
        countdownTimer.SetDuration(QuestTime2inTicks);
        gameManager.isQuestRunning = true;
        gameManager.CurrentRunningQuest = 2;
        gameManager.OnStartButtonClick();
    }

    public void OnStartButton3Press() {
        countdownTimer.SetDuration(QuestTime3inTicks);
        gameManager.isQuestRunning = true;
        gameManager.CurrentRunningQuest = 3;
        gameManager.OnStartButtonClick();
    }

    public void OnCloseButtonPress() {
        Debug.Log("CloseEditBoard");
        if (EditingQuest1) {
            UpdateQuestName(1);
        }
        else if (EditingQuest2) {
            UpdateQuestName(2);
        }
        else if (EditingQuest3) {
            UpdateQuestName(3);
        }
        EditBoard.SetActive(false);
    }
    public void UpdateRemainingTimer(int quest) {
        double exactSeconds = countdownTimer.TotalTimeRun.TotalSeconds;
        int TotalTimeRunSecondsRounded = Mathf.RoundToInt((float)exactSeconds);
        if (quest == 1) {
            QuestTime1inTicks = QuestTime1inTicks - (TotalTimeRunSecondsRounded / 600f);
        }
    }
    public void UpdateQuestName(int quest) {
        if (quest == 1) {
            Quest1Name = EditQuestName.text;
        }
        else if (quest == 2) {
            Quest2Name = EditQuestName.text;
        }
        else if (quest == 3) {
            Quest3Name = EditQuestName.text;
        }
    }

}
