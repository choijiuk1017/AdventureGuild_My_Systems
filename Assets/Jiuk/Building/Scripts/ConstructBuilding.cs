using System.Collections;
using System.Collections.Generic;
using Core.Manager;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;



//건물 혹은 타일 설치 관련 스크립트
namespace Core.Building
{
    public class ConstructBuilding : MonoBehaviour
    {

        private Camera mainCam;

        //private Vector3 mPos;

        //타일이 될 오브젝트
        public GameObject obj;

        //캔버스
        public Canvas canvas;

        //타일이 설치될 그리드
        [SerializeField] private CustomGrid customGrid;

        //건물의 프리펩 리스트(사전 등록 필요하나 경로를 통해 불러와도 될듯)
        public List<GameObject> buildingPrefabs;

        //건물 설치 코루틴
        private Coroutine buildingCoroutine;

        private bool isDragging = false; // 드래그 상태를 확인하는 변수
        private Vector3 dragStartPos; // 드래그 시작 위치
        private Vector3 dragEndPos; // 드래그 종료 위치

        // Start is called before the first frame update
        void Start()
        {
            mainCam = Camera.main;
            customGrid = FindObjectOfType<CustomGrid>();

            UpdateBuildingList();
        }

        //설치할 건물의 프리펩을 설정하는 함수
        public void SetBuildingPrefab(string buildingName)
        {
            GameObject buildingPrefab = buildingPrefabs.Find(prefab => prefab.name == buildingName);
            if (buildingPrefab != null)
            {
                obj = buildingPrefab;
                SetBuilding(true);
            }
            else
            {
                Debug.LogWarning("No Prefab found with the given name.");
            }
        }

        //건물을 설정하고, 설치하는 함수
        public void SetBuilding(bool isBuilding)
        {
            if (isBuilding)
            {
                if (buildingCoroutine != null)
                {
                    StopCoroutine(buildingCoroutine);
                }
                buildingCoroutine = StartCoroutine(StartBuilding());
            }
            else
            {
                if (buildingCoroutine != null)
                {
                    StopCoroutine(buildingCoroutine);
                    buildingCoroutine = null;
                }
            }
        }

        //건물의 리스트를 업데이트 하는 함수
        private void UpdateBuildingList()
        {
            foreach (var buildingData in BuildingManager.Instance.GetBuildingList())
            {
                var building = Resources.Load("Building/" + buildingData.buildingName) as GameObject;
                buildingPrefabs.Add(building);
            }
        }

        //건물 설치 코루틴
        private IEnumerator StartBuilding()
        {
            Vector3 mPos = mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            var spawnObj = Instantiate(obj, mPos, quaternion.identity);
            var buildingObj = spawnObj.GetComponent<Building>();

            while (true)
            {
                yield return null;

                mPos = mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
                Vector3 p = new Vector3(Mathf.Round(mPos.x), Mathf.Round(mPos.y));
                spawnObj.transform.position = p;

                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        dragStartPos = p; // 드래그 시작 위치 저장
                        isDragging = true; // 드래그 상태 활성화
                    }

                    if (Input.GetMouseButton(0) && isDragging)
                    {
                        dragEndPos = p; // 드래그 종료 위치 업데이트
                    }

                    if (Input.GetMouseButtonUp(0) && isDragging)
                    {
                        isDragging = false; // 드래그 상태 비활성화
                        HandleDragPlacement(buildingObj); // 드래그 영역에 건물 배치
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        Destroy(spawnObj);
                        SetBuilding(false);
                        yield break;
                    }
                    else if (Input.GetKeyDown(KeyCode.R))
                    {
                        buildingObj.RotationBuilding();
                    }
                }
            }
        }

        //드래그 관련 함수
        private void HandleDragPlacement(Building buildingObj)
        {
            Vector3 start = Vector3.Min(dragStartPos, dragEndPos); // 드래그 시작점과 끝점에서 최소값
            Vector3 end = Vector3.Max(dragStartPos, dragEndPos); // 드래그 시작점과 끝점에서 최대값

            Vector2 buildingSize = buildingObj.GetBuildingSize(); // 오브젝트의 규격 가져오기

            for (float x = start.x; x <= end.x; x += customGrid.nodeDiameter)
            {
                for (float y = start.y; y <= end.y; y += customGrid.nodeDiameter)
                {
                    Vector3 position = new Vector3(x, y, 0);
                    customGrid.SelectNode(position);
                    Node selectedNode = customGrid.GetSelectedNode();

                    if (selectedNode != null)
                    {
                        Vector3 objPos = new Vector3(selectedNode.worldPosition.x, selectedNode.worldPosition.y, 0);

                        if (CanPlaceBuilding(selectedNode, buildingSize))
                        {
                            Instantiate(obj, objPos, quaternion.identity);
                            UpdateNodeTypes(selectedNode, buildingSize);
                        }
                    }
                }
            }
        }

        //건물의 설치 가능 여부 확인 함수
        private bool CanPlaceBuilding(Node selectedNode, Vector2 buildingSize)
        {
            int startX = Mathf.RoundToInt(selectedNode.gridX);
            int startY = Mathf.RoundToInt(selectedNode.gridY);

            // 오브젝트가 Floor 레이어일 경우 Background 노드에만 설치
            if (obj.layer == LayerMask.NameToLayer("Floor") && selectedNode.nodeType != Node.NodeType.Background)
            {
                return false;
            }

            // 다른 오브젝트는 Floor 노드에만 설치
            if (obj.layer != LayerMask.NameToLayer("Floor") && selectedNode.nodeType != Node.NodeType.Floor)
            {
                return false;
            }

            for (int x = 0; x < Mathf.CeilToInt(buildingSize.x); x++)
            {
                for (int y = 0; y < Mathf.CeilToInt(buildingSize.y); y++)
                {
                    int checkX = startX + x;
                    int checkY = startY + y;

                    // 노드 범위를 넘어가거나, 설치 가능한 노드 타입이 맞지 않으면 설치 불가
                    if (checkX < 0 || checkX >= customGrid.gridSizeX || checkY < 0 || checkY >= customGrid.gridSizeY ||
                        (obj.layer == LayerMask.NameToLayer("Floor") && customGrid.grid[checkX, checkY].nodeType != Node.NodeType.Background) ||
                        (obj.layer != LayerMask.NameToLayer("Floor") && customGrid.grid[checkX, checkY].nodeType != Node.NodeType.Floor))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //건물 설치 후 해당 노드의 타입을 업데이트 하는 함수
        private void UpdateNodeTypes(Node selectedNode, Vector2 buildingSize)
        {
            int startX = Mathf.RoundToInt(selectedNode.gridX);
            int startY = Mathf.RoundToInt(selectedNode.gridY);

            for (int x = 0; x < Mathf.CeilToInt(buildingSize.x); x++)
            {
                for (int y = 0; y < Mathf.CeilToInt(buildingSize.y); y++)
                {
                    int updateX = startX + x;
                    int updateY = startY + y;

                    if (updateX >= 0 && updateX < customGrid.gridSizeX && updateY >= 0 && updateY < customGrid.gridSizeY)
                    {
                        Node currentNode = customGrid.grid[updateX, updateY];

                        // 오브젝트의 레이어에 따라 적절한 노드 타입으로 변경
                        if (obj.layer == 10) // Floor 레이어
                        {
                            currentNode.nodeType = Node.NodeType.Floor;
                        }
                        else if (obj.layer == 3) // Obstacle 레이어
                        {
                            currentNode.nodeType = Node.NodeType.Obstacle;
                        }
                        else
                        {
                            currentNode.nodeType = Node.NodeType.Entity;
                        }
                    }
                }
            }
        }
    }
}