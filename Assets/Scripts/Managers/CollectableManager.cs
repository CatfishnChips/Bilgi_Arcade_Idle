using Assets.Scripts.Enums;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [HideInInspector]
        public CollectableTypes Type
        {
            get => type;

            set
            {
                type = value;
                for (int i = 0; i < meshList.Count; i++)
                {
                    meshList[i].SetActive(false);
                }
                meshList[(int)value].SetActive(true);

            }
        }
        #region Serialized Variables

        [SerializeField] private CollectableTypes type;

        [SerializeField] private List<GameObject> meshList;

        #endregion

        #endregion

        #endregion

        private void Start()
        {
            ActivateFirstMesh();
        }

        private void ActivateFirstMesh()
        {
            Type = type;
        }
    }
}
