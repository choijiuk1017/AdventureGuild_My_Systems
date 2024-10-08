using System;
using System.Collections.Generic;
using Core.Guild.Quest;
using Core.UI.Quest;
using UnityEngine;
using Utility.Data;

namespace Core.Manager
{
    public enum QuestRank
    {
        S = 1, A, B, C, D, E, F
    }

    public enum QuestType
    {
        a = 1, b, c
    }

    public class QuestManager : Singleton<QuestManager>
    {
        private Dictionary<int, QuestData> questData;
        [SerializeField]
        private List<QuestData> questList;

        [SerializeField] private QuestListUI questListUI;

        private const string questTableName = "Quest_Master";

        private const string questID = "Quest_ID";
        private const string questName = "Quest_Name";
        private const string questRank = "Quest_Rank";
        private const string questType = "Quest_Type";
        private const string questText = "Quest_Text";
        private const string questTargetType = "Target_Type";
        private const string questTargetID = "Target_ID";
        
        // Kingdom Quest
        [SerializeField] private List<KingdomQuestData> kingdomQuestData;

        private const string kingdomQuestTable = "Quest_Data";

        private const string kingdomQuestID = "Kingdom_Quest_ID";
        private const string kingdomQuestName = "Kingdom_Quest_Name";
        private const string kingdomQuestText = "Kingdom_Quest_Text";
        private const string kingdomQuestStart = "Kingdom_Quest_Start";
        private const string kingdomQuestEnd = "Kingdom_Quest_End";
        private const string monsterID = "Monster_ID";
        private const string guildRewardFame = "Guild_Reward_Fame";
        private const string adventurerRewardExp = "Adventurer_Reward_EXP";
        
        
        // Start is called before the first frame update
        void Start()
        {
            InitQuest();
            RegisterQuest();

            questListUI = FindObjectOfType<QuestListUI>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                questListUI.ShowQuestList(true);
            }
        }

        private void InitQuest()
        {
            questData = new Dictionary<int, QuestData>();
            questList = new();

            var quests = DataParser.Parser(questTableName);

            foreach (var quest in quests)
            {
                var qData = new QuestData()
                {
                    questID = DataParser.IntParse(quest[questID]),
                    questName = quest[questName].ToString(),
                    questRank = (QuestRank)DataParser.EnumParse<QuestRank>(quest[questRank]),
                    questType = (QuestType)DataParser.EnumParse<QuestType>(quest[questType]),
                    questText = quest[questText].ToString(),
                    targetType = DataParser.IntParse(quest[questTargetType]),
                    targetID = DataParser.IntParse(quest[questTargetID])
                };

                questData.Add(qData.questID, qData);
                questList.Add(qData);
            }
            
            //Kingdom Quest

            kingdomQuestData = new List<KingdomQuestData>();

            var kingdomQuests = DataParser.Parser(kingdomQuestTable);

            Debug.Log("ASDfasdfsdf");
            foreach (var kingdomQuest in kingdomQuests)
            {
                var kQuestID = DataParser.IntParse(kingdomQuest[kingdomQuestID]);
                
                Debug.Log(kQuestID);
                var kQuest = new KingdomQuestData()
                {
                    kingdomQuestName = kingdomQuest[kingdomQuestName].ToString(),
                    kingdomQuestText = kingdomQuest[kingdomQuestText].ToString(),
                    kingdomQuestStart = DataParser.IntParse(kingdomQuest[kingdomQuestStart]),
                    kingdomQuestEnd = DataParser.IntParse(kingdomQuest[kingdomQuestEnd]),
                    monsterID = DataParser.IntParse(kingdomQuest[monsterID]),
                    guildRewardFame = DataParser.IntParse(kingdomQuest[guildRewardFame]),
                    adventurerRewardExp = DataParser.IntParse(kingdomQuest[adventurerRewardExp])
                };
                kingdomQuestData.Add(kQuest);
            }
        }

        private void RegisterQuest()
        {
            var noticeBoard = GameObject.FindObjectOfType<NoticeBoard>();

            foreach (var quest in questData)
            {
                noticeBoard.AddQuest(quest.Value);
            }
        }

        public List<QuestData> GetQuestList()
        {
            return questList;
        }

        [Serializable]
        public class QuestData
        {
            public int questID;
            public string questName;
            public QuestRank questRank;
            public QuestType questType;
            public string questText;
            public int targetType;
            public int targetID;
        }

        [Serializable]
        public class KingdomQuestData
        {
            public string kingdomQuestName;
            public string kingdomQuestText;
            public int kingdomQuestStart;
            public int kingdomQuestEnd;
            public int monsterID;
            public int guildRewardFame;
            public int adventurerRewardExp;
        }

        [Serializable]
        public class GuildQuestData
        {
            public string guildQuestName;
            public string guildQuestText;
            public int guildQuestType;
        }
    }
}
