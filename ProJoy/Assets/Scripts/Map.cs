using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public HexTile hexTilePrefab;
    
    HexTile[,] map;
    private HexTile selectedTile;

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

    public void SelectTile(HexTile h)
    {
        if(selectedTile == h)
        {
            h.Deselect();
            selectedTile = null;
            return;
        }
        if(selectedTile != null)
        {
            selectedTile.Deselect();
        }
        selectedTile = h;
        h.Select();

        foreach(HexTile nh in GetNeighbours(h))
        {
            if (nh == null)
                continue;
            nh.OnPointerEnter(null);
        }
    }

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
	
	void Update () {
		
	}
}
