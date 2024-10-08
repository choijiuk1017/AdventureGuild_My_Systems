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
        //������ ��� ���� ����Ʈ
        public List<string> equipmentOptions = new List<string> {"����", "ö ��", "ö ����"};

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

        //��� ���� �Լ�(����)
        private void CraftEquipment()
        {
            //������ ���� Ȯ�� �ʿ�
            Debug.Log("��� ����");
        }

        //��� ��ȭ �Լ�(����)
        private void EnhanceEquipment()
        {
            //������ ���� Ȯ�� �ʿ�
            Debug.Log("��� ��ȭ");
        }
    
        //�ǹ� �̿� �ڷ�ƾ
        protected IEnumerator UsingSmithy(Adventure adventure)
        {
            adventureInside = true;


            CraftEquipment();
            EnhanceEquipment();

            desire = adventure.GetComponent<Desire>();

            desire.IncreaseAppetite(50);

            yield return new WaitForSeconds(7f);

            Debug.Log("���谡 ���尣 ����");

            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);

            adventureInside = false;
   
        }

    }

}

