using System.Collections.Generic;
using UnityEngine;

public enum DesireType
{
    //식욕, 수면욕, 안전욕, 성장욕
    Appetite, SleepDesire, SafetyNeeds, ImprovementNeeds, None
}

public class Desire : MonoBehaviour
{
    private float appetite;
    private float sleepDesire;
    private float safetyNeeds;
    private float improvementNeeds;
    
    private float appetiteRate;
    private float sleepDesireRate;
    private float safetyNeedsRate;
    private float improvementNeedsRate;

    [SerializeField] private DesireType currentNeedDesire;

    [SerializeField]private Dictionary<DesireType, float> desires;
    [SerializeField]private Dictionary<DesireType, float> desireRates;
    [SerializeField]private Dictionary<DesireType, float> desireThresholds;
    // Unity

    // Start is called before the first frame update
    void Start()
    {
        appetite = 100;
        sleepDesire = 100;
        safetyNeeds = 100;
        improvementNeeds = 100;

        appetiteRate = 0.004f;
        sleepDesireRate = 0.07f;
        safetyNeedsRate = 0.006f;
        improvementNeedsRate = 0.007f;

        desires = new Dictionary<DesireType, float>
        {
            { DesireType.Appetite, appetite },
            { DesireType.SleepDesire, sleepDesire },
            { DesireType.SafetyNeeds, safetyNeeds },
            { DesireType.ImprovementNeeds, improvementNeeds }
        };

        desireRates = new Dictionary<DesireType, float>
        {
            { DesireType.Appetite, appetiteRate },
            { DesireType.SleepDesire, sleepDesireRate },
            { DesireType.SafetyNeeds, safetyNeedsRate },
            { DesireType.ImprovementNeeds, improvementNeedsRate }
        };

        desireThresholds = new Dictionary<DesireType, float>
        {
            { DesireType.Appetite, 60 },
            { DesireType.SleepDesire, 60 },
            { DesireType.SafetyNeeds, 50 },
            { DesireType.ImprovementNeeds, 40 }
        };

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDesires();
        CheckCurrentNeed();
        //LogDesires();
    }

    public DesireType GetCurrentNeedDesire()
    {
        return currentNeedDesire;
    }

    private void UpdateDesires()
    {
        var keys = new List<DesireType>(desires.Keys);

        foreach (var desire in keys)
        {
            desires[desire] = Mathf.Max(0, desires[desire] - desires[desire] * Time.deltaTime * desireRates[desire]);
        }
    }

    private void CheckCurrentNeed()
    {
        DesireType lowestDesire = DesireType.None;
        float lowestDifference = float.MaxValue;

        foreach (var desire in desires.Keys)
        {
            float difference = desires[desire] - desireThresholds[desire];
            if (difference <= 0)
            {
                currentNeedDesire = desire;
                Debug.Log($"Current Need Desire: {currentNeedDesire}");
                return;
            }
            else if (difference < 30 && difference < lowestDifference)
            {
                lowestDesire = desire;
                lowestDifference = difference;
            }
        }

        if (lowestDesire != DesireType.None)
        {
            currentNeedDesire = lowestDesire;
            Debug.Log($"Current Need Desire: {currentNeedDesire}");
        }
        else
        {
            currentNeedDesire = DesireType.None;
            Debug.Log("Current Need Desire: None");
        }
    }

    private void LogDesires()
    {
        Debug.Log($"Appetite: {desires[DesireType.Appetite]}");
        Debug.Log($"SleepDesire: {desires[DesireType.SleepDesire]}");
        Debug.Log($"SafetyNeeds: {desires[DesireType.SafetyNeeds]}");
        Debug.Log($"ImprovementNeeds: {desires[DesireType.ImprovementNeeds]}");
    }

    public void IncreaseAppetite(int amount)
    {
        desires[DesireType.Appetite] += amount;
    }

    public void IncreaseSleepDesire(int amount)
    {
        desires[DesireType.SleepDesire] += amount;
    }

    public void IncreaseSafetyNeeds(int amount)
    {
        desires[DesireType.SafetyNeeds] += amount;
    }

    public void IncreaseImprovementNeeds(int amount)
    {
        desires[DesireType.ImprovementNeeds] += amount;
    }
}
