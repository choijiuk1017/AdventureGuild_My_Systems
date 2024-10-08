using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.Inn
{
    public class Inn : Building
    {

        //임의의 음식 리스트
        public List<string> foodOptions = new List<string> { "토스트", "스테이크", "샐러드", "수프", "파스타" };


        protected override void Start()
        {
            base.Start();
            Init(5);
        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Inn, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingInn(adventureEntity));
        }

        public override void EndInteraction()
        {

        }


        //건물 사용 코루틴
        protected IEnumerator UsingInn(Adventure adventure)
        {
            adventureInside = true;

            string chosenFood = ChooseRandomFood();
            Debug.Log(adventure.GetComponent<Adventure>().AdventureInfo.AdventureName + "이(가) 선택한 음식: " + chosenFood);
            EatFood(chosenFood);

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseAppetite(50);

            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 주점 퇴장");

            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);

            adventureInside = false;

        }

        //임의의 음식을 랜덤으로 고르는 함수
        private string ChooseRandomFood()
        {
            int randomIndex = UnityEngine.Random.Range(0, foodOptions.Count);
            return foodOptions[randomIndex];
        }

        //모험가에게 효과를 준다던지 등 변화 관리 함수(예정)
        private void EatFood(string food)
        {
            Debug.Log("모험가가 " + food + "를(을) 먹습니다.");
        }


        //파티 매칭 관련 함수(예정)
        private void MatchingParty()
        {

        }

    }
}


