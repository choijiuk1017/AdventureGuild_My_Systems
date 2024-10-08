using System;
using Core.Guild;
using Core.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class AdventureListItem : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text ageText;
        [SerializeField] private Text strText;
        [SerializeField] private Text agiText;
        [SerializeField] private Text intText;
        [SerializeField] private Text hpText;
        [SerializeField] private Text mpText;
        [SerializeField] private Text rankText;

        public void SetAdventureInfo(Adventure adventure)
        {
            var adventureInfo = adventure.AdventureInfo;

            nameText.text = adventureInfo.AdventureName;
            ageText.text = adventureInfo.AdventureAge.ToString();
            strText.text = adventureInfo.AdventureStat.str.ToString();
            agiText.text = adventureInfo.AdventureStat.agi.ToString();
            intText.text = adventureInfo.AdventureStat.inte.ToString();
            hpText.text = adventureInfo.AdventureStat.hp.ToString();
            mpText.text = adventureInfo.AdventureStat.mp.ToString();
            rankText.text = adventureInfo.AdventureRank.ToString();
        }
    }
}
