using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Manager;
using Core.Building;

namespace Core.UI
{
    public class BuildingUI : MonoBehaviour
    {
        private int buildingID;
        public string buildingName;
        public Text nameText;
        public Text goldText;
        public ConstructBuilding constructBuilding;

        private void Start()
        {
            constructBuilding = FindObjectOfType<ConstructBuilding>();
        }

        public void SetUpUI(BuildingManager.BuildingData buildingData)
        {
            buildingName = buildingData.buildingName;
            nameText.text = buildingData.buildingKoreanName;
            goldText.text = buildingData.buildingGold.ToString();
        }

        public void ConstructButtonClick()
        {
            if (constructBuilding != null)
            {
                Debug.LogWarning(buildingName);
                constructBuilding.SetBuildingPrefab(buildingName);
                Debug.Log(buildingName);
            }
            else
            {
                Debug.LogWarning("BuildingManager is not assigned.");
            }
        }
    }
}