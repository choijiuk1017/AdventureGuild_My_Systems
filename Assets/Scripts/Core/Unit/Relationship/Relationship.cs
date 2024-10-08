using System;
using System.Collections.Generic;
using Core.Unit;
using UnityEngine;

[Serializable]
public class Relationship
{
    [SerializeField] private List<RelationshipData> relationshipData = new();

    public void ChangeRelationship(Adventure adventure, float addRelationshipPoint)
    {
        foreach (var rData in relationshipData)
        {
            if (rData.adventure != adventure) continue;

            rData.relationshipPoint = Mathf.Clamp(rData.relationshipPoint + addRelationshipPoint, -400, 400);
            return;
        }

        var newRelationship = new RelationshipData
        {
            adventure = adventure,
            relationshipPoint = addRelationshipPoint,
        };

        relationshipData.Add(newRelationship);
        
        UpdateRelationship();
    }

    private void UpdateRelationship()
    {
        foreach (var rData in relationshipData)
        {
            if (rData.relationshipPoint > 300)
                rData.relationshipType = RelationshipType.BestFriend;
            else if(rData.relationshipPoint > 200)
                rData.relationshipType = RelationshipType.Friend;
            else if (rData.relationshipPoint > 100)
                rData.relationshipType = RelationshipType.Trust;
            else if (rData.relationshipPoint >= 1)
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
}

public enum RelationshipType
{
    Enemy = -4, Hatred, Distrust, NotDesirable, Others, Acquaintance , Trust, Friend, BestFriend
}

[Serializable]
public class RelationshipData
{
    public Adventure adventure;
    public float relationshipPoint;
    public RelationshipType relationshipType;
    //public Sprite adventureImage; // 모험가 초상화
}