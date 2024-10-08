using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Item;
using Core.Guild;
using Core.Manager;


namespace Core.Building.Smithy
{

    public class Smithy : Building
    {
        //임의의 장비 관련 리스트
        public List<string> equipmentOptions = new List<string> {"갑옷", "철 검", "철 방패"};

        public Inventory inventory;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Init(3);

        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Smithy, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingSmithy(adventureEntity));
        }

        public override void EndInteraction()
        {

        }

        //장비 제작 함수(예정)
        private void CraftEquipment()
        {
            //아이템 조건 확인 필요
            Debug.Log("장비 제작");
        }

        //장비 강화 함수(예정)
        private void EnhanceEquipment()
        {
            //아이템 조건 확인 필요
            Debug.Log("장비 강화");
        }
    
        //건물 이용 코루틴
        protected IEnumerator UsingSmithy(Adventure adventure)
        {
            adventureInside = true;


            CraftEquipment();
            EnhanceEquipment();

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseAppetite(50);

            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 대장간 퇴장");

            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);

            adventureInside = false;
   
        }

    }

}

