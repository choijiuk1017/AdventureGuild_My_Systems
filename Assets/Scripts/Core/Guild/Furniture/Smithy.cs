using System;
using UnityEngine;
using System.Collections;
using Core.Manager;

namespace Core.Guild.Furniture
{
    public class Smithy : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(CheckItem());
        }

        private IEnumerator CheckItem()
        {
            yield return new WaitForSeconds(5f);
            
            var inventory = GuildManager.Instance.GuildInventory;
            var combinationList = ItemManager.Instance.GetAllCombinationList();
            
            Debug.Log(combinationList.Count);

            for (int i = 0; i < combinationList.Count; i++)
            {
                if(inventory.HasItem(combinationList[i].stuff1ID) && inventory.HasItem(combinationList[i].stuff2ID))
                    Debug.Log(combinationList[i].itemName);
            }

            StartCoroutine(CheckItem());
        }
    }
}
