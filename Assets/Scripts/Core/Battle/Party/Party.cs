using System;
using System.Collections.Generic;
using Core.Guild;
using Core.UI;
using Core.Unit;
using UnityEngine;

namespace Core.Battle.Party
{
    [Serializable]
    public class Party
    {
        [SerializeField] private List<Adventure> party;
        private List<BattleUI> battleUis; 
        
        public Party(List<Adventure> newParty)
        {
            party = newParty;
        }

        public void AddBattleUI(BattleUI newBattleUI)
        {
            if (battleUis == null)
                battleUis = new List<BattleUI>();
            
            battleUis.Add(newBattleUI);
        }

        public BattleUI GetBattleUI(int idx)
        {
            return battleUis[idx];
        }

        public int GetPartyCount()
        {
            return party.Count;
        }

        public Adventure GetAdventure(int idx)
        {
            return party[idx];
        }
    }
}
