using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private RelationshipData rData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            updateR();
        }
    }

    void updateR()
    {
        if (rData.relationshipPoint > 300)
            rData.relationshipType = RelationshipType.BestFriend;
        else if(rData.relationshipPoint > 200)
            rData.relationshipType = RelationshipType.Friend;
        else if (rData.relationshipPoint > 100)
            rData.relationshipType = RelationshipType.Trust;
        else if (rData.relationshipPoint > 1)
            rData.relationshipType = RelationshipType.Acquaintance;
        else if (rData.relationshipPoint >= -1)
            rData.relationshipType = RelationshipType.Others;
        else if (rData.relationshipPoint >= -101)
            rData.relationshipType = RelationshipType.NotDesirable;
        else if (rData.relationshipPoint >= -201)
            rData.relationshipType = RelationshipType.Distrust;
        else if (rData.relationshipPoint >= -301)
            rData.relationshipType = RelationshipType.Hatred;
        else if (rData.relationshipPoint >= -400)
            rData.relationshipType = RelationshipType.Enemy;
    }
}