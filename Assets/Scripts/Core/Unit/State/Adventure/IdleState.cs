using System;
using System.Collections.Generic;
using Core.Guild;
using Core.Manager;
using Core.Unit.FSM;
using UnityEngine;

namespace Core.Unit.State.Adventure
{
    public class IdleState : State<AdventureAI>
    {
        private const string animationName = "idle";
        [SerializeField]
        private Desire desireComponent;

        [SerializeField]
        private List<TaskType> availableTaskTypes;
        public void Awake()
        {
            desireComponent = GetComponent<Desire>();
        }

        public override void Enter(AdventureAI entity)
        {                    
            
            entity.unitAnimation.SetAnimation(animationName);


            if (entity.taskList.Count == 0)
                guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.Entrance);
            else
            {
                switch (entity.taskList[0].taskType)
                {
                    case TaskType.CheckQuest:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.NoticeBoard);
                        break;
                    case TaskType.Reception:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.ReceptionDesk);
                        break;
                    case TaskType.Temple:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.Temple);
                        break;
                    case TaskType.Shop:
                        guildEntity = GetRandomShopEntity();
                        break;
                    case TaskType.SellLoot:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.ReceptionDesk);
                        break;
                    case TaskType.Circus:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.Circus);
                        break;
                    case TaskType.CentralSquare:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.CentralSquare);
                        break;
                    case TaskType.TrainingCenter:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.TrainingCenter);
                        break;
                    case TaskType.Library:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.Library);
                        break;
                }
                
                entity.taskList.RemoveAt(0);
            }
        }

        //shop 일때 Inn, Smithy 둘 중 하나 선택
        private GuildEntity GetRandomShopEntity()
        {
            GuildEntityType[] shopEntities = {GuildEntityType.Inn, GuildEntityType.Smithy, GuildEntityType.PotionMarket};
            GuildEntityType chosenEntity = shopEntities[UnityEngine.Random.Range(0, shopEntities.Length)];
            return GuildManager.Instance.GetGuildEntity(chosenEntity);
        }

        public override void Execute(AdventureAI entity)
        {
            DesireType desireType = desireComponent.GetCurrentNeedDesire();

            if (guildEntity != null)
            {
                guildEntity.ReadyForInteraction(entity.adventure);
            }

            if(entity.taskList.Count == 0 && desireType != DesireType.None)
            {
                FindGuildEntity(entity);
            }
            
            if(desireType == DesireType.None)
            {
                entity.taskList.Clear();
            }
        }

        public void FindGuildEntity(AdventureAI entity)
        {
            TaskType taskType = DetermineTaskBasedOnDesire();
            var newTask = new TaskData
            {
                taskType = taskType
            };

            entity.taskList.Add(newTask);
        }

        private TaskType DetermineTaskBasedOnDesire()
        {
            if (desireComponent == null)
            {
                return TaskType.Shop; 
            }

            DesireType desireType = desireComponent.GetCurrentNeedDesire();

            Debug.Log(desireType);

            switch (desireType)
            {
                case DesireType.Appetite:
                    return TaskType.Shop; 
                case DesireType.SleepDesire:
                    return TaskType.Temple; 
                case DesireType.SafetyNeeds:
                    return TaskType.CentralSquare; 
                case DesireType.ImprovementNeeds:
                    return TaskType.Library;
                default:
                    return TaskType.None; 
            }
        }

        public override void Exit(AdventureAI entity)
        {
            
        }

        public override void OnTransition(AdventureAI entity)
        {
            
        }
    }
}
