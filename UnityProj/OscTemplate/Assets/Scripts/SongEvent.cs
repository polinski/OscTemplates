using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongEvent {

	public string name;
	public int length;
	public int transpose;
	public enum EventStyle {intro, middle, outro};
	public enum EventType {drone, mountain};
	public EventStyle thisStyle;
	public EventType thisType;
	public int index;
	 
	public SongEvent() {

		index = EventManager.EventIndex;

	}


}
