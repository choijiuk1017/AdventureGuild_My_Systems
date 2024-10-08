using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    //노드 타입 
    public enum NodeType
    {
        None, Floor, Entity, Obstacle, Last, Background
    }
    //노드가 이동 가능한지 여부
    public bool walkable;

    //노드 월드 좌표
    public Vector2 worldPosition;

    //그리드 상 노드 좌표
    public int gridX;
    public int gridY;

    //노드의 점유 여부 나타내는 변수
    public bool isOccupied = false;

    //시작 지점에서 해당 노드까지의 이동 비용
    public int gCost;

    //해당 노드에서 목표 지점까지 예상 비용
    public int hCost;

    //경로 탐색 중 현재 노드의 이전 노드 저장
    public Node parent;
    public NodeType nodeType;

    public bool isHighlighted = false;  // 외곽선 표시 여부

    public int fCost { get { return gCost + hCost; } }

    public Node(bool walkable, Vector2 worldPos, int gridX, int gridY, NodeType nodeType = NodeType.None)
    {
        this.walkable = walkable;
        this.worldPosition = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
        this.nodeType = nodeType;
    }

    public void SetHighlighted(bool highlighted)
    {
        isHighlighted = highlighted;
    }
}
