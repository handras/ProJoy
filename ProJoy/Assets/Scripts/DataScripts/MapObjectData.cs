using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ProJoy/MapObjectData")]
public class MapObjectData : ScriptableObject {

    public int MoveRange;
    public int Cost;
    public GameObject gameobject;
}
