using System;
using System.Collections.Generic;
using System.Linq;
using Core.Manager;
using UnityEngine;

namespace Core.Item
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> inventory;
        private int inventoryCount;
        private int curInventoryCount;

        private void Start()
        {
            InitInventory(2);
        }

        public void InitInventory(int inventoryCount)
        {
            inventory = new List<Item>();
            this.inventoryCount = inventoryCount;
            curInventoryCount = 0;
            AddItem(ItemManager.Instance.GetItem(1));
        }

        public List<Item> GetAllItem()
        {
            return inventory;
        }

        public void AddItem(Item newItem)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemID == newItem.itemID)
                {
                    //스택 가능 아이템인지 여부 확인 필요(기획과 상의)
                    inventory[i].itemCount++;
                    return;
                }
            }
            
            inventory.Add(newItem);
        }

        public Item PopItem(Item item)
        {
            Item popItem = null;
            
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemID == item.itemID)
                {
                    popItem = inventory[i];
                    inventory.RemoveAt(i);
                    break;
                }
            }

            return popItem;
        }
        
        //todo 인벤토리 재정렬 함수 필요

        public bool HasItem(int itemID)
        {
            return inventory.Any(t => t.itemID == itemID);
        }

        public bool IsInventoryFull()
        {
            return inventoryCount == curInventoryCount;
        }
    }
}
