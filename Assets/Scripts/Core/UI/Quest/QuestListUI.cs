using Core.Manager;
using UnityEngine;

namespace Core.UI.Quest
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] private GameObject listItem;
    
        // Start is called before the first frame update
        private void Start()
        {
            listItem = Resources.Load<GameObject>("Prefabs/UI/QuestListItem");
            
            ShowQuestList(false);
        }

        private void UpdateQuestList()
        {
            DeleteQuestListItems();

            var quests = QuestManager.Instance.GetQuestList();

            foreach (var quest in quests)
            {
                var item = Instantiate(listItem, transform).GetComponent<QuestListItem>();

                item.SetQuestInfo(quest);
            }
        }

        public void ShowQuestList(bool isOn)
        {
            if (isOn)
                UpdateQuestList();
            
            gameObject.SetActive(isOn);
        }

        private void DeleteQuestListItems()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
