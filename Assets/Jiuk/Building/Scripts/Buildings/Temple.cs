using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.Temple
{
    public class Temple : Building
    {

        //신전 내 모험가 관련 변수
        public int currentAdventure = 0;
        public int maxAdventure = 3;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Init(2);
        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Temple, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            if (currentAdventure <= maxAdventure)
            {
                currentAdventure++;
                StartCoroutine(UsingTemple(adventureEntity));
            }
        }

        public override void EndInteraction()
        {

        }


        //신전 사용 부분
        private IEnumerator UsingTemple(Adventure adventure)
        {

            adventureInside = true;


            var delayTime = buildingData.buildingTime ;

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseSleepDesire(50);

            yield return new WaitForSeconds(delayTime);

            PerformActionBasedOnBuildingType(adventure, buildingData.buildingType, buildingData.buildingValue);

            Debug.Log("모험가 신전 퇴장");

            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);

            currentAdventure--;

            adventureInside = false;
        }

    }

}

