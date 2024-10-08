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

        public RectTransform commercialContent;  // ��� ī�װ� Content
        public RectTransform trainingContent;    // �Ʒ� ī�װ� Content
        public RectTransform structureContent;   // ������ ī�װ� Content

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

                // ī�װ����� UI�� �ٸ� content�� �ִ´�.
                if (building.buildingType == 1 || building.buildingType == 2 || building.buildingType == 3)
                {
                    // ��� ī�װ�
                    buildingGo.transform.SetParent(commercialContent);
                }
                else if (building.buildingType >= 20)
                {
                    // �Ʒ� ī�װ�
                    buildingGo.transform.SetParent(trainingContent);
                }
                else if (building.buildingType == 4)
                {
                    // ������ ī�װ�
                    buildingGo.transform.SetParent(structureContent);
                }
            }
        }
    }
}