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
        //������ ���� ���� ����Ʈ
        public List<string> potionOptions = new List<string> {"ü�� ����", "���� ����", "�ż� ����"};

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


        //�ǹ� �̿� �ڷ�ƾ
        protected IEnumerator UsingPotionMarket(Adventure adventure)
        {
            adventureInside = true;


            string chosenPotion = ChooseRandomPotion();
            Debug.Log(adventure.GetComponent<Adventure>().AdventureInfo.AdventureName + "��(��) ������ ����: " + chosenPotion);
            PurchasePotion(chosenPotion);

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseAppetite(50);

            yield return new WaitForSeconds(7f);

            Debug.Log("���谡 ���� ���� ����");

            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);


            adventureInside = false;

        }


        //�������� ������ �����ϴ� �Լ�
        private string ChooseRandomPotion()
        {
            int randomIndex = UnityEngine.Random.Range(0, potionOptions.Count);
            return potionOptions[randomIndex];
        }


        //���谡�� ���� ���� ���� �Լ�(����)
        private void PurchasePotion(string potion)
        {
            Debug.Log("���谡�� " + potion+ "��(��) �����߽��ϴ�.");
        }

    }
}

