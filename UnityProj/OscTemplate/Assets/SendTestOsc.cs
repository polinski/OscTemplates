using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTestOsc : MonoBehaviour {

	public string address;
	public string message;

	private bool addressUpdated;
	private bool messageUpdated;

	private OscCodeSender oscCodeSender;

	// Use this for initialization
	void Start () {
		
		oscCodeSender = GameObject.Find("OscIO").GetComponent<OscCodeSender>();
	
	}
	
	// Update is called once per frame
	void Update () {

//		if (addressUpdated && messageUpdated) SendTestMessage(address,message);
		
	}

	public void SendOutTestMessage(){

		if (addressUpdated && messageUpdated){
			oscCodeSender.SendOsc(address,message);
			Debug.Log("sending message: " + address + " " + message);

			addressUpdated = false;
			messageUpdated = false;
		}
	}

	public void UpdateAddress(string newAddress){

		address = newAddress;
		addressUpdated = true;
	}
	public void UpdateMessage(string newMessage){

		message = newMessage;
		messageUpdated = true;
	}

}
