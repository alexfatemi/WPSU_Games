using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerAnswer : MonoBehaviour {
	public int answer;
	// Use this for initialization
	void Awake () {
		burgerGame.manager.targetBox = GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log ("Hello");
		if (other.GetComponent<burgerText> ().optionNumber == answer) {
			//Debug.Log ("Correct");
			burgerGame.manager.guess (true);
		} else {
			//Debug.Log ("Wrong");
			burgerGame.manager.guess (false);
		}
		Destroy (other.gameObject);
	}
}
