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

        //������ ���� ����Ʈ
        public List<string> foodOptions = new List<string> { "�佺Ʈ", "������ũ", "������", "����", "�Ľ�Ÿ" };


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


        //�ǹ� ��� �ڷ�ƾ
        protected IEnumerator UsingInn(Adventure adventure)
        {
            adventureInside = true;

            string chosenFood = ChooseRandomFood();
            Debug.Log(adventure.GetComponent<Adventure>().AdventureInfo.AdventureName + "��(��) ������ ����: " + chosenFood);
            EatFood(chosenFood);

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseAppetite(50);

            yield return new WaitForSeconds(7f);

            Debug.Log("���谡 ���� ����");

            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);

            adventureInside = false;

        }

        //������ ������ �������� ���� �Լ�
        private string ChooseRandomFood()
        {
            int randomIndex = UnityEngine.Random.Range(0, foodOptions.Count);
            return foodOptions[randomIndex];
        }

        //���谡���� ȿ���� �شٴ��� �� ��ȭ ���� �Լ�(����)
        private void EatFood(string food)
        {
            Debug.Log("���谡�� " + food + "��(��) �Խ��ϴ�.");
        }


        //��Ƽ ��Ī ���� �Լ�(����)
        private void MatchingParty()
        {

        }

    }
}


