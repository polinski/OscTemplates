using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ThisEvent : UnityEvent<object>
{
}

public class EventManager : MonoBehaviour {

	private Dictionary <string, ThisEvent> eventDictionary;

	private static EventManager eventManager;
	public static int EventIndex;


	// CREATES AN INSTANCE OF THE EVENT MANAGER. LOTS OF SAFETY CHECKS.

	public static EventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

				if (!eventManager)
				{
					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init ();  // INITIALISES THE EVENT MANAGER.
				}
			}

			return eventManager;
		}
	}

	void Init ()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, ThisEvent>(); // create a new dictionary. This holds a string as the key and a Unity event as the value.
		}
		EventIndex = 0; // resets the event index to zero. This is being used by the scheduler to keep track of where in a song we are.
	}


	// THIS IS CALLED REMOTELY BY THE LISTENER. The string is what the listener is looking out for. The action is the method it runs upon hearing that string.

	public static void StartListening (string eventName, UnityAction<object> listener)
	{
		ThisEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{	
			thisEvent = new ThisEvent();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	// THIS IS CALLED REMOTELY BY THE LISTENER TO UNSUBSCRIBE.

	public static void StopListening (string eventName, UnityAction<object> listener)
	{
		if (eventManager == null) return;
		ThisEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}


	// THIS IS THE KEY EVENT. ELSEWHERE A SCRIPT CAN FIRE OFF AN INSTRUCTION WITH A STRING. IF ANYTHING IS LISTENING FOR THAT STRING, IT WILL RUN THAT METHOD.

	public static void TriggerEvent (string eventName, object obj)
	{
		ThisEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.Invoke (obj);
		}
	}


}