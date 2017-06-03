using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTestOsc : MonoBehaviour {

	public string address; // set osc address manualy.
	public string message; // set message manually.

	private bool addressUpdated; // a bool button can check so it doesn't send null data.
	private bool messageUpdated; // a bool button can check so it doesn't send null data.

	private OscCodeSender oscCodeSender; // the oscCodeSender script.

	void Start () {
		
		oscCodeSender = GameObject.Find("OscIO").GetComponent<OscCodeSender>(); // access the script.
	
	}

	// checks to see that both address and message data have been entered. if both true, sends the message.
	public void SendOutTestMessage(){

		if (addressUpdated && messageUpdated){
			oscCodeSender.SendOsc(address,message);
		}
	}

	// this is remotely called by the 'address' text input field.
	public void UpdateAddress(string newAddress){

		address = newAddress; // update string.
		addressUpdated = true; // set update to true.
	}

	// this is remotely called by the 'message' text input field.
	public void UpdateMessage(string newMessage){

		message = newMessage; // update string.
		messageUpdated = true; // set update to true.
	}
}
