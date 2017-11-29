using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerAnswer : MonoBehaviour {
	public int answerNum;

	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log ("Hello");
		if (other.GetComponent<burgerText> ().optionNumber == answerNum) {
			//Debug.Log ("Correct");
			burgerGame.manager.guess (true);
		} else {
			//Debug.Log ("Wrong");
			burgerGame.manager.guess (false);
		}
	}
}
