using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventGenerator : MonoBehaviour {

	Dictionary<int, SongEvent> eventDict;


	void OnEnable () {

		EventManager.StartListening ("GenerateDroneEvent", GenerateDroneEvent);
		EventManager.StartListening ("GenerateMountainEvent", GenerateMountainEvent);

		eventDict = GetComponent<EventCues>().eventDictionary;

	}

	void OnDisable () {

		EventManager.StopListening ("GenerateDroneEvent", GenerateDroneEvent);
		EventManager.StopListening ("GenerateMountainEvent", GenerateMountainEvent);

	}

	void GenerateDroneEvent(object obj){

		Debug.Log("GENERATING DRONE EVENT");
		DroneEvent thisDrone = new DroneEvent(1000, 0, DroneEvent.EventStyle.intro);
		eventDict.Add(EventManager.EventIndex, thisDrone);

	}

	void GenerateMountainEvent(object obj){

		Debug.Log("GENERATING MOUNTAIN EVENT");
		MountainEvent thisMountain = new MountainEvent(2000, 0, MountainEvent.EventStyle.outro);
		eventDict.Add(EventManager.EventIndex, thisMountain);
	}

}
