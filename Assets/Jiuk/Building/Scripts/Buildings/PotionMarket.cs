using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.PotionMarket
{
    public class PotionMarket : Building
    {
        //임의의 포션 관련 리스트
        public List<string> potionOptions = new List<string> {"체력 포션", "마나 포션", "신속 포션"};

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Init(5);
        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.PotionMarket, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingPotionMarket(adventureEntity));
        }

        public override void EndInteraction()
        {

        }


        //건물 이용 코루틴
        protected IEnumerator UsingPotionMarket(Adventure adventure)
        {
            adventureInside = true;


            string chosenPotion = ChooseRandomPotion();
            Debug.Log(adventure.GetComponent<Adventure>().AdventureInfo.AdventureName + "이(가) 선택한 음식: " + chosenPotion);
            PurchasePotion(chosenPotion);

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseAppetite(50);

            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 포션 상점 퇴장");

            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);


            adventureInside = false;

        }


        //랜덤으로 포션을 선택하는 함수
        private string ChooseRandomPotion()
        {
            int randomIndex = UnityEngine.Random.Range(0, potionOptions.Count);
            return potionOptions[randomIndex];
        }


        //모험가의 포션 구매 관련 함수(예정)
        private void PurchasePotion(string potion)
        {
            Debug.Log("모험가가 " + potion+ "를(을) 구입했습니다.");
        }

    }
}

