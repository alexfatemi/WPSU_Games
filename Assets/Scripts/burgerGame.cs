using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class burgerGame : MonoBehaviour {
	public static burgerGame manager;
	public GameObject[] questions;
	private int qNum = 0;
	public Transform[] optionPos;
	public BoxCollider targetBox;
	private Animator anim;
	private GameObject question;
	public AudioClip[] clips;
	private AudioSource sound;
	private BoxCollider[] options;
	public GameObject instructions;
	public Text startText;
	// Use this for initialization
	void Awake () {
		if (manager == null) {
			manager = this;
		} else if (manager != this) {
			Destroy (gameObject);
		}
		optionPos = GetComponentsInChildren<Transform> ();
		question = Instantiate (questions [qNum], Vector3.zero, Quaternion.identity);
		anim = question.GetComponentInChildren<Animator> ();
		sound = GetComponent<AudioSource> ();
		options = question.GetComponentsInChildren<BoxCollider> ();
		foreach (BoxCollider c in options)
			c.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			targetBox.enabled = false;
		} else if (!targetBox.enabled)
			targetBox.enabled = true;
	}

	public void startGame(){
		startText.text = "Back";
		instructions.SetActive (false);
		options = question.GetComponentsInChildren<BoxCollider> ();
		foreach (BoxCollider c in options)
			c.enabled = true;
		sound.Stop ();
	}

	public void pauseGame(){
		instructions.SetActive (true);
		options = question.GetComponentsInChildren<BoxCollider> ();
		foreach (BoxCollider c in options)
			c.enabled = false;
	}

	public void instruct(){
		sound.clip = clips [2];
		sound.Play ();
	}

	public void guess(bool answer) {
		if (answer)
			StartCoroutine ("correct");
		else
			StartCoroutine ("wrong");	
	}

	private IEnumerator correct () {
		anim.SetBool ("right", true);
		anim.SetTrigger ("try");
		sound.clip = clips [0];
		sound.Play ();
		yield return new WaitForSeconds (1.5f);
		qNum += 1;
		if (qNum > questions.Length-1)
			qNum = 0;
		Destroy (question);
		question = Instantiate (questions [qNum], Vector3.zero, Quaternion.identity);
		anim = question.GetComponentInChildren<Animator> ();
		yield return null;
	}

	private IEnumerator wrong() {
		anim.SetBool ("right", false);
		anim.SetTrigger ("try");
		sound.clip = clips [1];
		sound.Play ();
		yield return new WaitForSeconds (0.5f);
		qNum += 1;
		if (qNum > questions.Length-1)
			qNum = 0;
		Destroy (question);
		question = Instantiate (questions [qNum], Vector3.zero, Quaternion.identity);
		anim = question.GetComponentInChildren<Animator> ();
		yield return null;
	}

}
