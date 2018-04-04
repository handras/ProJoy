using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public MapData mapData;
    
    HexTile[,] map;

    private List<HexTile> _highlightedTiles;
    private HexTile _selectedTile;
    private HexTile SelectedTile
    {
        get { return _selectedTile; }
        set {
            if (_selectedTile != null)
            {
                _selectedTile.Deselect();
                foreach (HexTile ht in _highlightedTiles)
                {
                    ht.Highlight();
                }
            }
            _selectedTile = value;
            if (_selectedTile != null)
            {
                _selectedTile.Select();
                _highlightedTiles = calculateValidMoves(_selectedTile);
            }
        }
    }

    int width = 15;
    int height = 15;
    
	void Start () {
        Debug.Log("map.Start()");
	}
    
    public void GenerateMap() {
        HexTile hexTilePrefab = mapData.hexTilePrefab;
        map = new HexTile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                HexTile h = Instantiate(hexTilePrefab, this.transform);
                map[i, j] = h;
                h.Create(i, j);
            }
        }
    }

    private void NoTileSelectedLogic(HexTile h)
    {
        // can't select a tile which has no movable character
        if (h.mapObject == null || h.mapObject.MoveRange == 0)
        {
            SelectedTile = null;
            return;
        }
        // there wasn't an alredy selected tile, select this
        SelectedTile = h;
    }

    private void TileSelectedLogic(HexTile h)
    {
        // here the current selection is deselected
        if (SelectedTile == h)
        {
            SelectedTile = null;
            return;
        }
        // if the player can occupy do it
        if (OccupyTile(SelectedTile, h))
        {
            SelectedTile = h;
            return;
        }
        // if can't occupy, maybe he intended to deselect
        else
        {
            SelectedTile = null;
            return;
        }
    }

    public void SelectTile(HexTile h)
    {
        if (SelectedTile == null)
        {
            NoTileSelectedLogic(h);
        }
        else
        {
            TileSelectedLogic(h);
        }      
    }

    private List<HexTile> calculateValidMoves(HexTile from)
    {
        List<HexTile> hexes = new List<HexTile>();
        calculateValidMoves(from, from, 0, ref hexes);
        return hexes;
    }
    private void calculateValidMoves(HexTile current, HexTile original, int deepness, ref List<HexTile> results)
    {
        Color color = original.Owner.color;
        MapObject unit = original.mapObject;
        HexTile[] neighbours = GetNeighbours(current);
        for (int i = 0; i < 6; i++)
        {
            HexTile neighbour = neighbours[i];
            if (neighbour == null /*|| results.Contains(neighbour)*/)
                continue;
            neighbour.Highlight(color);
            results.Add(neighbour);
            // recursive call
            if (unit.MoveRange > deepness+1)
            {
                // any unit can only move 1 tile outside of his motherland
                if (neighbour.Owner == original.Owner)
                {
                    calculateValidMoves(neighbour, original, deepness + 1, ref results);
                }
            }
        }
    }

    // returns the 6 neighbours of a tile, some might be null on the edges
    public HexTile[] GetNeighbours(HexTile h)
    {
        HexTile[] neighbours = new HexTile[6];
        Vector2Int[] neighbourCoordinates = h.Neighbours;
        for (int i=0; i<6; i++)
        {
            int ni = neighbourCoordinates[i].x;
            int nj = neighbourCoordinates[i].y;
            try
            {
                neighbours[i] = map[ni, nj];
            } catch (System.IndexOutOfRangeException)
            {
                neighbours[i] = null;
            }
        }
        return neighbours;
    }

    public bool AddMapObject(int i, int j, MapObject mapObject)
    {
        map[i, j].mapObject = mapObject;
        return true;
    }

    // makes the tile belonging to player if he can occupy it
    // this overload is for map initialization, it places a simple unit to this tile
    public bool OccupyTile(int i, int j, Player player, MapObjectData unitData=null)
    {
        HexTile h = map[i, j];
        h.Owner=player;
        if (unitData != null)
        {
            MapObject unit = new MapObject(unitData, h);
            h.mapObject = unit;
        }
        return true;
    }

    // makes the tile belonging to player if the attack is valid 
    // (this means the destination tile is in the highlighted set of tiles)
    public bool OccupyTile(HexTile attackSource, HexTile attackDest)
    {
        if (_highlightedTiles.Contains(attackDest))
        {
            // the source mapObject moves to the dest tile
            if (attackDest.mapObject != null)
            {
                Destroy(attackDest.mapObject.Prefab);
            }
            attackDest.mapObject = attackSource.mapObject;
            attackSource.mapObject = null;
            // tile ownership update
            attackDest.Owner = attackSource.Owner;
            return true;
        }
        else
        {
            return false;
        }
    }
	
}
