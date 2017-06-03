using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KikGenerator : MonoBehaviour {

	void OnEnable(){

		// subscribe to the OSC RECEIVER delegate broadcasts.

		OscReceiver.onString += GenerateKickPattern;

	}

	void Disable(){

		OscReceiver.onString -= GenerateKickPattern;
	}

	OscCodeSender oscCode;
	public List<string> triggerNumbers = new List<string>();


	void Start(){

		oscCode = GameObject.Find("OscIO").GetComponent<OscCodeSender>();

	}
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)){
			//GenerateKickPattern();
		}
	}

	public void GenerateKickPattern(string address, string instruction){

		if (instruction != "newBeat") return;

		int triggers = Random.Range(0, triggerNumbers.Count-1);

		oscCode.SendOsc("/instrument", 0.ToString());
		oscCode.SendOsc("/pattern", 1.ToString());
		oscCode.SendOsc("/triggers", triggerNumbers[triggers]);

	}
}
