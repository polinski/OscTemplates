using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventLog : MonoBehaviour {


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

	void Intro(){

		Debug.Log("INTRO STARTED");
	}

	void Verse(){

		Debug.Log("VERSE STARTED");
	}

	void Chorus(){

		Debug.Log("CHORUS STARTED");
	}

	void Outro(){

		Debug.Log("OUTRO STARTED");
	}


}
