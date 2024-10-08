using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Building : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mPos;
    public GameObject obj;
    [SerializeField] private CustomGrid customGrid;

    public List<GameObject> buildingPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        customGrid = FindObjectOfType<CustomGrid>();
        StartCoroutine(StartBuilding());
    }

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
            Debug.LogWarning("No Prefab founf with the given name.");
        }
    }
    public void SetBuilding(bool isBuilding)
    {
        if (isBuilding)
            StartCoroutine(StartBuilding());
        else
            StopCoroutine(StartBuilding());
    }

    private IEnumerator StartBuilding()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                customGrid.SelectNode(mPos);
                Node selectedNode = customGrid.GetSelectedNode();

                if (selectedNode != null && selectedNode.nodeType == Node.NodeType.Floor)
                {
                    Vector2 buildingSize = GetBuildingSize(obj);
                    Vector3 objPos = new Vector3(selectedNode.worldPosition.x, selectedNode.worldPosition.y, 0);

                    if (CanPlaceBuilding(selectedNode, buildingSize))
                    {
                        Instantiate(obj, objPos, Quaternion.identity);
                        UpdateNodeTypes(selectedNode, buildingSize);
                    }
                }
            }
        }
    }

    private bool CanPlaceBuilding(Node startNode, Vector2 buildingSize)
    {
        int startX = Mathf.RoundToInt(startNode.gridX - buildingSize.x / 2);
        int startY = Mathf.RoundToInt(startNode.gridY - buildingSize.y / 2);

        for (int x = 0; x < Mathf.CeilToInt(buildingSize.x); x++)
        {
            for (int y = 0; y < Mathf.CeilToInt(buildingSize.y); y++)
            {
                int checkX = startX + x;
                int checkY = startY + y;

                if (checkX < 0 || checkX >= customGrid.gridSizeX || checkY < 0 || checkY >= customGrid.gridSizeY || customGrid.grid[checkX, checkY].nodeType != Node.NodeType.Floor)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void UpdateNodeTypes(Node startNode, Vector2 buildingSize)
    {
        int startX = Mathf.RoundToInt(startNode.gridX - buildingSize.x / 2);
        int startY = Mathf.RoundToInt(startNode.gridY - buildingSize.y / 2);

        for (int x = 0; x < Mathf.CeilToInt(buildingSize.x); x++)
        {
            for (int y = 0; y < Mathf.CeilToInt(buildingSize.y); y++)
            {
                int updateX = startX + x;
                int updateY = startY + y;

                if (updateX >= 0 && updateX < customGrid.gridSizeX && updateY >= 0 && updateY < customGrid.gridSizeY)
                {
                    customGrid.grid[updateX, updateY].nodeType = Node.NodeType.Entity;
                }
            }
        }
    }

    private Vector2 GetBuildingSize(GameObject building)
    {
        // 건물 오브젝트의 크기를 가져와서 타일 단위로 변환
        Renderer renderer = building.GetComponent<Renderer>();
        Vector3 size = renderer.bounds.size;
        return new Vector2(size.x / customGrid.nodeDiameter, size.y / customGrid.nodeDiameter);
    }
}