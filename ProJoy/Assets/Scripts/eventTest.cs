using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class eventTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer enter! in event test");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exit! in event test");

    }

    // Use this for initialization
    void Start () {
        GameObject hex = GetComponentInChildren<SpriteRenderer>().gameObject;
        Debug.Log(hex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
