using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.Keys;

namespace Scripts.Assets.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton Pattern

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            Application.targetFrameRate = 60;
        }

        #endregion

        #region Self Variables

        #region Public Variables

        public GameStates state;

        #endregion

        #region Private Variables

        #endregion

        #endregion

          private void Start()
        {
            EventManager.Instance.onSaveGameData += OnSaveGameData;
            EventManager.Instance.onUpdateGameState += OnUpdateGameState;
        }

        private void OnDisable()
        {
            EventManager.Instance.onSaveGameData -= OnSaveGameData;
            EventManager.Instance.onUpdateGameState -= OnUpdateGameState;
        }

        private void OnUpdateGameState(GameStates newState)
        {
            state = newState;
        }

        private void OnSaveGameData(GameSaveDataParams saveData)
        {
            //ES3.Save("Level", saveData.Level);
            //ES3.Save("Coin", saveData.Coin);
            //ES3.Save("Haptic", saveData.Haptic);
            //ES3.Save("SFX", saveData.SFX);
        }

    }
}

