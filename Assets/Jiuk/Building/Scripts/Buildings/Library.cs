using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.Library
{
    public class Library : Building
    {      
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Init(7);
        }
        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Library, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingLibrary(adventureEntity));
        }

        public override void EndInteraction()
        {

        }


        //건물 사용 코루틴
        protected IEnumerator UsingLibrary(Adventure adventure)
        {
            adventureInside = true;

            var delayTime = buildingData.buildingTime;

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseImprovementNeeds(50);

            UpgradeSkill();

            yield return new WaitForSeconds(delayTime);
           
            Debug.Log("모험가 도서관 퇴장");


            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);

            adventureInside = false;

        }

        //모험가의 스킬을 업그레이드 해주는 함수(예정)
        private void UpgradeSkill()
        {
            Debug.Log("스킬 강화");
        }
    }

}


