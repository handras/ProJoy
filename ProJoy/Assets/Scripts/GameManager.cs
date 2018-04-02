using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PlayerData playerData;
    public MapData mapData;

    List<Player> players;
	
	void Start () {
        players = new List<Player>();
        addPlayer();
        addPlayer();

        // name is important as other scripts search it
        Debug.Log("creating map GO");
        GameObject mapGO = new GameObject("Map");
        Debug.Log("adding map component");
        Map map = mapGO.AddComponent<Map>();
        map.mapData = mapData;
        map.GenerateMap();

        Debug.Log("players occupying");
        map.OccupyTile(2, 2, players[0]);
        map.OccupyTile(9, 9, players[1]);
    }
	
	void Update () {
		
	}

    public Player addPlayer()
    {
        Player newPlayer = gameObject.AddComponent<Player>();
        newPlayer.color = playerData.PlayerColors[players.Count];
        players.Add(newPlayer);
        return newPlayer;
    }
}
