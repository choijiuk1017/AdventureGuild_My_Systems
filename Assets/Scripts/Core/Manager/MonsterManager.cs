using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Data;

namespace Core.Manager
{
    public class MonsterManager : MonoBehaviour
    {
        private Dictionary<int, MonsterData> monsterData;
        [SerializeField]
        private List<MonsterData> debugData;

        private const string monsterDataTableName = "Monster_Master";

        private const string monsterID = "Monster_ID";
        private const string monsterName = "Monster_Name_UI";
        private const string monsterMinHp = "Min_HP";
        private const string monsterMaxHp = "Max_HP";
        private const string monsterDefHp = "Def_HP";
        private const string monsterPartType = "Part_Type";
        private const string monsterAttack = "Monster_Attack";
        private const string monsterAction = "Monster_Action";
        private const string monsterSkill1 = "Monster_Skill_1";
        private const string monsterSkill2 = "Monster_Skill_2";
        private const string monsterSkill3 = "Monster_Skill_3";
        /*private const string monsterDropItem1 = "Drop_Item_1";
        private const string monsterDropItem2 = "Drop_Item_2";
        private const string monsterDropItem3 = "Drop_Item_3";
        private const string monsterDropProb1 = "Drop_Prob_1";
        private const string monsterDropProb2 = "Drop_Prob_2";
        private const string monsterDropProb3 = "Drop_Prob_3";*/

        private void Start()
        {
            InitMonsterData();
        }

        private void InitMonsterData()
        {
            monsterData = new Dictionary<int, MonsterData>();
            debugData = new();

            var monsters = DataParser.Parser(monsterDataTableName);

            foreach (var monster in monsters)
            {
                var mData = new MonsterData()
                {
                    monsterID = DataParser.IntParse(monster[monsterID]),
                    monsterName = monster[monsterName].ToString(),
                    /*dropItemCode1 = DataParser.IntParse(monster[monsterDropItem1]),
                    dropItemCode2 = DataParser.IntParse(monster[monsterDropItem2]),
                    dropItemCode3 = DataParser.IntParse(monster[monsterDropItem3]),*/
                    /*dropItemProb1 = DataParser.FloatParse(monster[monsterDropProb1]), 
                    dropItemProb2 = DataParser.FloatParse(monster[monsterDropProb2]), 
                    dropItemProb3 = DataParser.FloatParse(monster[monsterDropProb3])*/ 
                };
                
                debugData.Add(mData);
                monsterData.Add(mData.monsterID, mData);
            }
            
            // MonsterData 초기화
        }

        public MonsterData GetMonsterData(int monsterID)
        {
            return monsterData[monsterID];
        }

        [Serializable]
        public class MonsterData
        {
            public int monsterID;
            public string monsterName;
            
            public int dropItemCode1;
            public int dropItemCode2;
            public int dropItemCode3;
            
            public float dropItemProb1;
            public float dropItemProb2;
            public float dropItemProb3;
        }
    }
}
