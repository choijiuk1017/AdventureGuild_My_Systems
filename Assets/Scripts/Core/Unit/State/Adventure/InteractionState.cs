using Core.Guild;
using Core.Unit.FSM;
using UnityEngine;

namespace Core.Unit.State.Adventure
{
    public class InteractionState : State<AdventureAI>
    {
        public override void Enter(AdventureAI entity)
        {
            if (entity.targetObject.TryGetComponent(out GuildEntity guildEntity))
            {
                Debug.Log("상호작용" + entity.targetObject.name);
                guildEntity.OnInteraction(entity.adventure);
            }
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
    }
}
