using Core.Manager;
using Core.Unit;
using UnityEngine;
using UnityEngine.UI;
using EventType = Core.Manager.EventType;

namespace Core.UI
{
    public class RecruitUI : MonoBehaviour
    {
        [SerializeField] private Text strText;
        [SerializeField] private Text intText;
        [SerializeField] private Text agiText;
        [SerializeField] private Text hpText;
        [SerializeField] private Text mpText;
        [SerializeField] private Text nameText;
        [SerializeField] private Text ageText;
        [SerializeField] private Button cancel;
        [SerializeField] private Button register;

        private Adventure adventurer;
        
        // Start is called before the first frame update
        void Start()
        {
            strText = transform.Find("STR").GetComponent<Text>();
            intText = transform.Find("INT").GetComponent<Text>();
            agiText = transform.Find("AGI").GetComponent<Text>();
            hpText = transform.Find("HP").GetComponent<Text>();
            mpText = transform.Find("MP").GetComponent<Text>();
            nameText = transform.Find("Name").GetComponent<Text>();
            ageText = transform.Find("Age").GetComponent<Text>();
            cancel = transform.Find("Cancel").GetComponent<Button>();
            register = transform.Find("Register").GetComponent<Button>();
            
            register.onClick.AddListener(AcceptRecruit);
            cancel.onClick.AddListener(RefuseRecruit);
        }

        public void SetRecruitData(Adventure adventurer)
        {
            this.adventurer = adventurer;
            var adventureInfo = adventurer.AdventureInfo;
            
            strText.text = "STR : " + adventureInfo.AdventureStat.str;
            intText.text = "INT : " + adventureInfo.AdventureStat.inte;
            agiText.text = "AGI : " + adventureInfo.AdventureStat.agi;
            hpText.text = "HP : " + adventureInfo.AdventureStat.hp;
            mpText.text = "MP : " + adventureInfo.AdventureStat.mp;
            nameText.text = "이름 : " + adventureInfo.AdventureName;
            ageText.text = "나이 : " + adventureInfo.AdventureAge;
        }

        public void ShowRecruitUI(bool isOn)
        {
            gameObject.SetActive(isOn);
        }

        public void AcceptRecruit()
        {
            EventBus.Publish(EventType.FinishReception);
            EventBus.Publish(EventType.CompleteRecruit);
            //GuildManager.Instance.GetGuild().AddAdventure();
            ShowRecruitUI(false);
        }

        public void RefuseRecruit()
        {
            EventBus.Publish(EventType.FinishReception);
            adventurer.AdventureAI.ChangeState(AdventureStateType.Exit);
            ShowRecruitUI(false);
        }
    }
}
