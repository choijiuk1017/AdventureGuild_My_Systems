using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Manager;
using Core.UI;

namespace Core.UI
{
    public class ConstructUI : MonoBehaviour
    {
        private List<BuildingManager.BuildingData> buildingList;

        public GameObject panel;

        public RectTransform commercialContent;  // 상업 카테고리 Content
        public RectTransform trainingContent;    // 훈련 카테고리 Content
        public RectTransform structureContent;   // 구조물 카테고리 Content

        public GameObject buildingUIPrefab;

        // Start is called before the first frame update
        void Start()
        {
            BuildingManager buildingManager = GameObject.FindObjectOfType<BuildingManager>();

            if (buildingManager != null)
            {
                buildingList = buildingManager.GetBuildingList();
            }

            SpawnUI();
        }

        void SpawnUI()
        {
            foreach (BuildingManager.BuildingData building in buildingList)
            {
                GameObject buildingGo = Instantiate(buildingUIPrefab);

                BuildingUI buildingUI = buildingGo.GetComponent<BuildingUI>();

                buildingUI.SetUpUI(building);

                // 카테고리별로 UI를 다른 content에 넣는다.
                if (building.buildingType == 1 || building.buildingType == 2 || building.buildingType == 3)
                {
                    // 상업 카테고리
                    buildingGo.transform.SetParent(commercialContent);
                }
                else if (building.buildingType >= 20)
                {
                    // 훈련 카테고리
                    buildingGo.transform.SetParent(trainingContent);
                }
                else if (building.buildingType == 4)
                {
                    // 구조물 카테고리
                    buildingGo.transform.SetParent(structureContent);
                }
            }
        }
    }
}