using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCues : MonoBehaviour {

	public List<string> eventCues = new List<string>();
	public List<int> eventLengths = new List<int>();

	private float timer;
	private int eventIndex = 0;

	// Use this for initialization
	void Start () {

		timer = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.E)) RunEvents();

		timer = timer + Time.time;
//		Debug.Log(timer);

	}

	public void RunEvents(){

		if (eventCues.Count != eventLengths.Count) return;
		else {
			timer = 0;
			StartCoroutine(EventScheduler(eventIndex));
		}
	}

	IEnumerator EventScheduler(int index){

		EventManager.TriggerEvent(eventCues[index]);
	
		yield return new WaitForSecondsRealtime(eventLengths[index]);

		eventIndex++;

		if (eventIndex < eventCues.Count) RunEvents();
		else {
			Debug.Log("No More Events");
			eventIndex = 0;
		}

	}
}
