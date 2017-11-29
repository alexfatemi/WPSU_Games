using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class burgerGame : MonoBehaviour {
	public static burgerGame manager;
	private int totalQuestions;
	public burgerQuestion nullQuestion;
	public burgerQuestion[] questions;
	private int qNum = 0;
	public burgerText[] optionText;
	public Transform[] optionPos;
	public BoxCollider targetBox;
	public burgerAnswer answer;
	private Animator anim;
	public AudioClip[] clips;
	private AudioSource sound;
	private BoxCollider[] options;
	public GameObject instructions;
	public Text startText;
	private int numCorrect = 0;
	private int numDone = 0;
	public Text correctNumber;
	public Text doneNumber;
	// Use this for initialization
	void Awake () {
		if (manager == null) {
			manager = this;
		} else if (manager != this) {
			Destroy (gameObject);
		}
		optionPos = GetComponentsInChildren<Transform> ();
		anim = GetComponentInChildren<Animator> ();
		sound = GetComponent<AudioSource> ();
		options = GetComponentsInChildren<BoxCollider> ();
		foreach (BoxCollider c in options)
			c.enabled = false;
		correctNumber.text = numCorrect.ToString ();
		doneNumber.text = numDone.ToString ();
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
		options = GetComponentsInChildren<BoxCollider> ();
		foreach (BoxCollider c in options)
			c.enabled = true;
		sound.Stop ();
	}

	public void pauseGame(){
		instructions.SetActive (true);
		options = GetComponentsInChildren<BoxCollider> ();
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
		yield return new WaitForSeconds (1f);
		numCorrect++;
		numDone++;
		correctNumber.text = numCorrect.ToString ();
		doneNumber.text = numDone.ToString ();
		qNum += 1;
		if (qNum > questions.Length-1)
			qNum = 0;
		reloadQuestion (questions [qNum]);
		yield return null;
	}

	private IEnumerator wrong() {
		anim.SetBool ("right", false);
		anim.SetTrigger ("try");
		sound.clip = clips [1];
		sound.Play ();
		yield return new WaitForSeconds (0.5f);
		numDone++;
		doneNumber.text = numDone.ToString ();
		qNum += 1;
		if (qNum > questions.Length-1)
			qNum = 0;
		reloadQuestion (questions [qNum]);
		yield return null;
	}

	public void assignVariables (string[] variables){
		StartCoroutine (assignVariable (variables));
	}

	private IEnumerator assignVariable(string[] variables) {
		totalQuestions = int.Parse(variables [0]);
		questions = new burgerQuestion[totalQuestions];
		yield return new WaitForEndOfFrame();
		int index = 1;
		int q = 0;
		while (q < totalQuestions) {
			questions [q] = new burgerQuestion();
			questions [q].options = new string[3];
			questions [q].clips = new string[3];
			int optionNum = 0;
			while (optionNum < 3) {
				questions [q].options [optionNum] = variables [index];
				index++;
				questions[q].clips[optionNum] = variables [index];
				index++;
				optionNum++;
			}
			questions[q].answer = int.Parse (variables [index]);
			index++;
			q++;
		}
		reloadQuestion (questions [0]);
		yield return null;
	}

	public void reloadQuestion (burgerQuestion question){
		int optionNum = 0;
		while (optionNum < 3) {
			optionText [optionNum].text = question.options[optionNum];
			optionText [optionNum].wordWrap ();
			optionText [optionNum].line.clip = Resources.Load<AudioClip> ("BurgerLines/" + question.clips[optionNum]);
			optionNum++;
		}
		answer.answerNum = question.answer;
	}
}
