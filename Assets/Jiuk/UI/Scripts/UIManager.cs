using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<GameObject> panelPrefabs = new List<GameObject>();
    public Canvas canvas; // 패널이 생성될 캔버스
    public Camera mainCamera;

    private bool isPanelActive = false;
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;

    public void Start()
    {
        mainCamera = Camera.main;
        canvas = GameObject.FindObjectOfType<Canvas>();

        // Raycaster 설정
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        if (raycaster == null)
        {
            raycaster = canvas.gameObject.AddComponent<GraphicRaycaster>();
        }

        pointerEventData = new PointerEventData(null);
    }

    // 패널을 생성하고 활성화하는 함수, 각 패널들을 불러올 버튼의 클릭 함수로 적용 
    public void ShowPanel(int panelIndex)
    {
        // 인덱스에 해당하는 패널 프리팹 가져오기
        if (panelIndex >= 0 && panelIndex < panelPrefabs.Count)
        {
            GameObject panelPrefab = panelPrefabs[panelIndex];

            // 이미 해당 패널이 존재하는지 확인
            GameObject existingPanel = GetExistingPanel(panelPrefab);

            if (existingPanel != null)
            {
                // 이미 존재하는 패널을 활성화
                existingPanel.SetActive(true);
                SetCanvasGroupProperties(existingPanel);
            }
            else
            {
                // 패널을 생성하고 씬에 추가
                GameObject panelInstance = Instantiate(panelPrefab, Vector3.zero, Quaternion.identity);

                // 패널을 캔버스의 자식으로 설정
                panelInstance.transform.SetParent(canvas.transform, false);

                // 생성한 패널을 화면 중앙으로 이동 (예시로, 중앙으로 이동하는 코드)
                panelInstance.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

                // 패널의 CanvasGroup 컴포넌트를 찾아 활성화
                SetCanvasGroupProperties(panelInstance);
            }

            // 패널이 활성화된 상태로 설정
            isPanelActive = true;
        }
    }

    private GameObject GetExistingPanel(GameObject panelPrefab)
    {
        // 캔버스의 자식으로 있는 모든 패널 검사
        foreach (Transform child in canvas.transform)
        {
            if (child.gameObject.name == panelPrefab.name + "(Clone)")
            {
                return child.gameObject;
            }
        }
        return null;
    }

    private void SetCanvasGroupProperties(GameObject panel)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        Canvas panelCanvas = panel.GetComponent<Canvas>();
        if (panelCanvas != null)
        {
            // 가장 위에 그려지도록 sortingOrder 설정
            panelCanvas.overrideSorting = true;
            panelCanvas.sortingOrder = 100; // 큰 값으로 설정
        }
    }

    public void Update()
    {
        if (isPanelActive && Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI())
            {
                Debug.Log("UI 클릭 중");
                return;
            }

            Debug.Log("게임 오브젝트 클릭 막음");
        }
    }

    private bool IsPointerOverUI()
    {
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);
        return results.Count > 0;
    }

    public void ExitButton()
    {
        mainCamera.transform.position = new Vector3(0f, 0f, -10f);
        isPanelActive = false; // 패널 비활성화 상태로 설정
    }
}