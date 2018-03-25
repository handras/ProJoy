using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject HexagonPrefab;
    public GameObject HexagonBorderPrefab;

    HexTile[,] map;

    int width = 15;
    int height = 15;
    
	void Start () {
        map = new HexTile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j< height; j++)
            {
                HexTile h = new HexTile(i, j);
                map[i, j] = h;
                h.Create(HexagonPrefab, HexagonBorderPrefab, this);
            }
        }

	}
	
	void Update () {
		
	}
}
