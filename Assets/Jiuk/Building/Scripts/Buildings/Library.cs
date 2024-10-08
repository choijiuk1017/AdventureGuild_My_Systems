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


        //�ǹ� ��� �ڷ�ƾ
        protected IEnumerator UsingLibrary(Adventure adventure)
        {
            adventureInside = true;

            var delayTime = buildingData.buildingTime;

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseImprovementNeeds(50);

            UpgradeSkill();

            yield return new WaitForSeconds(delayTime);
           
            Debug.Log("���谡 ������ ����");


            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);

            adventureInside = false;

        }

        //���谡�� ��ų�� ���׷��̵� ���ִ� �Լ�(����)
        private void UpgradeSkill()
        {
            Debug.Log("��ų ��ȭ");
        }
    }

}


