using System;
using System.Collections;
using System.Collections.Generic;
using Core.Battle;
using Core.Guild;
using Core.Manager;
using Core.Unit.Body;
using UnityEngine;

namespace Core.Unit
{
    public class Adventure : Unit
    {
        [SerializeField] private AdventureInfo adventureInfo;
        private AdventureAI adventureAI;
        private BattleStats battleStats;
        private Desire desire;
        [SerializeField] private bool isQuestActive;

        public AdventureAI AdventureAI => adventureAI;
        public AdventureInfo AdventureInfo => adventureInfo;
        public BattleStats BattleStats => battleStats;
        public Desire Desire => desire;

        [SerializeField] QuestManager.QuestData acceptedQuest;
        [SerializeField] private Relationship relationship;

        protected override void Init()
        {
            base.Init();
            
            adventureInfo = RecruitAdventure.CreateAdventureInfo();
            adventureAI = GetComponent<AdventureAI>();
            battleStats = new BattleStats();
            desire = GetComponent<Desire>();

            //FindObjectOfType<ReceptionDesk>().AddWaitingList(this);
            
            //acceptedQuest = new List<QuestManager.QuestData>();
            SetStat(adventureInfo.AdventureStat);
        }

        public void SingUpAdventurer()
        {
            adventureInfo.RankUp();
        }

        public void AcceptQuest(QuestManager.QuestData newQuest)
        {
            acceptedQuest = newQuest;
        }

        public QuestManager.QuestData GetQuest()
        {
            return acceptedQuest;
        }

        public void SetQuestActive(bool isQuestActive)
        {
            this.isQuestActive = isQuestActive;
        }

        public void SetAdventurerSkin(Sprite skin)
        {
            GetComponent<SpriteRenderer>().sprite = skin;
        }

        public bool IsQuestActive()
        {
            return isQuestActive;
        }
    }
}
