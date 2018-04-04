using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexTile : MonoBehaviour {

    //public GameObject HexagonPrefab, HexagonBorderPrefab;
    public Color HexBaseColor, HexMouseOverColorTint, hexBorderSelectedColor;

    private GameObject hexagon, hexagonBorder;
    private SpriteRenderer hexagonSpriteRenderer, hexagonborderSpriteRenderer;

    private static float height = 2.30862f;
    private static float width = Mathf.Sqrt(3) / 2 * height;
    
    //cubic coordinates
    private int q, r, s;

    private Player _owner;
    public Player Owner {
        get { return _owner; }
        set {
            _owner = value;
            HexBaseColor = value.color;
            hexagonSpriteRenderer.color = HexBaseColor;
        }
    }
    private MapObject _mapObject;
    public MapObject mapObject
    {
        get { return _mapObject; }
        set
        {
            _mapObject = value;
            if (_mapObject != null)
            {
                _mapObject.Prefab.transform.position = getPosition();
            }
        }
    }

    public void Create(int col, int row)
    {
        q = col;
        r = row;
        s = -q - r;
 
        transform.position = getPosition();

        hexagon = transform.Find("Hexagon").gameObject;
        hexagonBorder = transform.Find("Hexagon-border").gameObject;

        hexagonSpriteRenderer = hexagon.GetComponentInChildren<SpriteRenderer>();
        hexagonborderSpriteRenderer = hexagonBorder.GetComponentInChildren<SpriteRenderer>();

        hexagonSpriteRenderer.color = HexBaseColor;
    }

    private Vector2 getPosition()
    {
        return new Vector2(width * (q + r / 2f), height * r * 0.75f);
    }

    public void getMapIndices(out int col, out int row)
    {
        col = q;
        row = r;
    }

    private Vector2Int[] _neighbours;
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
        for (int i = 0; i < 6; i++)
        {
            _neighbours[i] = new Vector2Int(q, r) + neighbourDirections[i];
        }
    }

    void OnTrigger()
    {
        Debug.Log("HexTile triggered");
    }

    public void Select()
    {
        hexagonborderSpriteRenderer.color = hexBorderSelectedColor;
    }

    public void Deselect()
    {
        hexagonborderSpriteRenderer.color = Color.black;
    }

    // call this to indicate this tile is neighbouring to a tile which owner has playerColor
    public void Highlight(Color playerColor)
    {
        if (HexBaseColor.Equals(playerColor))
        {
            hexagonSpriteRenderer.color = Color.Lerp(Color.white, playerColor, 0.8f);
        }
        else
        {
            hexagonSpriteRenderer.color = Color.Lerp(HexBaseColor, playerColor, 0.2f);
        }
    }

    // with no parameter it ends the highlight
    public void Highlight()
    {
        hexagonSpriteRenderer.color = HexBaseColor;
    }

}
