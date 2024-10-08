using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private AdventureListUI adventureListUI;

    private void Awake()
    {
        adventureListUI = FindObjectOfType<AdventureListUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            adventureListUI.ShowAdventureList(true);
        else if (Input.GetKeyDown(KeyCode.Escape))
            adventureListUI.ShowAdventureList(false);
    }
}
