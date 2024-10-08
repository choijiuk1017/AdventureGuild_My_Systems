using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Data;

namespace Core.Manager
{
    public enum ItemType
    {
        Stuff = 1, Weapon
    }
    
    public class ItemManager : Singleton<ItemManager>
    {
        private Dictionary<int, ItemData> itemList;
        [SerializeField] private List<ItemData> debugData;

        private Dictionary<int, ItemCombinationData> itemCombinationList;

        private const string itemDataTableName = "Item_Master";
        private const string itemID = "Item_ID";
        private const string itemName = "Item_Name";
        private const string itemUiName = "Item_Name_UI";
        private const string itemIcon = "Item_Icon";
        private const string itemText = "Item_Text";
        private const string itemType = "Item_Type";
        private const string itemPrice = "Item_Price_Def";
        private const string itemPriceMin = "Item_Price_Min";
        private const string itemPriceMax = "Item_Price_Max";

        private const string itemCombinationTableName = "Combination_Master";
        private const string itemCombinationID = "Item_ID";
        private const string itemCombinationName = "Item_Name";
        private const string itemCombinationStuff1 = "Stuff_1";
        private const string itemCombinationStuff1Quantity = "Stuff_Quantity_1";
        private const string itemCombinationStuff2 = "Stuff_2";
        private const string itemCombinationStuff2Quantity = "Stuff_Quantity_2";
        private const string itemCombinationStuff3 = "Stuff_3";
        private const string itemCombinationStuff3Quantity = "Stuff_Quantity_3";
        private const string itemCombinationManaStoneType1 = "ManaStone_Type_1";
        private const string itemCombinationManaStoneType2 = "ManaStone_Type_2";
        private const string itemCombinationManaStoneType3 = "ManaStone_Type_3";
        private const string itemCombinationManaStoneType4 = "ManaStone_Type_4";
        
        
        // Start is called before the first frame update
        void Start()
        {
            InitItemData();
            InitItemCombinationData();
        }

        private void InitItemData()
        {
            itemList = new Dictionary<int, ItemData>();
            debugData = new();

            var items = DataParser.Parser(itemDataTableName);
            
            foreach (var item in items)
            {
                var itemId = DataParser.IntParse(item[itemID]);

                var iData = new ItemData()
                {
                    itemName = item[itemName].ToString(),
                    itemUiName = item[itemUiName].ToString(),
                    //icon image
                    itemText = item[itemText].ToString(),
                    itemType = (ItemType)DataParser.EnumParse<ItemType>(item[itemType]),
                    itemPrice = DataParser.IntParse(item[itemPrice]),
                    itemPriceMin = DataParser.IntParse(item[itemPriceMin]),
                    itemPriceMax = DataParser.IntParse(item[itemPriceMax])
                };
                
                itemList.Add(itemId, iData);
                debugData.Add(iData);
            }
        }

        private void InitItemCombinationData()
        {
            itemCombinationList = new Dictionary<int, ItemCombinationData>();

            var combinationList = DataParser.Parser(itemCombinationTableName);

            foreach (var itemCombination in combinationList)
            {
                var itemId = DataParser.IntParse(itemCombination[itemCombinationID]);

                var combinationData = new ItemCombinationData()
                {
                    itemName = itemCombination[itemCombinationName].ToString(),
                    stuff1ID = DataParser.IntParse(itemCombination[itemCombinationStuff1]),
                    stuff1Quantity = DataParser.IntParse(itemCombination[itemCombinationStuff1Quantity]),
                    stuff2ID = DataParser.IntParse(itemCombination[itemCombinationStuff2]),
                    stuff2Quantity = DataParser.IntParse(itemCombination[itemCombinationStuff2Quantity]),
                    stuff3ID = DataParser.IntParse(itemCombination[itemCombinationStuff3]),
                    stuff3Quantity = DataParser.IntParse(itemCombination[itemCombinationStuff3Quantity]),
                    manaStone1 = DataParser.IntParse(itemCombination[itemCombinationManaStoneType1]),
                    manaStone2 = DataParser.IntParse(itemCombination[itemCombinationManaStoneType2]),
                    manaStone3 = DataParser.IntParse(itemCombination[itemCombinationManaStoneType3]),
                    manaStone4 = DataParser.IntParse(itemCombination[itemCombinationManaStoneType4])
                };
                
                itemCombinationList.Add(itemId, combinationData);
            }
        }

        public ItemData GetItemData(int itemID)
        {
            return itemList[itemID];
        }

        public Item.Item GetItem(int itemID)
        {
            //itemID--;
            var item = new Item.Item()
            {
                itemID = itemID,
                itemIcon = itemList[itemID].itemIcon,
                itemText = itemList[itemID].itemText,
                itemName = itemList[itemID].itemUiName
            };

            return item;
        }

        public List<ItemCombinationData> GetAllCombinationList()
        {
            var combinationList = new List<ItemCombinationData>();

            foreach (var combination in itemCombinationList)
            {
                combinationList.Add(combination.Value);
            }

            return combinationList;
        }


        //public void ChangePrices(float factor, int itemID)
        //{
        //    foreach (Item item in itemList)
        //    {
        //        if (item.Item_ID == itemID)
        //        {
        //            item.Item_Price_Def *= factor;
        //            Debug.Log(item.Item_ID + " 아이템 가격 변경: " + item.Item_Price_Def); //확인용 로그

        //            if (OnPriceChanged != null)
        //                OnPriceChanged();

        //            break;
        //        }
        //    }
        //}
    }

    [Serializable]
    public class ItemData
    {
        public string itemName;
        public string itemUiName;
        public Sprite itemIcon;
        public string itemText;
        public ItemType itemType;
        public int itemPrice;
        public int itemPriceMin;
        public int itemPriceMax;
    }

    [Serializable]
    public class ItemCombinationData
    {
        public string itemName;
        public int stuff1ID;
        public int stuff1Quantity;
        public int stuff2ID;
        public int stuff2Quantity;
        public int stuff3ID;
        public int stuff3Quantity;
        public int manaStone1;
        public int manaStone2;
        public int manaStone3;
        public int manaStone4;
    }
}
