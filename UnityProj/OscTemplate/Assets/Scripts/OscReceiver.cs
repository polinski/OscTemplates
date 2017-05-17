
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UniOSC;
using OSCsharp.Data;

public class OscReceiver :  UniOSCEventTarget {

	public delegate void OscString(string address, string data);
	public static event OscString onString;

	public delegate void OscFloat(string address, float data);
	public static event OscFloat onFloat;

	public delegate void OscInt(string address, int data);
	public static event OscInt onInt;

		void Awake(){

		}


		public override void OnEnable()
		{
			base.OnEnable();
		}


	public override void OnOSCMessageReceived(UniOSCEventArgs args)
		{

		OscMessage msg = (OscMessage)args.Packet;

		if(msg.Data.Count <1)return;

		string types = msg.TypeTag;

		if(msg.Data.Count >= 1){

			for (int i = 0; i < msg.Data.Count; i++){

//				Debug.Log("address: " + msg.Address + ". message data : " + i + " is: " + msg.Data[i] + " which is a type: " + types[i+1]);
				if (types[i+1] == 's') onString(msg.Address, msg.Data[i].ToString());
				if (types[i+1] == 'f') onFloat(msg.Address, (float)msg.Data[i]);
				if (types[i+1] == 'i') onInt(msg.Address, (int)msg.Data[i]);
			}
		}
	}
}
