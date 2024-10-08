using System;
using Core.Guild;
using Core.Manager;
using UnityEngine;

namespace Core.UI
{
    public class AdventureListUI : MonoBehaviour
    {
        [SerializeField] private GameObject adventureList;
        [SerializeField] private GameObject listItem;
    
        // Start is called before the first frame update
        void Start()
        {
            adventureList = FindObjectOfType<AdventureListUI>().gameObject;
            listItem = Resources.Load<GameObject>("Prefabs/UI/AdventureListItem");

            ShowAdventureList(false);
        }

        private void UpdateAdventureList()
        {
            DeleteAdventureListItems();
            
            var adventures = GuildManager.Instance.GetGuild().GetAdventures();

            foreach (var adventure in adventures)
            {
                var item = Instantiate(listItem, adventureList.transform).GetComponent<AdventureListItem>();

                item.SetAdventureInfo(adventure);
            }
        }

        public void ShowAdventureList(bool isOn)
        {
            if (isOn)
                UpdateAdventureList();
            
            gameObject.SetActive(isOn);
        }

        private void DeleteAdventureListItems()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
