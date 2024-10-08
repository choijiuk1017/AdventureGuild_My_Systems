using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Data;

public class BuildManager : MonoBehaviour
{
    private Dictionary<int, BuildData> buildData;
    [SerializeField] private List<BuildData> debugBuildData;
    private Dictionary<int, BuildData> buildTileData;
    private Dictionary<int, BuildData> buildFurnitureData;

    private const string buildID = "Build_ID";
    private const string buildName = "Build_Name";
    private const string buildText = "Build_Text";
    private const string prerequisiteFurniture1 = "Prerequisite_Furniture1";
    private const string prerequisiteFurniture2 = "Prerequisite_Furniture2";
    private const string prerequisiteFurniture3 = "Prerequisite_Furniture3";
    private const string bonusStatsTile1 = "Bonus_Stats_Tile_1";
    private const string bonusStatsType1 = "Bonus_Stats_Type_1";
    private const string bonusStatsValue1 = "Bonus_Stats_Value_1";
    private const string bonusStatsTile2 = "Bonus_Stats_Tile_2";
    private const string bonusStatsType2 = "Bonus_Stats_Type_2";
    private const string bonusStatsValue2 = "Bonus_Stats_Value_2";
    private const string buildTax = "Build_Tax";
    
    // Start is called before the first frame update
    void Start()
    {
        InitBuildManager();
    }

    private void InitBuildManager()
    {
        buildData = new Dictionary<int, BuildData>();
        debugBuildData = new List<BuildData>();

        var buildingData = DataParser.Parser("건물");

        foreach (var bData in buildingData)
        {
            var id = DataParser.IntParse(bData[buildID]);
            
            var bd = new BuildData()
            {
                buildName = bData[buildName].ToString(),
                buildText = bData[buildText].ToString(),
                prerequisiteFurniture = DataParser.IntParse(bData[prerequisiteFurniture1]),
                prerequisiteFurniture2 = DataParser.IntParse(bData[prerequisiteFurniture2]),
                prerequisiteFurniture3 = DataParser.IntParse(bData[prerequisiteFurniture3]),
                bonusStatsTile = DataParser.IntParse(bData[bonusStatsTile1]),
                bonusStatsType = DataParser.IntParse(bData[bonusStatsType1]),
                bonusStatsValue = DataParser.IntParse(bData[bonusStatsValue1]),
                bonusStatsTile2 = DataParser.IntParse(bData[bonusStatsTile2]),
                bonusStatsType2 = DataParser.IntParse(bData[bonusStatsType2]),
                bonusStatsValue2 = DataParser.IntParse(bData[bonusStatsValue2]),
                buildTax = DataParser.IntParse(bData[buildTax])
            };
            debugBuildData.Add(bd);
            buildData.Add(id, bd);
        }
    }
}

[Serializable]
public class BuildData
{
    public string buildName;
    public string buildText;
    public int prerequisiteFurniture;
    public int prerequisiteFurniture2;
    public int prerequisiteFurniture3;
    public int bonusStatsTile;
    public int bonusStatsType;
    public int bonusStatsValue;
    public int bonusStatsTile2;
    public int bonusStatsType2;
    public int bonusStatsValue2;
    public int buildTax;
}