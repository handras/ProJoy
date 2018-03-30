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

    private Vector2Int[]  _neighbours;
    public Vector2Int[] Neighbours
    {
        get
        {
            if (_neighbours == null)
                calculateNeighours();
            return _neighbours;
        }
        private set { }
    }

    private readonly static Vector2Int[] neighbourDirections = {
        new Vector2Int(+1,  0), new Vector2Int(+1, -1), new Vector2Int( 0, -1),
        new Vector2Int(-1,  0), new Vector2Int(-1, +1), new Vector2Int( 0, +1)
    };
    private void calculateNeighours()
    {
        _neighbours = new Vector2Int[6];
        for(int i = 0; i<6; i++)
        {
            _neighbours[i] = new Vector2Int(q, r) + neighbourDirections[i];
        }
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
