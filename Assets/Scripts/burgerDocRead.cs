using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class burgerDocRead : MonoBehaviour {
	public string total;
	public string[] entries;

	// Use this for initialization
	void Awake () {
		TextAsset text = Resources.Load("burgerQuestions") as TextAsset;
		total = text.text;
		entries = total.Split ('|');
		burgerGame.manager.assignVariables (entries);
	}
}
