using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UniOSC;
using OSCsharp.Data;

public class OscReceiver :  UniOSCEventTarget {

	public delegate void OscString(string address, string data); // this delegate broadcasts a 'string' message.
	public static event OscString onString;

	public delegate void OscFloat(string address, float data); // this delegate broadcasts a 'float' message.
	public static event OscFloat onFloat;

	public delegate void OscInt(string address, int data); // this delegate broadcasts a 'int' message.
	public static event OscInt onInt;

	EventManager eventManager;


	// no idea what this does, tbh. Taken from the UniOSC examples. Probably necessary? 

	public override void OnEnable()
	{
		base.OnEnable();
	}

	void Start(){

//		eventManager = GameObject.Find("Event_Manager").GetComponent<EventManager>();
	}
//	// Runs this when an OSC message is received.
//	public override void OnOSCMessageReceived(UniOSCEventArgs args)
//	{
//
//		OscMessage msg = (OscMessage)args.Packet; // grab the message.
//
//		if(msg.Data.Count <1)return; // if the message has no data, don't do anything.
//
//		string types = msg.TypeTag; // get the message type. (will be a letter for each piece of data. E.G '1' = i. '4.5 tree' = fs (f for float, s for string).
//
//		// grab each piece of data and use the relevant delegate to broadcast it to whatever is listening...
//		if(msg.Data.Count >= 1){
//
//			for (int i = 0; i < msg.Data.Count; i++){
//
//				if (types[i+1] == 's') onString(msg.Address, msg.Data[i].ToString());
//				if (types[i+1] == 'f') onFloat(msg.Address, (float)msg.Data[i]);
//				if (types[i+1] == 'i') onInt(msg.Address, (int)msg.Data[i]);
//			}
//		}
//	}


	// Runs this when an OSC message is received.
	public override void OnOSCMessageReceived(UniOSCEventArgs args)
	{

		OscMessage msg = (OscMessage)args.Packet; // grab the message.

		if(msg.Data.Count <1)return; // if the message has no data, don't do anything.

		string types = msg.TypeTag; // get the message type. (will be a letter for each piece of data. E.G '1' = i. '4.5 tree' = fs (f for float, s for string).

		// grab each piece of data and use the relevant delegate to broadcast it to whatever is listening...
		if(msg.Data.Count >= 1){

			object message = (object)msg;
			EventManager.TriggerEvent("/fromMax", message);

		}
	}
}
