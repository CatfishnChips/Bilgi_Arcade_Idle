using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    #region Self Variable

    #region Serialized Variables

    [SerializeField] private GameObject StartPanel, ControlPanel, panel3, panel4;

    #endregion

    #endregion


    #region Event Subscription

    private void Start()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        EventManager.Instance.onPlay -= OnOpenControlPanel;
        EventManager.Instance.onPlay -= OnCloseStartPanel;
    }

    private void SubscribeEvents()
    {
        EventManager.Instance.onPlay += OnOpenControlPanel;
        EventManager.Instance.onPlay += OnCloseStartPanel;
    }

    private void OnCloseStartPanel()
    {
        StartPanel.SetActive(false);
    }

    #endregion

    private void OnOpenControlPanel()
    {
        ControlPanel.SetActive(true);
    }

    public void Play()
    {
        EventManager.Instance.onPlay?.Invoke();
    }
}
