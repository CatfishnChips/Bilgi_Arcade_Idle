using Assets.Scripts.Enums;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    #region Self Variables


    #region Serialized Variables


    [SerializeField] private GameObject startPanel, winPanel, failPanel, joystickPanel, economyPanel, wavePanel;
    [SerializeField] private TextMeshProUGUI woodText, stoneText, goldText, timerText, waveText;
    [SerializeField] private GameObject timerObject;

    #endregion

    #endregion

    #region Event Subscription
    private void Start()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventManager.Instance.onPlay += OnOpenJoystickPanel;
        EventManager.Instance.onPlay += OnCloseStartPanel;
        EventManager.Instance.onUpdateUICollectableType += OnUpdateCollectableType;
        EventManager.Instance.onWaveEnd += OnWaveEnd;
        EventManager.Instance.onUpdateTimer += OnUpdateTimer;
        EventManager.Instance.onWaveStart += OnWaveStart;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.onPlay -= OnOpenJoystickPanel;
        EventManager.Instance.onPlay -= OnCloseStartPanel;
        EventManager.Instance.onUpdateUICollectableType -= OnUpdateCollectableType;
        EventManager.Instance.onWaveEnd -= OnWaveEnd;
        EventManager.Instance.onUpdateTimer -= OnUpdateTimer;
        EventManager.Instance.onWaveStart -= OnWaveStart;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    private void OnOpenJoystickPanel()
    {
        joystickPanel.SetActive(true);
    }

    private void OnCloseStartPanel()
    {
        startPanel.SetActive(false);
        economyPanel.SetActive(true);
        wavePanel.SetActive(true);
    }

    public void Play()
    {
        EventManager.Instance.onPlay?.Invoke();    
    }

    public void Skip() {
        EventManager.Instance.onSkip?.Invoke();
    }

    public void OnUpdateCollectableType(CollectableTypes type, int value)
    {
        switch (type)
        {
            case CollectableTypes.Wood:
                {
                    woodText.text = value.ToString();
                    break;
                }
            case CollectableTypes.Stone:
                {
                    stoneText.text = value.ToString();
                    break;
                }
            case CollectableTypes.Gold:
                {
                    goldText.text = value.ToString();
                    break;
                }
        }
    }

    private void OnWaveEnd() {
        waveText.enabled = false;
        timerObject.SetActive(true);
    }

    private void OnUpdateTimer(int timer) {
        timerText.text = timer.ToString();
    }

    private void OnWaveStart(int wave) {
        waveText.enabled = true;
        timerObject.SetActive(false);
        waveText.text = "Wave " + wave;
    }
}
