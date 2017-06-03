using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniOSC;

public class EventLog : MonoBehaviour {

	OscCodeSender oscCode;

	void OnEnable () {

		EventManager.StartListening ("intro", Intro);
		EventManager.StartListening ("verse", Verse);
		EventManager.StartListening ("chorus", Chorus);
		EventManager.StartListening ("outro", Outro);
	}

	void OnDisable () {

		EventManager.StopListening ("intro", Intro);
		EventManager.StopListening ("verse", Verse);
		EventManager.StopListening ("chorus", Chorus);
		EventManager.StopListening ("outro", Outro);

	}

	void Start(){

		oscCode = GameObject.Find("OscIO").GetComponent<OscCodeSender>();

	}

	void Intro(object obj){

		Debug.Log("INTRO STARTED" + EventManager.EventIndex);
		EventManager.EventIndex++;
		oscCode.SendOsc("/Max_DecompMain", "INTRO STARTED");
	}

	void Verse(object obj){

		Debug.Log("VERSE STARTED"+ EventManager.EventIndex);
		EventManager.EventIndex++;
		oscCode.SendOsc("/Max_DecompMain", "VERSE STARTED");
	}

	void Chorus(object obj){

		Debug.Log("CHORUS STARTED"+ EventManager.EventIndex);
		EventManager.EventIndex++;
		oscCode.SendOsc("/Max_DecompMain", "CHORUS STARTED");
	}

	void Outro(object obj){

		Debug.Log("OUTRO STARTED"+ EventManager.EventIndex);
		EventManager.EventIndex++;
		oscCode.SendOsc("/Max_DecompMain", "OUTRO STARTED");
	}


}
