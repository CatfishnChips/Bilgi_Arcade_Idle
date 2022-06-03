using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

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

    }
}

