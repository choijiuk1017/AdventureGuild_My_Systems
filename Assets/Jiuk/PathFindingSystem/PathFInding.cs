using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Utility.Algorithm
{
    public class PathFinding : MonoBehaviour
    {
        //�׸���
        [SerializeField]
        private CustomGrid grid;

        //seeker:���谡, target:������
        public Transform seeker, target;

        //�̵��ӵ�
        public float moveSpeed = 2f;

        //�̵��� ��� ��ǥ���� ������� ����� ť
        public Queue<Vector2> wayQueue = new Queue<Vector2>();

        private void Awake()
        {
            moveSpeed = 2f;
            this.grid = FindAnyObjectByType<CustomGrid>();
        }

        //������������ ���������� ��θ� ã�� �Լ�
        //FindPath �޼���� ��θ� ����ϰ� FollowPath �ڷ�ƾ���� �̵�
        public void StartFindPath(Vector2 startPos, Transform targetTransform)
        {
            target = targetTransform;
            wayQueue.Clear();
            FindPath(startPos, targetTransform.position);
            StartCoroutine(FollowPath());
        }

        //Transform�� �ƴ� ���� ��ǥ�� �̿��Ͽ� ��θ� ã�� �Լ�
        public void StartFindPathToWorldPosition(Vector2 startPos, Vector2 worldPosition)
        {
            Debug.Log("���� ��ǥ�� ��� ã�� ����");
            wayQueue.Clear();
            FindPath(startPos, worldPosition);
            StartCoroutine(FollowPath());
        }

        //A* �˰���
        void FindPath(Vector2 startPos, Vector2 targetPos)
        {
            //��� �ʱ�ȭ
            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);

            //Ž���� ��� ����Ʈ
            List<Node> openSet = new List<Node> { startNode };

            //Ž���� ��� ����Ʈ
            HashSet<Node> closedSet = new HashSet<Node>();

            //���� ��� ���
            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    //fcost(�� ���) �������� ���� ������ ��带 ����
                    if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                //��ǥ ��忡 ���� ��, ��θ� �����Ͽ� ����
                if (currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                //�̿� ��� �˻�
                //�̿��� ��带 �˻��ϰ�, ����� �� ���ٸ� ����
                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    if (neighbour.nodeType == Node.NodeType.Obstacle ||
                        (neighbour.nodeType == Node.NodeType.Entity && neighbour != targetNode))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }

        //��θ� ��¤�� ���� startNode���� endNode���� ��� ���� �� ť�� ��ǥ ����
        void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();

            wayQueue.Clear();
            wayQueue = new Queue<Vector2>(path.Select(node => node.worldPosition));
        }

        //�� ��� ���� �Ÿ� ��� �Լ�
        //�밢�� �̵��� 14, ���� �̵��� 10�� ���
        int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }

        //�˰����� �̿��� ������ ��θ� ���� ���谡�� �̵��ϴ� �ڷ�ƾ
        IEnumerator FollowPath()
        {
            while (wayQueue.Count > 0)
            {
                Vector2 targetPosition = wayQueue.Dequeue();
                while ((Vector2)transform.position != targetPosition)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
    }
}