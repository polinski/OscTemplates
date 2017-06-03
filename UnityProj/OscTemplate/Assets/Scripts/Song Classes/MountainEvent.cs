using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainEvent : SongEvent {


	public MountainEvent (int _length, int _transpose, EventStyle style){

		length = _length;
		transpose = _transpose;
		thisStyle = style;
		name = "Mountain_" + EventManager.EventIndex.ToString();
		thisType = EventType.mountain;
		MonoBehaviour.print("MADE A MOUNTAIN EVENT:" + name);
		int test = GeneratingAValue();

	}

	private int GeneratingAValue(){

		int ran = Random.Range(0, 666);
		MonoBehaviour.print("RUNNING MOUNTAIN METHOD ON INSTANTIATION: " + ran);
		return ran;
	}

}
