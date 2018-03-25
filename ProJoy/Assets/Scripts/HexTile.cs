using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile {
    
    private GameObject hexagon, hexagonBorder;
    
    private static float height = 2.30862f;
    private static float width = Mathf.Sqrt(3) / 2 * height;

    //cubic coordinates
    private int q, r, s;
    
	public HexTile(int col, int row) {
        q = col;
        r = row;
        s = -q - r;        
	}

    public void Create(GameObject HexagonPrefab, GameObject HexagonBorderPrefab, Map map)
    {
        Vector2 pos =  new Vector2(width*(q+r/2f), height*r*0.75f);
        hexagon = Object.Instantiate(HexagonPrefab, pos, Quaternion.identity, map.transform);
        hexagonBorder = Object.Instantiate(HexagonBorderPrefab, pos, Quaternion.identity, map.transform);
    }


	
}
