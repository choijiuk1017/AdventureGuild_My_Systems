using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//그리드 스크립트
public class CustomGrid : MonoBehaviour
{
    //노드의 색상과 그리드를 보여줄지를 결정하는 변수
    public bool displayGridGizmos;

    //현재 그리드 내에 있는 모험가를 담아두는 리스트
    public GameObject[] main;

    //뒷 배경
    public GameObject background; 

    //장애물 레이어 마스크
    public LayerMask OBSTACLE;

    //엔티티 레이어 마스크
    public LayerMask ENTITY;


    public Vector2 gridWorldSize;

    public float nodeRadius;

    //노드 타입과 좌표 정보를 배열로 그리드 구성
    public Node[,] grid;


    public float nodeDiameter;

    public int gridSizeX, gridSizeY;


    //길찾기 알고리즘에서 사용할 노드 리스트
    [SerializeField]
    public List<Node> path;

    public Node selectedNode;


    //그리드 생성
    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        
        CreateGrid();
    }

    private void Update()
    {
        UnityEditor.SceneView.RepaintAll();
    }

    private void OnDrawGizmos()
    {
        if (!displayGridGizmos) return;

        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = GetGizmoColor(n);

                if (n.isHighlighted)  // 선택된 노드에 외곽선 표시
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireCube(n.worldPosition, Vector2.one * nodeDiameter);
                }
                else
                {
                    Gizmos.DrawCube(n.worldPosition, Vector2.one * nodeDiameter);
                }
            }
        }
    }

    //그리드의 각 노드를 생성 후 노드의 타입을 결정하는 함수
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);


                bool isObstacle = Physics2D.OverlapCircle(worldPoint, nodeRadius, OBSTACLE);
                bool isEntity = Physics2D.OverlapCircle(worldPoint, nodeRadius, ENTITY);

                Node.NodeType nodeType;
                if (isObstacle)
                {
                    nodeType = Node.NodeType.Obstacle;
                }
                else if (isEntity)
                {
                    nodeType = Node.NodeType.Entity;
                }
                else
                {
                    nodeType = IsPointInBackground(worldPoint) ? Node.NodeType.Background : Node.NodeType.None;
                }

                grid[x, y] = new Node(nodeType == Node.NodeType.Floor, worldPoint, x, y, nodeType);
            }
        }
    }

    //배경 오브젝트 관련 함수
    bool IsPointInBackground(Vector2 point)
    {
        if (background != null)
        {
            Collider2D backgroundCollider = background.GetComponent<Collider2D>();
            if (backgroundCollider != null)
            {
                return backgroundCollider.OverlapPoint(point);
            }
        }
        return false;
    }

    //특정 노드 주변의 이웃 노드를 리스트로 반환하는 함수
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }


    //주어진 월드 좌표에서 해당하는 노드 반환하는 함수
    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    
    public Node GetSelectedNode()
    {
        return selectedNode;
    }


    //특정 좌표에 해당하는 노드 선택
    public void SelectNode(Vector2 worldPosition)
    {
        selectedNode = NodeFromWorldPoint(worldPosition);
    }


    //노드 타입 별 색상
    private Color GetGizmoColor(Node node)
    {
        switch (node.nodeType)
        {
            case Node.NodeType.Floor:
                return new Color(0, 1, 0, 0.3f);  // Green
            case Node.NodeType.Obstacle:
                return new Color(1, 0, 0, 0.3f);  // Red
            case Node.NodeType.Entity:
                return new Color(0, 0, 1, 0.3f);  // Blue
            case Node.NodeType.Background:
                return new Color(1, 1, 0, 0.3f);  // Yellow
            default:
                return new Color(1, 1, 1, 0.3f);
        }
    }
}