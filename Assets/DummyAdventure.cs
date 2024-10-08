using System.Collections;
using System.Collections.Generic;
using Core.Unit;
using UnityEngine;

public class DummyAdventure : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Adventure(Adventure adventure)
    {
        Debug.Log(adventure.GetQuest().questName);
    }
}
