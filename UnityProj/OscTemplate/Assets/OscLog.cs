using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscLog : MonoBehaviour {

	private Text log;
	public int maxLines;

	void OnEnable(){

		OscReceiver.onString += LogString;
		OscReceiver.onFloat += LogFloat;
		OscReceiver.onInt += LogInt;
	}

	void Disable(){

		OscReceiver.onString -= LogString;
		OscReceiver.onFloat -= LogFloat;
		OscReceiver.onInt -= LogInt;
	}

	void Start(){

		log = GameObject.Find("OscStream").GetComponent<Text>();
		log.text = "";

	}
		
	void LogString(string address, string data){

		string logData = "> " + address + " > " + data;
		LogData(logData);
	}

	void LogFloat(string address, float data){

		string logData = "> " + address + " > " + data.ToString();
		LogData(logData);
	}

	void LogInt(string address, int data){

		string logData = "> " + address + " > " + data.ToString();
		LogData(logData);
	}

	void LogData(string data){
		if (log.cachedTextGenerator.lineCount > maxLines) log.text = "";
		log.text = data + "\n" + log.text;
	}
}
