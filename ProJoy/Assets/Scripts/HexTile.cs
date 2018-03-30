using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    //public GameObject HexagonPrefab, HexagonBorderPrefab;
    public Color HexBaseColor, HexMouseOverColorTint, hexBorderSelectedColor;

    private GameObject hexagon, hexagonBorder;
    private SpriteRenderer hexagonSpriteRenderer, hexagonborderSpriteRenderer;

    private static float height = 2.30862f;
    private static float width = Mathf.Sqrt(3) / 2 * height;

    //cubic coordinates
    private int q, r, s;    
    
    public void Create(int col, int row)
    {
        q = col;
        r = row;
        s = -q - r;

        Vector2 pos =  new Vector2(width*(q+r/2f), height*r*0.75f);
        transform.position = pos;

        hexagon = transform.Find("Hexagon").gameObject;
        hexagonBorder = transform.Find("Hexagon-border").gameObject;

        hexagonSpriteRenderer = hexagon.GetComponentInChildren<SpriteRenderer>();
        hexagonborderSpriteRenderer = hexagonBorder.GetComponentInChildren<SpriteRenderer>();

        hexagonSpriteRenderer.color = HexBaseColor;
    }

    public void getMapIndices(out int col, out int row)
    {
        col = q;
        row = r;
    }

    void OnTrigger()
    {
        Debug.Log("HexTile triggered");
    }

    public void OnPointerEnter(PointerEventData ed) {
        Debug.Log("HexTile OnPointerEnter event");
        hexagonSpriteRenderer.color = Color.Lerp(HexBaseColor, HexMouseOverColorTint, 0.5f);
    }

    public void OnPointerExit(PointerEventData ed)
    {
        Debug.Log("HexTile OnPointerExit event");
        hexagonSpriteRenderer.color = HexBaseColor;
    }

    public void Select()
    {
        hexagonborderSpriteRenderer.color = hexBorderSelectedColor;
    }

    public void Deselect()
    {
        hexagonborderSpriteRenderer.color = Color.black;
    }
}
