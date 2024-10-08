using System.Collections.Generic;
using System.Linq;
using Core.Battle.Party;
using Core.Guild;
using Core.Unit;
using UnityEngine;

namespace Core.Manager
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private List<Battle.Battle> battles;
        
        // Start is called before the first frame update
        void Start()
        {
            var adventureList = FindObjectsByType<Adventure>(FindObjectsSortMode.None).ToList();

            Party party = new Party(adventureList);

            battles.Add(GetComponent<Battle.Battle>());

            battles[0].InitBattle(party);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
