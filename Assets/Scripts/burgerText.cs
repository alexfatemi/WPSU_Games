using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerText : MonoBehaviour {
	private GameObject shadow;
	public int optionNumber;
	private bool dragging;
	private float xOffset;
	private float yOffset;
	private Vector3 restPos;
	private AudioSource line;
	private Vector3 clickPos;
	// Use this for initialization
	void Awake () {
		shadow = transform.GetChild (0).gameObject;
		shadow.SetActive (false);
		restPos = burgerGame.manager.optionPos [optionNumber].position;
		transform.position = restPos;
		line = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		dragging = true;
		shadow.SetActive (true);
		line.Play ();
		clickPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
	void OnMouseDrag(){
		Vector3 newClickPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		xOffset = clickPos.x - newClickPos.x;
		yOffset = clickPos.y - newClickPos.y;
		Vector3 newPos = restPos;
		newPos.x -= xOffset;
		newPos.y -= yOffset;
		transform.position = newPos;
	}
	void OnMouseUp(){
		dragging = false;
		shadow.SetActive (false);
		iTween.MoveTo (gameObject, restPos, 0.5f);
	}
		
}
