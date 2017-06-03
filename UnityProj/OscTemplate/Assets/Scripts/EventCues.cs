using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniOSC;
using OSCsharp.Data;
using UnityEngine.UI;

public class EventCues : MonoBehaviour {

	public List<string> eventCues = new List<string>(); // cues that the song will be built from. eventually needs to be automated.
	public List<int> eventLengths = new List<int>(); // lengths for each event in seconds.

	public List<bool> patience = new List<bool>(); // list of bools that this controller watches to activate next event upon completion of the previous one.

	private int eventIndex = 0; // to keep track of the event order.

	public Dictionary<int, SongEvent> eventDictionary = new Dictionary<int, SongEvent>(); // this is where the Event Generators will add their data.

	OscCodeSender oscCodeSender;
	Text log;
	public bool playing;

	public string currentEvent;
	private Coroutine prevEvent = null;

	void OnEnable () {

		EventManager.StartListening ("/fromMax", ReceiveInstructions);

	}

	void OnDisable () {

		EventManager.StopListening ("/fromMax", ReceiveInstructions);

	}


	void Start () {

		// Make bool list based on number of events.
		for (int i = 0; i < eventCues.Count; i++){

			patience.Add(false);
		}

		playing = false;
		oscCodeSender = GameObject.Find("OscIO").GetComponent<OscCodeSender>();
		log = GameObject.Find("EventManagerLog").GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.E)){
			GenerateEvents(eventCues);
		}
		if (Input.GetKeyDown(KeyCode.R)){
			RunEvents();
		}

	}


	// Generate Events based on the eventCue list.

	public void GenerateEvents(List<string> eventTypes){

		EventManager.EventIndex = 0;

		foreach (string s in eventTypes){

			object obj = 0; // a redundant object to pass for the moment and there needs to be a second argument.
			EventManager.TriggerEvent("Generate" + s + "Event", obj); // Trigger relevant event class.
			EventManager.EventIndex++; // add 1 to the event index.

		}

		log.text = "song generated. press r to run.";
	}

	// Run the Events.

	public void RunEvents(){

		// As long as there are as many lengths as there are cues, start the coroutine. This check hopefully will go away once a better way to implement the variables
		// that are passed to each event generator is figured out.

		if (eventCues.Count != eventLengths.Count) return;
		else {
			playing = false;

			if (prevEvent != null) StopCoroutine(prevEvent);
			SongEvent thisEvent;
			eventDictionary.TryGetValue(eventIndex, out thisEvent);
			currentEvent = thisEvent.name;
			StartCoroutine(EventScheduler(eventIndex));
			playing = true;
			prevEvent = StartCoroutine(Logger(currentEvent));
		}
	}

	// This is the coroutine which will be running the song. It fires off the first event to Max then waits while it is running. When max switches the event's bool to
	// 'true', the coroutine fires off the next event and so on until there are no more left.

	IEnumerator EventScheduler(int index){

		SongEventToOsc(index);

		while (!patience[index]){
			yield return new WaitForEndOfFrame();
		}
		StopCoroutine(prevEvent);
		log.text = "end.";
		eventIndex++;
		if (eventIndex < eventCues.Count) RunEvents();
		else {
			oscCodeSender.SendOsc("Max_DecompMain", "/transport " + "STOP");
			eventIndex = 0;
		}
	}

	IEnumerator Logger(string str){

		string[] dots = {".",". .",". . .",". . . .",". . . . ."};

		int count = 0;

		while (count < dots.Length){


			log.text = str +" is playing" + dots[count];
			yield return new WaitForSeconds(0.4f);
			count++;
			if (count == dots.Length) count = 0;
		}

		yield return null;

	}

	void SongEventToOsc(int eventIndex){

		SongEvent thisEvent;
		eventDictionary.TryGetValue(eventIndex, out thisEvent);
		if (thisEvent != null){
			
			oscCodeSender.AddToBundle("/max", "clear");
			oscCodeSender.AddToBundle("/index", thisEvent.index);
			oscCodeSender.AddToBundle("/name", thisEvent.name);
			oscCodeSender.AddToBundle("/type", thisEvent.thisType.ToString());
			oscCodeSender.AddToBundle("/style", thisEvent.thisStyle.ToString());
			oscCodeSender.AddToBundle("/length", thisEvent.length);
			oscCodeSender.AddToBundle("/transpose", thisEvent.transpose);
			oscCodeSender.AddToBundle("/max", "done");
			oscCodeSender.SendBundle();

		}
	}

	void ReceiveInstructions(object obj){

		OscPacket msg = obj as OscPacket;
		Debug.Log("MSG: " + msg);
		string instruction = msg.Data[0].ToString();

		if (instruction == "index"){

			int ix;
			if (int.TryParse(msg.Data[1].ToString(), out ix));
				if (ix != null) patience[ix] = true;
		}
	}
}
