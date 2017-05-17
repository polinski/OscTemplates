using UnityEngine;
using System.Collections;
using UniOSC;

public class OscCodeSender : MonoBehaviour {

	#region public

	public string OSCAddressOUT1; // set the address manually.
	public UniOSCConnection OSCConnectionOUT; // set the OSC connection.

	[Space(10)]

	#endregion

	#region private

	private UniOSCEventDispatcherCBImplementation oscDispatcher1; // Declare an OSC message dispatcher from UniOSC framework.

	#endregion


	void Awake ()
	{		
		oscDispatcher1 = new UniOSCEventDispatcherCBImplementation(OSCAddressOUT1,OSCConnectionOUT); // Wake up and get the dispatcher ready.
	}

	void OnEnable()
	{			
		oscDispatcher1.Enable ();	// enable it.
		oscDispatcher1.ClearData (); // clear data just in case.
	}



	// Function to send out an OSC message.

	public void SendOsc (string address, string message)
	{
		oscDispatcher1.oscOutAddress = address; // sets the address.
		oscDispatcher1.ClearData (); // clears the cache.
		oscDispatcher1.AppendData(message); // add the message.
		oscDispatcher1.SendOSCMessage(); // send the message.
	}
}



