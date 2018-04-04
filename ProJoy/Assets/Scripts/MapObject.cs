using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject  {

    public int MoveRange;
    public int Cost;
    public GameObject Prefab;

    public MapObject(MapObjectData data, HexTile hex)
    {
        MoveRange = data.MoveRange;
        Cost = data.Cost;

        Prefab = GameObject.Instantiate(data.gameobject, hex.transform);
    }
}
