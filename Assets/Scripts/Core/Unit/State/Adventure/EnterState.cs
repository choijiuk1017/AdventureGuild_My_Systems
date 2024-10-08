using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Core.Guild;
using Core.Guild.Furniture;
using Core.Manager;
using Core.Unit.FSM;
using Debug = UnityEngine.Debug;


namespace Core.Unit.State.Adventure
{
    public class EnterState : State<AdventureAI>
    {
        [SerializeField]
        private List<TaskType> availableTaskTypes;

        public override void Enter(AdventureAI entity)
        {
            if (entity.adventure.IsQuestActive())
            {
                if (entity.adventure.GetStat().hp > entity.adventure.GetStat().curHp)
                {
                    // TaskData에 추가 의료소
                    Debug.Log("의료소 방문");
                    NewTask(entity, TaskType.Temple);
                }
                
                //NewTask(entity, TaskType.SellLoot);
            }

            GuildManager.Instance.GetGuildEntity(GuildEntityType.ReceptionDesk).GetComponent<ReceptionDesk>().AddWaitingList(entity.adventure);
            NewTask(entity, TaskType.CheckQuest);

            //entity.ChangeState(AdventureStateType.Idle);
        }

        private TaskType ChooseRandomTask(AdventureAI entity)
        {
            if (availableTaskTypes.Count == 0)
            {
                InitAvailableTaskType();
            }
             
            int randomIndex = UnityEngine.Random.Range(0, availableTaskTypes.Count);

            TaskType randomTaskType = availableTaskTypes[randomIndex];

            availableTaskTypes.RemoveAt(randomIndex);
            ;
            var newTask = new TaskData
            {
                taskType = randomTaskType
            };

            return randomTaskType;
        }

        public override void Execute(AdventureAI entity)
        {
            
        }

        public override void Exit(AdventureAI entity)
        {
            
        }

        public override void OnTransition(AdventureAI entity)
        {
            
        }

        private void InitAvailableTaskType()
        {
            availableTaskTypes = new List<TaskType>
            {
                TaskType.Shop,
                TaskType.Circus,
                TaskType.CentralSquare,
                TaskType.Library,
                TaskType.TrainingCenter
            };
        }


        public void NewTask(AdventureAI entity, TaskType newTaskType)
        {
            var newTask = new TaskData
            {
                taskType = newTaskType
            };

            entity.taskList.Add(newTask);
        }
    }
}
