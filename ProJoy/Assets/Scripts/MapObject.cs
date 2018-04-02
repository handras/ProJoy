using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject  {

    public int MoveRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class Unit : MapObject
{
    public Unit()
    {
        MoveRange = 2;
    }
}
