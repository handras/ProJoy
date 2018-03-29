using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public HexTile hexTilePrefab;

    HexTile[,] map;

    int width = 15;
    int height = 15;
    
	void Start () {
        map = new HexTile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j< height; j++)
            {
                HexTile h = Instantiate(hexTilePrefab, this.transform);
                map[i, j] = h;
                h.Create(i, j);
            }
        }

	}
	
	void Update () {
		
	}
}
