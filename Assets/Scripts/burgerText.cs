using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerText : MonoBehaviour {
	private GameObject shadow;
	public string text;
	public int optionNumber;
	private bool dragging;
	private float xOffset;
	private float yOffset;
	private Vector3 restPos;
	public AudioSource line;
	public int rowLimit;
	private Vector3 clickPos;
	public TextMesh textMesh;
	// Use this for initialization
	void Start () {
		shadow = transform.GetChild (0).gameObject;
		shadow.SetActive (false);
		restPos = burgerGame.manager.optionPos [optionNumber].position;
		transform.position = restPos;
		line = GetComponent<AudioSource> ();
		textMesh = GetComponent<TextMesh> ();
		wordWrap();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown(){
		dragging = true;
		shadow.GetComponent<TextMesh> ().text = GetComponent<TextMesh> ().text;
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

	public void wordWrap(){
		string builder = "";
		textMesh.text = "";
		string[] parts = text.Split (' ');
		int charCount = 0;
		for (int i = 0; i < parts.Length; i++) {
			textMesh.text += parts [i] + " ";
			charCount += parts [i].Length;
			if (charCount > rowLimit) {
				textMesh.text = builder.TrimEnd () + System.Environment.NewLine + parts [i] + " ";
				charCount = 0;
			}
			builder = textMesh.text;
		}
	}
		
}
