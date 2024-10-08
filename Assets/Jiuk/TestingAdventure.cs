using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Algorithm;

public class TestingAdventure : MonoBehaviour
{
    //�� �ڵ���� �÷��̾��� ��ġ�� ������ 
    public int willingness = 100;
    public int maxWillingness = 100;

    public int currentHp;
    public int maxHp = 3000;

    public int currentMp;
    public int maxMp = 3000;

    public int money;

    public int equipmentValue; //�׽�Ʈ��
    public int equipmentDurability; //�׽�Ʈ��
    public int maxEquipmentDurability;
  

    public bool isCompleteRequest;

    public bool isInBuilding;

    public bool isSetDestination  = false;

    public GameObject destination; // ������

    [SerializeField]
    private bool isMoving = false; // �̵� ������ ����


    private Vector2 currentDestination;


    Rigidbody2D rigid;

    private List<int> remainingBuildingIndices = new List<int> { 0, 1, 2, 3, 4, 5 };


    PathFinding pathFinding;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        pathFinding = GetComponent<PathFinding>();

        destination = this.gameObject;

        StartCoroutine(DecreaseWillpowerRoutine());

        
    }

    void Update()
    {
        if (!isInBuilding && !isSetDestination)
        {
            UpdateDestination();
        }
        if (!isInBuilding && destination != null)
        {
            StartMovingToDestination();
        }
    }

    void StartMovingToDestination()
    {
        currentDestination = destination.transform.position;

        isMoving=true;

       // pathFinding.StartFindPath(this.transform.position, currentDestination);
    }

    void FixedUpdate()
    {
        //FixedUpdate �������� Rigidbody2D�� ����� ���� ��� �̵��� ����
        //if (isMoving && pathFinding.wayQueue.Count > 0)
        //{
        //    var dir = pathFinding.wayQueue.Peek() - (Vector2)this.transform.position;
        //    rigid.velocity = dir.normalized * pathFinding.moveSpeed * Time.fixedDeltaTime;
        //    if (Vector2.Distance(this.transform.position, pathFinding.wayQueue.Peek()) < 0.1f)
        //    {
        //        pathFinding.wayQueue.Dequeue();
        //    }
        //}
    }

    private IEnumerator DecreaseWillpowerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f); 
            willingness -= 10; // ���� ����
        }

        
    }

    void UpdateDestination()
    {
        isSetDestination = true;
        if (currentHp < maxHp || currentMp < maxMp)
        {
            // ü���̳� ������ �ִ밡 �ƴ� ���, ������ �������� ����
            destination = FindBuilding("TempleEntrance");
        }
        else if (willingness < maxWillingness)
        {
            // �������� �ִ밡 �ƴ� ���, ��Ŀ���� �������� ����
            destination = FindBuilding("CircusEntrance");
        }
        else
        {
            SelectRandomDestination();
        }
    }

    void SelectRandomDestination()
    {
        if(remainingBuildingIndices.Count == 0)
        {
            remainingBuildingIndices = new List<int> { 0, 1, 2, 3, 4, 5 };
        }

        int randomIndex = Random.Range(0, remainingBuildingIndices.Count);
        int selectedBuildingIndex = remainingBuildingIndices[randomIndex];
        remainingBuildingIndices.RemoveAt(randomIndex);

        switch (selectedBuildingIndex)
        {
            case 0:
                destination = FindBuilding("SmithyEntrance");
                break;
            case 1:
                destination = FindBuilding("InnEntrance");
                break;
            case 2:
                destination = FindBuilding("PotionMarketEntrance");
                break;
            case 3:
                destination = FindBuilding("TrainingCenterEntrance");
                break;
            case 4:
                destination = FindBuilding("LibraryEntrance");
                break;
            case 5:
                destination = FindBuilding("CentralSquareEntrance");
                break;
            default:
                Debug.LogError("Invalid random building index!");
                break;
        }
    }

    public GameObject FindBuilding(string buildingName)
    {
        GameObject findBuilding = GameObject.Find(buildingName);

        
        if(findBuilding == null)
        {
            Debug.LogError("�ǹ� ����");
        }

        return findBuilding;

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Building"))
        {
            // �÷��̾ �ǹ��� ���ٸ� 'isInBuilding'�� true�� ����
            isInBuilding = true;

            isMoving = false;

            rigid.velocity = Vector2.zero;

            pathFinding.StopAllCoroutines();

        }
    }

}
