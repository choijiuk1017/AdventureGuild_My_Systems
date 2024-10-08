using System.Collections;
using System.Collections.Generic;
using Core.Item;
using Core.Unit;
using Core.Unit.Monster;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    [SerializeField] private Monster monster;
    [SerializeField] private Adventure adventure;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GetDropItem", 4f);
    }

    public void GetDropItem()
    {
        var dropItems = monster.DropItem();

        foreach (var dropItem in dropItems)
        {
            Debug.Log(dropItem.itemName);
            adventure.GetComponent<Inventory>().AddItem(dropItem);
        }
    }
}
