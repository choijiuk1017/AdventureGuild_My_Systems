using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.CentralSquare
{
    public class CentralSquare : Building
    {

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Init(8);
        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.CentralSquare, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingCentralSquare(adventureEntity));
        }

        public override void EndInteraction()
        {

        }

        //건물 사용 코루틴
        protected IEnumerator UsingCentralSquare(Adventure adventure)
        {
            adventureInside = true;

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseSafetyNeeds(50);

            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 중앙 광장 퇴장");

            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);


            adventureInside = false;

        }

        private void MatchingParty()
        {

        }

    }
}

