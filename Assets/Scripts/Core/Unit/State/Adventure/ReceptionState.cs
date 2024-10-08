using Core.Guild.Furniture;
using Core.Item;
using Core.Manager;
using Core.Unit.FSM;
using UnityEngine;
using EventType = Core.Manager.EventType;

namespace Core.Unit.State.Adventure
{
    public class ReceptionState : State<AdventureAI>
    {
        private const string animationName = "idle";
        
        public override void Enter(AdventureAI entity)
        {
            if (entity.adventure.AdventureInfo.AdventureRank == AdventureRank.None)
                EventBus.Publish(EventType.Recruit);
            else
                entity.targetObject.GetComponent<ReceptionDesk>().PurchaseDropItem(entity.GetComponent<Inventory>().GetAllItem());
            
            entity.unitAnimation.SetAnimation(animationName);
        }

        public override void Execute(AdventureAI entity)
        {
            
        }

        public override void Exit(AdventureAI entity)
        {
            //randomPos or next Behavior
        }

        public override void OnTransition(AdventureAI entity)
        {
            
        }
    }
}
