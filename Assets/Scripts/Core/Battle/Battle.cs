using System;
using System.Collections;
using System.Collections.Generic;
using Core.Item;
using Core.Manager;
using Core.UI;
using Core.Unit.Monster;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Battle
{
    public class Battle : MonoBehaviour
    {
        [SerializeField] private Party.Party party;
        [SerializeField] private List<Monster> monsters;
        [SerializeField] private List<BattleUI> monsterBattleUis;
        [SerializeField] private GameObject battleUiPrefabs;
        [SerializeField] private Transform canvas;
        [SerializeField] private Inventory dropItem;
        [SerializeField] private List<Monster> deadMonsters;
        [SerializeField] private PurchaseSystem purchaseSystem; 
        [SerializeField] private UIManager uiManager;

        private Camera battleCam;

        private bool battleEnd;

        private void Awake()
        {
            battleUiPrefabs = Resources.Load("UI/BattleUI/BattleUI") as GameObject;
            canvas = GameObject.Find("Canvas").transform;
            uiManager = FindAnyObjectByType<UIManager>();
            

        }

        // Start is called before the first frame update
        private void Start()
        {
            //battleCam = transform.Find("BattleCam").GetComponent<Camera>();
            
            StartCoroutine(BattleCoroutine());
        }
        
        private IEnumerator BattleCoroutine()
        {
            while (!battleEnd)
            {
                yield return null;
                
                for (int i = 0; i < party.GetPartyCount(); i++)
                {
                    var adventure = party.GetAdventure(i);
                    var battleUI = party.GetBattleUI(i);
                    adventure.BattleStats.actionGauge += adventure.AdventureInfo.AdventureStat.agi * Time.deltaTime;
                    battleUI.UpdateActionGauge(adventure.BattleStats.actionGauge / 100f);
                    
                    Debug.Log(adventure.name + " : " + adventure.BattleStats.actionGauge);

                    if (adventure.BattleStats.actionGauge >= 100f)
                    {
                        var targetMonster = monsters[GetRandomTargetMonster()];
                        adventure.Attack(targetMonster);
                        UpdateBattleInfo();
                        yield return new WaitForSeconds(1f);
                        adventure.BattleStats.actionGauge = 0;
                        battleUI.UpdateActionGauge(adventure.BattleStats.actionGauge / 100f);
                    }
                }


                for (int j = monsters.Count - 1; j >= 0; j--)
                {
                    var monster = monsters[j];
                    var battleUI = monsterBattleUis[j];

                    if (monster.GetStat().curHp == 0)
                    {
                        deadMonsters.Add(monster);
                        monsters.RemoveAt(j);
                        Destroy(monsterBattleUis[j].gameObject); // UI 객체 제거
                        monsterBattleUis.RemoveAt(j); // UI 리스트에서 제거
                        continue;
                    }

                    monster.GetBattleStats().actionGauge += monster.GetStat().agi * Time.deltaTime;
                    battleUI.UpdateActionGauge(monster.GetBattleStats().actionGauge / 100f);

                    if (monster.GetBattleStats().actionGauge >= 100f)
                    {
                        var target = party.GetAdventure(GetRandomTargetAdventure());
                        monster.Attack(target);
                        UpdateBattleInfo();
                        yield return new WaitForSeconds(1f);
                        monster.GetBattleStats().actionGauge = 0;
                        battleUI.UpdateActionGauge(monster.GetBattleStats().actionGauge / 100f);
                    }
                }

                if (monsters.Count == 0)
                {
                    battleEnd = true;
                }
                //int j = 0;
                //foreach (var monster in monsters)
                //{
                //    var battleUI = monsterBattleUis[j++];

                //    if (monster.GetStat().curHp == 0)
                //    {
                //        deadMonsters.Add(monster);
                //        monsters.Remove(monster);
                //        continue;
                //    }

                //    monster.GetBattleStats().actionGauge += monster.GetStat().agi * Time.deltaTime;
                //    battleUI.UpdateActionGauge(monster.GetBattleStats().actionGauge / 100f);

                //    if (monster.GetBattleStats().actionGauge >= 100f)
                //    {
                //        var target = party.GetAdventure(GetRandomTargetAdventure());
                //        monster.Attack(target);
                //        UpdateBattleInfo();
                //        yield return new WaitForSeconds(1f);
                //        monster.GetBattleStats().actionGauge = 0;
                //        battleUI.UpdateActionGauge(monster.GetBattleStats().actionGauge / 100f);
                //    }
                //}
            }
            
            EndBattle();
        }

        public void InitBattle(Party.Party p)
        {
            //dropItem = new();
            party = p;
            deadMonsters = new();
          
            for (int i = 0; i < party.GetPartyCount(); i++)
            {
                var uiPos = Camera.main.WorldToScreenPoint(party.GetAdventure(i).transform.position + new Vector3(0, 2, 0));
                var battleUI = Instantiate(battleUiPrefabs, uiPos, Quaternion.identity, canvas).GetComponent<BattleUI>();
                party.AddBattleUI(battleUI);
            }
            
            monsterBattleUis = new List<BattleUI>();

            for (int i = 0; i < monsters.Count; i++)
            {
                var uiPos = Camera.main.WorldToScreenPoint(monsters[i].transform.position + new Vector3(0, 2, 0));
                var battleUI = Instantiate(battleUiPrefabs, uiPos, Quaternion.identity, canvas).GetComponent<BattleUI>();
                monsterBattleUis.Add(battleUI);
            }
        }

        public void AddDropItem(Item.Item newDropItem)
        {
            dropItem.AddItem(newDropItem);
        }

        private int GetRandomTargetAdventure()
        {
            return Random.Range(0, party.GetPartyCount());
        }

        private int GetRandomTargetMonster()
        {
            return Random.Range(0, monsters.Count);
        }

        private void UpdateBattleInfo()
        {
            for (int i = 0; i < party.GetPartyCount(); i++)
            {
                var unitStat = party.GetAdventure(i).GetStat();
                party.GetBattleUI(i).UpdateHpBar(unitStat.curHp / unitStat.hp);
            }

            for (int i = 0; i < monsters.Count; i++)
            {
                var unitStat = monsters[i].GetStat();
                monsterBattleUis[i].UpdateHpBar(unitStat.curHp / unitStat.hp);
            }
        }

        private void EndBattle()
        {
            foreach (var deadMonster in deadMonsters)
            {
                Debug.Log("전투 종료");
                var dItem = ItemManager.Instance.GetItem(deadMonster.GetMonsterID());
                dropItem.AddItem(dItem);
                uiManager.ShowPanel(0);
                purchaseSystem = FindAnyObjectByType<PurchaseSystem>();
                purchaseSystem.AddItemToPurchase(dItem, Random.Range(1, 31));
            }
        }
    }
}
