using System.Collections.Generic;
using Core.Guild;
using Core.Item;
using UnityEngine;

namespace Core.Manager
{
    public class GuildManager : Singleton<GuildManager>
    {
        private Guild.Guild guild;

        [SerializeField] private Inventory guildInventory;

        [SerializeField]
        private RecruitAdventure recruitAdventure;

        public AdventurerManager adventurerManager;

        [SerializeField]
        private Dictionary<GuildEntityType, GuildEntity> guildEntities;
        
        public Inventory GuildInventory { get { return guildInventory; } }

        private void Awake()
        {
            guild = FindObjectOfType<Guild.Guild>();
            recruitAdventure = guild.GetComponent<RecruitAdventure>();
            guildEntities = new Dictionary<GuildEntityType, GuildEntity>();
            adventurerManager = GetComponent<AdventurerManager>();
        }
        
        public Guild.Guild GetGuild()
        {
            return guild;
        }

        public RecruitAdventure GetRecruitAdventure()
        {
            return recruitAdventure;
        }

        public void AddGuildEntity(GuildEntityType guildEntityType, GuildEntity guildEntity)
        {
            // 중복시 처리 필요
            guildEntities.Add(guildEntityType, guildEntity);
            Debug.Log(guildEntity + "추가됨");
        }

        public GuildEntity GetGuildEntity(GuildEntityType guildEntityType)
        {
            return guildEntities[guildEntityType];
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                ShowLog();
            }
            else if(Input.GetKeyDown(KeyCode.N))
                NewDay();
        }

        public void ShowLog()
        {
            foreach(var entity in guildEntities.Values)
            {
                Debug.Log(entity.gameObject.name);
            }    
        }

        public void NewDay()
        {
            var visitingAdventurerCount = Random.Range(0, 2);

            for (int i = 0; i < visitingAdventurerCount; i++)
            {
                adventurerManager.EnterAdventurer();
            }
        }
    }
}
