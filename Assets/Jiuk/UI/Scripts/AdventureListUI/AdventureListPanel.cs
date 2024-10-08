using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.UI;
using Core.Unit;


namespace Core.UI
{
    public class AdventureListPanel : MonoBehaviour
    {
        public GameObject panel;

        public RectTransform content;

        public GameObject adventureListPrefab;

        private List<Adventure> adventure;

        public GameObject guildManager;

        // Start is called before the first frame update
        void Start()
        {

            SpawnUI();

            
        }

        void SpawnUI()
        {
            for(int i = 0; i < 5; i++)
            {
                GameObject adventureList = Instantiate(adventureListPrefab, content);

                AdventureList adventureListUI = adventureList.GetComponent<AdventureList>();

                //adventureListUI.SetUpUI();
            }
        }

        public void ExitButton()
        {
            gameObject.SetActive(false);
        }
    }
}

