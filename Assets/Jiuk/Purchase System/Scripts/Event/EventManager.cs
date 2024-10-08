using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Data;

//�̺�Ʈ�� ���� �ٷ�� ��ũ��Ʈ
public class Eventmanager : MonoBehaviour
{

    private Dictionary<int, ItemEvents> eventData;
    [SerializeField]
    private List<ItemEvents> debugData;

    public List<ItemEvents> eventList;

    private const string eventDataTableName = "Event_Master";

    private const string eventID = "Event_ID";
    private const string eventName = "Event_Name";
    private const string eventMainType = "Event_Main_Type";
    private const string eventSubType = "Event_Sub_Type";
    private const string eventDetailType = "Event_Detail_Type";
    private const string eventScript = "Event_Script";
    private const string eventPrecede = "Event_Precede";
    private const string eventProb = "Event_Prob";
    private const string resetEvent = "Reset_Event";

    //public List<Item> itemList;
    //public ItemSetting itemSetting;

    //�ߺ� Ȯ���ϴµ� ����� ���� �̺�Ʈ ����Ʈ
    private HashSet<int> usedEventIndices = new HashSet<int>(); //�ߺ��� ���� ������� �ʱ� ���� HashSet ���

    void Start()
    {

        InitEventData();

        GameObject itemSettingObject = GameObject.Find("ItemManager");

        //itemSetting = itemSettingObject.GetComponent<ItemSetting>();
    }

    private void InitEventData()
    {
        eventData = new Dictionary<int, ItemEvents>();
        debugData = new();

        var events = DataParser.Parser(eventDataTableName);

        foreach (var itemEvent in events)
        {
            var iData = new ItemEvents()
            {
                Event_ID = DataParser.IntParse(itemEvent[eventID]),
                Event_Name = itemEvent[eventName].ToString(),
                Event_Main_Type = DataParser.IntParse(itemEvent[eventMainType]),
                Event_Sub_Type = DataParser.IntParse(itemEvent[eventSubType]),
                Event_Detail_Type = DataParser.IntParse(itemEvent[eventDetailType]),
                Event_Script = itemEvent[eventScript].ToString(),
                Event_Precede = DataParser.IntParse(itemEvent[eventPrecede]),
                Event_Prob = DataParser.IntParse(itemEvent[eventProb]),
                Reset_Event = DataParser.IntParse(itemEvent[resetEvent]),
            };

            debugData.Add(iData);
            eventData.Add(iData.Event_ID, iData);
        }
        eventList = debugData;
    }

    // �̺�Ʈ �߻� �Լ�
    public void TriggerRandomEvent()
    {
        int randomEventIndex = GetRandomAvailableEventIndex();

        if (randomEventIndex != -1)
        {
            ActiveEvent(randomEventIndex);
            MarkEventAsUsed(randomEventIndex);
            CheckResetEvent(randomEventIndex);
        }
    }

    private int GetRandomAvailableEventIndex()
    {
        List<int> availableEventIndices = new List<int>();

        for (int i = 0; i < eventList.Count; i++)
        {
            if (!usedEventIndices.Contains(i))
            {
                availableEventIndices.Add(i);
            }
        }

        if (availableEventIndices.Count == 0)
        {
            // ��� ������ �̺�Ʈ�� ���� ���
            return -1;
        }

        int randomIndex = Random.Range(0, availableEventIndices.Count);
        int randomEventIndex = availableEventIndices[randomIndex];

        // ���õ� �̺�Ʈ�� ���� �̺�Ʈ�� �ʿ�� �ϴ� ���
        if (eventList[randomEventIndex].Event_Precede != 0)
        {
            int precedeIndex = eventList[randomEventIndex].Event_Precede;

            for (int i = 0; i < eventList.Count; i++)
            {
                if (eventList[i].Event_ID == precedeIndex && !usedEventIndices.Contains(i))
                {
                    return i;
                }
            }

        }

        return randomEventIndex;
    }

    private void MarkEventAsUsed(int eventIndex)
    {
        usedEventIndices.Add(eventIndex);
    }

    //���� �̺�Ʈ ���� �Լ�
    private void CheckResetEvent(int eventIndex)
    {
        int resetEventID = eventList[eventIndex].Reset_Event;

        if (resetEventID != 0 )
        {
            for(int i = 0; i < eventList.Count; i++)
            {
                if (eventList[i].Event_ID == resetEventID && usedEventIndices.Contains(i))
                {
                    usedEventIndices.Remove(i);
                }
            }  
        }
    }

    //�̺�Ʈ Ȱ��ȭ �Լ�
    public void ActiveEvent(int eventNum)
    {
        if (eventNum >= 0 && eventNum < debugData.Count)
        {

            Debug.Log(debugData[eventNum].Event_Script);

            //�̺�Ʈ Ÿ�Կ� ���� �з�
            //1 �̸� ����, 2 �̸� ����, 3 �̸� Ư�� �Ƿڷ� �з�
            if (debugData[eventNum].Event_Main_Type == 1)
            {
                float increaseAmount = 1f + debugData[eventNum].Event_Detail_Type / 100f;
                //itemSetting.ChangePrices(increaseAmount, debugData[eventNum].Event_Sub_Type);
            }
            else if (debugData[eventNum].Event_Main_Type == 2)
            {
                float decreaseAmount = 1f - debugData[eventNum].Event_Detail_Type / 100f;
                //itemSetting.ChangePrices(decreaseAmount, debugData[eventNum].Event_Sub_Type);
            }
            else
            {
                Debug.Log("Ư�� �Ƿ�");
            }
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� �̺�Ʈ �ε����Դϴ�." + eventNum);
        }
    }

}
