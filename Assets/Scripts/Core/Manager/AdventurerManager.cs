using System.Collections;
using System.Collections.Generic;
using Core.Unit;
using UnityEngine;

public class AdventurerManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> uniqueAdventurerList;
    [SerializeField] private List<Sprite> normalAdventurerList;

    [SerializeField] private List<Adventure> adventures;
    
    // 길드 명성별 영입되는 모험가 등급 수식 파싱 데이터 필요
    [SerializeField] private float rate; 
    
    // Start is called before the first frame update
    void Start()
    {
        adventures = new List<Adventure>();
    }

    public Sprite GetRandomNormalAdventurer()
    {
        var randomIdx = Random.Range(0, normalAdventurerList.Count);
        return normalAdventurerList[randomIdx];
    }

    public void GetRandomUniqueAdventurer()
    {
        
    }

    public void ExitAdventurer(Adventure adventurer)
    {
        adventures.Add(adventurer);
    }

    public void EnterAdventurer()
    {
        // 몇명 등장할지
        adventures[0].AdventureAI.ChangeState(AdventureStateType.Enter);
        adventures[0].gameObject.SetActive(true);
    }
}
