using System;
using System.Collections;
using System.Collections.Generic;
using Core.Unit.FSM;
using Core.Guild;
using Core.Unit.State.Adventure;
using UnityEngine;


namespace Core.Unit
{
    public enum AdventureStateType
    {
        Init, Enter, Exit, Idle, Move, Reception, Interaction, Last
    }

    public enum AdventurePlan
    {
        Home, Dungeon, Raid, Rest
    }

    public class AdventureAI : MonoBehaviour
    {
        private AdventureFSM<AdventureAI> fsm;
        private State<AdventureAI>[] states;
        [SerializeField] private AdventureStateType prevState;
        [SerializeField] private AdventureStateType curState;

        public Adventure adventure;

        public UnitAnimation unitAnimation;

        public GameObject targetObject;

        public List<TaskData> taskList;

        public AdventurePlan adventurePlan;
        
        // Start is called before the first frame update
        private void Start()
        {
            unitAnimation = GetComponent<UnitAnimation>();
            adventure = GetComponent<Adventure>();

            fsm = new AdventureFSM<AdventureAI>();
            states = new State<AdventureAI>[((int)AdventureStateType.Last)];

            states[((int)AdventureStateType.Init)] = GetComponent<InitState>();
            states[((int)AdventureStateType.Enter)] = GetComponent<EnterState>();
            states[((int)AdventureStateType.Exit)] = GetComponent<ExitState>();
            states[((int)AdventureStateType.Idle)] = GetComponent<IdleState>();
            states[((int)AdventureStateType.Move)] = GetComponent<MoveState>();
            states[((int)AdventureStateType.Reception)] = GetComponent<ReceptionState>();
            states[((int)AdventureStateType.Interaction)] = GetComponent<InteractionState>();

            fsm.Init(this, states[(int)AdventureStateType.Enter]);
        }

        private void Update()
        {
            fsm.StateUpdate();
        }

        public void ChangeState(AdventureStateType newState)
        {
            if (newState != AdventureStateType.Move)
                prevState = newState;

            curState = newState;
            fsm.ChangeState(states[(int)newState]);
        }

        public void ChangeStateWithDesire()
        {
            DesireType desireType = adventure.Desire.GetCurrentNeedDesire();
            TaskType taskType = TaskType.None;
            
            switch (desireType)
            {
                case DesireType.Appetite:
                    taskType = TaskType.Shop;
                    break;
                case DesireType.SleepDesire:
                    taskType = TaskType.Temple;
                    break;
                case DesireType.SafetyNeeds:
                    taskType = TaskType.CentralSquare;
                    break;
                case DesireType.ImprovementNeeds:
                    taskType = TaskType.Library;
                    break;
                default:
                    taskType = TaskType.None;
                    break;
            }
            
            var newTask = new TaskData
            {
                taskType = taskType
            };

            taskList.Add(newTask);
        }
    }

    public enum TaskType
    {
        CheckQuest, Reception, Temple, SellLoot, Shop, Circus, CentralSquare, Library, TrainingCenter, None
    }

    [Serializable]
    public class TaskData
    {
        public TaskType taskType;
    }
}