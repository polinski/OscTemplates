using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HhGenerator : MonoBehaviour {

	OscCodeSender oscCode;
	public List<string> triggerNumbers = new List<string>();
	public List<string> speeds = new List<string>();
	public List<string> possibleSteps = new List<string>();

	void OnEnable(){

		OscReceiver.onString += GenerateHHPattern;
	}

	void Disable(){

		OscReceiver.onString -= GenerateHHPattern;
	}

	void Start(){

		oscCode = GameObject.Find("OscIO").GetComponent<OscCodeSender>();

	}
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)){
	//		GenerateHHPattern();
		}
	}

	public void GenerateHHPattern(string address, string instruction){

		if (instruction != "newBeat") return;

		int triggers = Random.Range(0, triggerNumbers.Count-1);
		int speed = Random.Range(0, speeds.Count-1);
		int steps = Random.Range(0, possibleSteps.Count-1);

		oscCode.SendOsc("/instrument", 1.ToString());
		oscCode.SendOsc("/pattern", 1.ToString());
		oscCode.SendOsc("/triggers", triggerNumbers[triggers]);
		oscCode.SendOsc("/speed", speeds[speed]);
		oscCode.SendOsc("/step", possibleSteps[steps]);

	}
}
