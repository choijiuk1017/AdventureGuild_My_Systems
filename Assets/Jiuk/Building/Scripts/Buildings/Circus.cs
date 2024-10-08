using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.Circus
{
    public class Circus : Building
    {
        //��Ŀ�� �̿� Ƽ�� ���� ����
        public int maxTickets = 3;
        public int currentTickets = 0;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Init(1);
            DayCycle.OnDayChanged += HandleDayChanged;
        }

        protected override void InitEntity()
        {
            Debug.Log("��Ŀ��");
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Circus, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            Debug.Log("��Ŀ�� ���");
            StartCoroutine(UsingCircus(adventureEntity));
        }

        public override void EndInteraction()
        {
            // ��ƼƼ���� ��ȣ�ۿ� ���� �� ������ ������ �߰�
        }

        //Ƽ���� �߰����ִ� �Լ�
        //��¥�� Ư�� �̺�Ʈ�� ���� �߰����� �� �̿�
        void AddRandomTickets()
        {
            int randomTickets = UnityEngine.Random.Range(1, 4); // 1���� 3 ������ ������ Ƽ�� ��
            currentTickets += randomTickets;
            //Debug.Log(randomTickets + "���� Ƽ���� ��Ŀ���� �߰��Ǿ����ϴ�.");
        }

        // ��Ŀ�� ��� �κ�
        private IEnumerator UsingCircus(Adventure adventure)
        {
            currentTickets--;
            Debug.Log("��Ŀ�� ��� ��");
            var delayTime = buildingData.buildingTime;
            yield return new WaitForSeconds(delayTime);

            PerformActionBasedOnBuildingType(adventure, buildingData.buildingType, buildingData.buildingValue);

            Debug.Log("���谡 ��Ŀ�� ����");

            adventureInside = false;
            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);
        }

        // ���� ������ ���� �̺�Ʈ �Լ�
        private void OnDestroy()
        {
            // ��ũ��Ʈ�� �Ҹ�� �� �ڵ鷯 ����
            DayCycle.OnDayChanged -= HandleDayChanged;
        }

        // ���� ������ ���� �̺�Ʈ �Լ�
        private void HandleDayChanged()
        {
            AddRandomTickets();
        }
    }
}
