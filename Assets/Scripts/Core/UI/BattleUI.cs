using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class BattleUI : MonoBehaviour
    {
        private Slider actionGauge;
        private Slider hpBar;
    
        // Start is called before the first frame update
        private void Start()
        {
            InitUI();
        }

        private void InitUI()
        {
            actionGauge = transform.Find("ActionGauge").GetComponent<Slider>();
            hpBar = transform.Find("HpBar").GetComponent<Slider>();
        }

        public void UpdateActionGauge(float newActionGaugeValue)
        {
            actionGauge.value = newActionGaugeValue;
        }

        public void UpdateHpBar(float newHpValue)
        {
            hpBar.value = newHpValue;
        }
    }
}
