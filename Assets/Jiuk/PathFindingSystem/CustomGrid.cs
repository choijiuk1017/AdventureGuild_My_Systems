using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�׸��� ��ũ��Ʈ
public class CustomGrid : MonoBehaviour
{
    //����� ����� �׸��带 ���������� �����ϴ� ����
    public bool displayGridGizmos;

    //���� �׸��� ���� �ִ� ���谡�� ��Ƶδ� ����Ʈ
    public GameObject[] main;

    //�� ���
    public GameObject background; 

    //��ֹ� ���̾� ����ũ
    public LayerMask OBSTACLE;

    //��ƼƼ ���̾� ����ũ
    public LayerMask ENTITY;


    public Vector2 gridWorldSize;

    public float nodeRadius;

    //��� Ÿ�԰� ��ǥ ������ �迭�� �׸��� ����
    public Node[,] grid;


    public float nodeDiameter;

    public int gridSizeX, gridSizeY;


    //��ã�� �˰��򿡼� ����� ��� ����Ʈ
    [SerializeField]
    public List<Node> path;

    public Node selectedNode;


    //�׸��� ����
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

                if (n.isHighlighted)  // ���õ� ��忡 �ܰ��� ǥ��
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

    //�׸����� �� ��带 ���� �� ����� Ÿ���� �����ϴ� �Լ�
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

    //��� ������Ʈ ���� �Լ�
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

    //Ư�� ��� �ֺ��� �̿� ��带 ����Ʈ�� ��ȯ�ϴ� �Լ�
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


    //�־��� ���� ��ǥ���� �ش��ϴ� ��� ��ȯ�ϴ� �Լ�
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


    //Ư�� ��ǥ�� �ش��ϴ� ��� ����
    public void SelectNode(Vector2 worldPosition)
    {
        selectedNode = NodeFromWorldPoint(worldPosition);
    }


    //��� Ÿ�� �� ����
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