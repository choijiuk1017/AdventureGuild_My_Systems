using System;
using UnityEngine;

namespace Core.Item
{
    [Serializable]
    public class Item
    {
        public int itemID;
        public int itemCount;
        public int itemPrice;
        public Sprite itemIcon;
        public string itemText;
        public string itemName;
    }
}
