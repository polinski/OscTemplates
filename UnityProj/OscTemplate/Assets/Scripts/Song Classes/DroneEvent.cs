using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEvent: SongEvent{


	public DroneEvent (int _length, int _transpose, EventStyle style){

		length = _length;
		transpose = _transpose;
		thisStyle = style;
		name = "Drone_" + EventManager.EventIndex.ToString();
		thisType = EventType.drone;
		MonoBehaviour.print("MADE A DRONE EVENT:" + name);
		int test = GeneratingAValue();


	}

	private int GeneratingAValue(){

		int ran = Random.Range(0, 666);
		MonoBehaviour.print("RUNNING METHOD ON INSTANTIATION: " + ran);
		return ran;
	}

}
