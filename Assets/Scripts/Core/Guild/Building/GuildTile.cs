using System.Collections;
using System.Collections.Generic;
using Core.Guild.Building;
using UnityEngine;

public class GuildTile : MonoBehaviour
{
    [SerializeField] private Tile[,] tiles;
    [SerializeField] private Vector2Int guildSize;
    
    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[guildSize.x, guildSize.y];
        
        for(int i = 0; i < guildSize.x; i++)
        {
            for (int j = 0; j < guildSize.y; j++)
            {
                tiles[i, j] = new Tile(i, j);
            }
        }
    }

    public void SetTile(Vector2Int tilePos)
    {
        tiles[tilePos.x, tilePos.y].isEmpty = false;
    }

    public Tile GetTile(Vector2Int tilePos)
    {
        return tiles[tilePos.x, tilePos.y];
    }

    public bool IsEmptyTile(Vector2Int tilePos)
    {
        Debug.Log(tiles[tilePos.x, tilePos.y].isEmpty);
        
        return tiles[tilePos.x, tilePos.y].isEmpty;
    }
}

public class TileData
{
    // 타일? 오브젝트 타입 3x4 타일일때 3x4 만큼의 건설 or 철거    처리필요위해 사용
}
