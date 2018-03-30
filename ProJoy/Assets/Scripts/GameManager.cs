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
        GameObject mapGO = new GameObject("Map");
        Map map = mapGO.AddComponent<Map>();
        map.mapData = mapData;
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
