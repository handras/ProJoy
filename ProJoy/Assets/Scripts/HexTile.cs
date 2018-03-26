using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexTile {
    
    private GameObject hexagon, hexagonBorder;
    private SpriteRenderer hexagonSpriteRenderer, hexagonborderSpriteRenderer;

    private static float height = 2.30862f;
    private static float width = Mathf.Sqrt(3) / 2 * height;

    //cubic coordinates
    private int q, r, s;

    private Color baseColor = new Color(1f, 1f, 1f) ;
    private Color mouseOverColor = new Color(0.2f, 0.2f, 0.2f);    
    
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

        hexagonSpriteRenderer = hexagon.GetComponentInChildren<SpriteRenderer>();
        hexagonborderSpriteRenderer = hexagonBorder.GetComponentInChildren<SpriteRenderer>();
    }

    void OnTrigger()
    {
        Debug.Log("HexTile triggered");
    }

    public void PointerEnter() {
        hexagonSpriteRenderer.color = mouseOverColor;
    }

    public void PointerExit()
    {
        hexagonSpriteRenderer.color = baseColor;
    }
}
