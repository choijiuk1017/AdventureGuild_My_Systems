using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.TrainingCenter
{
    public class TrainingCenter : Building
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Init(6);
        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.TrainingCenter, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingTrainingCenter(adventureEntity));
        }

        public override void EndInteraction()
        {

        }

        protected IEnumerator UsingTrainingCenter(Adventure adventure)
        {
            adventureInside = true;


            var delayTime = buildingData.buildingTime;

            yield return new WaitForSeconds(delayTime);

            UpgradeSkill();

            Debug.Log("모험가 훈련소 퇴장");

            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);

            adventureInside = false;

        } 

        private void UpgradeSkill()
        {
            Debug.Log("스킬 강화");
        }
    }
}

