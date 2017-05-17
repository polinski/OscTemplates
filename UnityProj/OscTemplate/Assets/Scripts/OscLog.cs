using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscLog : MonoBehaviour {

	private Text log; // text field.
	public int maxLines; // maximum lines to display

	void OnEnable(){

		// subscribe to the OSC RECEIVER delegate broadcasts.

		OscReceiver.onString += LogString;
		OscReceiver.onFloat += LogFloat;
		OscReceiver.onInt += LogInt;
	}

	void Disable(){

		// unsubscribe to the OSC RECEIVER delegate broadcasts if disabled.
		OscReceiver.onString -= LogString;
		OscReceiver.onFloat -= LogFloat;
		OscReceiver.onInt -= LogInt;
	}

	void Start(){

		log = GameObject.Find("OscStream").GetComponent<Text>(); // access the relevant text field.
		log.text = ""; // clear the text field.

	}
		
	// if data received is a string.
	void LogString(string address, string data){

		string logData = "> " + address + " > " + data;
		LogData(logData);
	}

	// if data received is a float.
	void LogFloat(string address, float data){

		string logData = "> " + address + " > " + data.ToString();
		LogData(logData);
	}

	// if data received is an int.
	void LogInt(string address, int data){

		string logData = "> " + address + " > " + data.ToString();
		LogData(logData);
	}

	// post the data to the log.
	void LogData(string data){
		if (log.cachedTextGenerator.lineCount > maxLines) log.text = ""; // if the text field has reached the maximum number of lines, clear it.
		log.text = data + "\n" + log.text; // post old data followed by the new data at the top.
	}
}
