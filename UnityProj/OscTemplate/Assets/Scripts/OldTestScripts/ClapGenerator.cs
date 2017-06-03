using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapGenerator : MonoBehaviour {

	void OnEnable(){

		OscReceiver.onString += GenerateClapPattern;
	}

	void Disable(){

		OscReceiver.onString -= GenerateClapPattern;
	}

	OscCodeSender oscCode;

	public List<string> triggerNumbers = new List<string>();
	public List<string> possibleSteps = new List<string>();


	void Start(){

		oscCode = GameObject.Find("OscIO").GetComponent<OscCodeSender>();

	}
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)){
		//	GenerateClapPattern();
		}
	}

	public void GenerateClapPattern(string address, string instruction){

		if (instruction != "newBeat") return;

		int triggers = Random.Range(0, triggerNumbers.Count-1);
		int steps = Random.Range(0, possibleSteps.Count-1);

		oscCode.SendOsc("/instrument", 2.ToString());
		oscCode.SendOsc("/pattern", 1.ToString());
		oscCode.SendOsc("/triggers", triggerNumbers[triggers]);
		oscCode.SendOsc("/step", possibleSteps[steps]);

	}
}
