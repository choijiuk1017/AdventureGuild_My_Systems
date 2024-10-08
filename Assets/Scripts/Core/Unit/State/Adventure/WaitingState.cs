using Core.Guild;
using Core.Guild.Furniture;
using Core.Manager;
using Core.Unit.FSM;

namespace Core.Unit.State.Adventure
{
    public class WaitingState : State<Core.Unit.Adventure>
    {
        public override void Enter(Core.Unit.Adventure entity)
        {
            var receptionDesk = GuildManager.Instance.GetGuildEntity(GuildEntityType.ReceptionDesk).GetComponent<ReceptionDesk>();
            
            
        }

        public override void Execute(Core.Unit.Adventure entity)
        {
            
        }

        public override void Exit(Core.Unit.Adventure entity)
        {
            
        }

        public override void OnTransition(Core.Unit.Adventure entity)
        {
            
        }
    }
}
