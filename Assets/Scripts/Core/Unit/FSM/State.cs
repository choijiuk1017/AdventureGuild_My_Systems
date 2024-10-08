using UnityEngine;
using Core.Guild;

namespace Core.Unit.FSM
{
    public abstract class State<T> : MonoBehaviour
    {
        [SerializeField]
        protected GuildEntity guildEntity = null;

        protected float elapsedTime;

        protected virtual void Awake()
        {
               
        }

        public abstract void Enter(T entity);
        public abstract void Execute(T entity);
        public abstract void Exit(T entity);
        public abstract void OnTransition(T entity);
    }
}