using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<GameObject> panelPrefabs = new List<GameObject>();
    public Canvas canvas; // �г��� ������ ĵ����
    public Camera mainCamera;

    private bool isPanelActive = false;
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;

    public void Start()
    {
        mainCamera = Camera.main;
        canvas = GameObject.FindObjectOfType<Canvas>();

        // Raycaster ����
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        if (raycaster == null)
        {
            raycaster = canvas.gameObject.AddComponent<GraphicRaycaster>();
        }

        pointerEventData = new PointerEventData(null);
    }

    // �г��� �����ϰ� Ȱ��ȭ�ϴ� �Լ�, �� �гε��� �ҷ��� ��ư�� Ŭ�� �Լ��� ���� 
    public void ShowPanel(int panelIndex)
    {
        // �ε����� �ش��ϴ� �г� ������ ��������
        if (panelIndex >= 0 && panelIndex < panelPrefabs.Count)
        {
            GameObject panelPrefab = panelPrefabs[panelIndex];

            // �̹� �ش� �г��� �����ϴ��� Ȯ��
            GameObject existingPanel = GetExistingPanel(panelPrefab);

            if (existingPanel != null)
            {
                // �̹� �����ϴ� �г��� Ȱ��ȭ
                existingPanel.SetActive(true);
                SetCanvasGroupProperties(existingPanel);
            }
            else
            {
                // �г��� �����ϰ� ���� �߰�
                GameObject panelInstance = Instantiate(panelPrefab, Vector3.zero, Quaternion.identity);

                // �г��� ĵ������ �ڽ����� ����
                panelInstance.transform.SetParent(canvas.transform, false);

                // ������ �г��� ȭ�� �߾����� �̵� (���÷�, �߾����� �̵��ϴ� �ڵ�)
                panelInstance.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

                // �г��� CanvasGroup ������Ʈ�� ã�� Ȱ��ȭ
                SetCanvasGroupProperties(panelInstance);
            }

            // �г��� Ȱ��ȭ�� ���·� ����
            isPanelActive = true;
        }
    }

    private GameObject GetExistingPanel(GameObject panelPrefab)
    {
        // ĵ������ �ڽ����� �ִ� ��� �г� �˻�
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
            // ���� ���� �׷������� sortingOrder ����
            panelCanvas.overrideSorting = true;
            panelCanvas.sortingOrder = 100; // ū ������ ����
        }
    }

    public void Update()
    {
        if (isPanelActive && Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI())
            {
                Debug.Log("UI Ŭ�� ��");
                return;
            }

            Debug.Log("���� ������Ʈ Ŭ�� ����");
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
        isPanelActive = false; // �г� ��Ȱ��ȭ ���·� ����
    }
}