using Core.Manager;
using Core.Unit;
using UnityEngine;

namespace Core.Guild
{
    public class GuildEntrance : GuildEntity
    {
        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Entrance, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            //adventureEntity.AdventureAI.ChangeState(AdventureStateType.Reception);
            GuildManager.Instance.adventurerManager.ExitAdventurer(adventureEntity);
            adventureEntity.AdventureAI.ChangeState(AdventureStateType.Exit);
        }

        public override void EndInteraction()
        {
            
        }
    }
}
