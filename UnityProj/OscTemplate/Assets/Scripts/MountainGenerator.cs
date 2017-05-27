using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainGenerator : MonoBehaviour {

	public int numStreams;

	private MountainRoots mountainRoots;
	private MountainStreams mountainStreams;
	private OscCodeSender oscCodeSender;


	// Use this for initialization
	void Start () {

		mountainRoots = GetComponent<MountainRoots>();
		mountainStreams = GetComponent<MountainStreams>();
		oscCodeSender = GameObject.Find("OscIO").GetComponent<OscCodeSender>();
		
	}
	
	void Update(){

		if (Input.GetKeyDown(KeyCode.O)){

			Debug.Log("intruction reluctantly acknowledged");
			StartCoroutine(GenerateMountain());

		}

	}

	IEnumerator GenerateMountain(){

		mountainRoots.GenerateRoots();
		Debug.Log("generating roots");

		while (!mountainRoots.rootsGenerated){
			Debug.Log("waiting for roots");
			yield return new WaitForEndOfFrame();
		}

		mountainStreams.GenerateStreams(numStreams);
		Debug.Log("generating streams");

		while (!mountainStreams.streamsGenerated){
			Debug.Log("waiting for streams");
			yield return new WaitForEndOfFrame();
		}

		if (mountainRoots && mountainStreams) {

			Debug.Log("making new mountain");
			Mountain m = new Mountain(mountainRoots.notes, mountainStreams.streamsList);
			SendMountainViaOsc(m);
		}

		yield return null;
	}


	void SendMountainViaOsc(Mountain mountain){

		Debug.Log("sending mountain data");
		oscCodeSender.SendOsc("/roots", "reset");
		oscCodeSender.SendOsc("/roots", mountain.rootString);

		int count = 0;

		foreach (string s in mountain.streamStrings){

			oscCodeSender.SendOsc("/stream_" + count, "reset");
			oscCodeSender.SendOsc("/stream_" + count, s);
			count++;
		}



	}
}
