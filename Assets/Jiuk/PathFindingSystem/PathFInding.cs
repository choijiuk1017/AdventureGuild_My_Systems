using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Utility.Algorithm
{
    public class PathFinding : MonoBehaviour
    {
        //그리드
        [SerializeField]
        private CustomGrid grid;

        //seeker:모험가, target:목적지
        public Transform seeker, target;

        //이동속도
        public float moveSpeed = 2f;

        //이동할 경로 좌표들이 순서대로 저장될 큐
        public Queue<Vector2> wayQueue = new Queue<Vector2>();

        private void Awake()
        {
            moveSpeed = 2f;
            this.grid = FindAnyObjectByType<CustomGrid>();
        }

        //시작지점에서 목적지까지 경로를 찾는 함수
        //FindPath 메서드로 경로를 계산하고 FollowPath 코루틴으로 이동
        public void StartFindPath(Vector2 startPos, Transform targetTransform)
        {
            target = targetTransform;
            wayQueue.Clear();
            FindPath(startPos, targetTransform.position);
            StartCoroutine(FollowPath());
        }

        //Transform이 아닌 월드 좌표를 이용하여 경로를 찾는 함수
        public void StartFindPathToWorldPosition(Vector2 startPos, Vector2 worldPosition)
        {
            Debug.Log("월드 좌표로 경로 찾기 시작");
            wayQueue.Clear();
            FindPath(startPos, worldPosition);
            StartCoroutine(FollowPath());
        }

        //A* 알고리즘
        void FindPath(Vector2 startPos, Vector2 targetPos)
        {
            //노드 초기화
            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);

            //탐색할 노드 리스트
            List<Node> openSet = new List<Node> { startNode };

            //탐색한 노드 리스트
            HashSet<Node> closedSet = new HashSet<Node>();

            //최적 경로 계산
            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    //fcost(총 비용) 기준으로 가장 최적의 노드를 선택
                    if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                //목표 노드에 도달 시, 경로를 추적하여 저장
                if (currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                //이웃 노드 검사
                //이웃한 노드를 검사하고, 비용이 더 적다면 갱신
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

        //경로를 되짚어 가며 startNode에서 endNode까지 경로 생성 후 큐에 좌표 저장
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

        //두 노드 간의 거리 계산 함수
        //대각선 이동은 14, 직선 이동은 10의 비용
        int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }

        //알고리즘을 이용해 생성된 경로를 따라 모험가가 이동하는 코루틴
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