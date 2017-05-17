/*
* UniOSC
* Copyright ©2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using System.Collections;
using UniOSC;

/// <summary>
/// Demo to show how to use the class based scripts.
/// </summary>
public class OscCodeSender : MonoBehaviour {
		
		#region public
		
		public string OSCAddressOUT1;
		public UniOSCConnection OSCConnectionOUT;
			
		[Space(10)]
		
		#endregion
		
		#region private
		
		private UniOSCEventDispatcherCBImplementation oscDispatcher1;
		
		#endregion
		
		
		void Awake ()
		{		
			oscDispatcher1 = new UniOSCEventDispatcherCBImplementation(OSCAddressOUT1,OSCConnectionOUT);
		}
		
		void OnEnable()
		{			
			oscDispatcher1.Enable ();	
			oscDispatcher1.ClearData ();			
		}
		
		
	public void SendOsc (string address, string message)
		{
				oscDispatcher1.oscOutAddress = address;
				oscDispatcher1.ClearData ();
				oscDispatcher1.AppendData(message);
				oscDispatcher1.SendOSCMessage();				
		}
	}



