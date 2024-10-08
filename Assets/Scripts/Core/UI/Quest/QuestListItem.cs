using System;
using Core.Guild;
using Core.Manager;
using Core.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class QuestListItem : MonoBehaviour
    {
        [SerializeField] private Text questName;
        [SerializeField] private Text questRank;
        [SerializeField] private Text questText;

        public void SetQuestInfo(QuestManager.QuestData questData)
        {
            questName.text = questData.questName;
            questRank.text = questData.questRank.ToString();
            questText.text = questData.questText;
        }
    }
}
