using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

//todo
/*
 * 건물 Rotation 관련 메서드 추가할 것
 */

//모든 건물의 부모가 되는 스크립트
//현재 무슨 오류인지 한글로 쓴 로그들이 모두 깨짐
namespace Core.Building
{
    public class Building : GuildEntity
    {
        //카메라
        protected Camera mainCamera;

        //욕구 시스템
        //모험가에게 욕구 충족을 위해 필요
        protected Desire desire;

        //건물 데이터 
        public BuildingManager.BuildingData buildingData;

        //건물 내부에 모험가가 있는지 없는지 확인할 변수
        [SerializeField]
        protected bool adventureInside = false;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        protected override void InitEntity()
        {

        }

        public override void OnInteraction(Adventure adventureEntity)
        {

        }

        public override void EndInteraction()
        {

        }

        //건물 회전 함수
        public void RotationBuilding()
        {
            transform.Rotate(new Vector3(0, 0, 90));
        }


        //건물의 규격을 확인하고 알려주는 함수
        public Vector2 GetBuildingSize()
        {
            Renderer renderer = GetComponent<Renderer>();
            Vector3 size = renderer.bounds.size;
            return Math.Abs((int)transform.rotation.z) == 90 ? new Vector2(size.y, size.x) : new Vector2(size.x, size.y);
        }

        protected void Init(int buildingID)
        {
            BuildingManager buildingManager = GameObject.FindObjectOfType<BuildingManager>();
            if (buildingManager != null)
            {
                buildingData = buildingManager.GetBuildingData(buildingID);
                //Debug.Log("���� ����" + buildingData.buildingName);
            }
            else
            {
                //Debug.LogError("BuildingManager�� ���� �����ϴ�.");
            }
        }

        //세금 징수 함수(예정)
        public void CollectTax(int tax)
        {
            //Debug.Log("���� ¡��: " + tax);
        }

        //월급 지급 함수(예정)
        public void PaymentSalary(int salary)
        {
            //Debug.Log("���� ����: " + salary);
        }

        //건물의 타입에 따라 기능을 하는 함수
        //플레이어의 의지력, 체력 등을 관리
        public void PerformActionBasedOnBuildingType(Adventure adventure, int buildingType, int buildingValue)
        {
            switch (buildingType)
            {
                case 1:

                    RestoreWillingness(adventure, buildingValue);
                    break;
                case 2:

                    RestoreHPAndMP(adventure, buildingValue);
                    break;

                default:
                    break;
            }
        }


        //의지력 회복 함수
        private void RestoreWillingness(Adventure adventure, int value)
        {
            //Debug.Log("������ ȸ���մϴ�: " + value);
        }

        //체력과 마나 회복 함수
        private void RestoreHPAndMP(Adventure adventure, int value)
        {
            adventure.AdventureInfo.AdventureStat.curHp += value;
            adventure.AdventureInfo.AdventureStat.curMp += value;
            //Debug.Log("ü�°� ������ ȸ���մϴ�: " + value);
        }


        /*
        public void SetLayerRecursively(GameObject obj, int newLayer)
        {
            if (obj == null)
                return;

            obj.layer = newLayer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
        */
    }
}

