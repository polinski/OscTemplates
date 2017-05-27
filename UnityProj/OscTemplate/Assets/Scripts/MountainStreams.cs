using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MountainStreams : MonoBehaviour {

	public GameObject dot;

	public int amount; // number of root notes to generate
	public float spacing = 0.1f; // spacing for the vis.
	public float height; // to raise from roots.


	public List<int> radioMel = new List<int>();
	public List<int> sleepwalkMel = new List<int>();

	public List<List<int>> songMels = new List<List<int>>(); // master list of song lists

	public List<int> relativeJumps = new List<int>(); // list of the jumps.
	public float relativeJumpDistance;  // the mean jump distance.

	public List<int> melNotes = new List<int>(); // list of the generated root notes.
	private int overallDirection = 0; // tally of whether song generally rises or falls. **UNUSED DATE RIGHT NOW**

	public List<List<int>> streamsList;

	public bool parsed = false;
	public bool streamsGenerated = false;

	void Start () {



		// add songs.
		songMels.Add(radioMel);
		songMels.Add(sleepwalkMel);

		ParseMels(); // parse all the songs.

	}

	void Update(){

	}

	void ParseMels(){

		foreach (List<int> l in songMels){

			overallDirection = 0;
			List<string> thisSong = new List<string>();

			for (int i = 0; i < l.Count; i++){

				if (i == 0) thisSong.Add("s");
				else {
					overallDirection += (l[i] - l[i-1]);
					relativeJumps.Add(l[i] - l[i-1]);
					if (l[i] - l[i-1] > 0)thisSong.Add("u");
					else if (l[i] - l[i-1] < 0) thisSong.Add("d");
					else thisSong.Add("r");
				}
				Debug.Log("ongoing: " + overallDirection);
			}	
			Debug.Log("overall: " + overallDirection);
		}

		int totalJumpDist = 0;
		for (int i = 0; i < relativeJumps.Count; i++){

			int jump;
			if (relativeJumps[i] < 0) jump = relativeJumps[i] * -1;
			else jump = relativeJumps[i];
			totalJumpDist += jump;
			Debug.Log("total: " + overallDirection);
		}
		Debug.Log("total: " + totalJumpDist + " divided by: " + relativeJumps.Count);
		relativeJumpDistance = ((float)totalJumpDist/(float)relativeJumps.Count);
		parsed = true;
	}

	public void GenerateStreams(int numStreams){

		streamsList = new List<List<int>>();

		for (int x = 0 ; x < numStreams; x++){
				
			Color streamColor = new Color (Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f));
			List<int> thisStream = new List<int>();

			int root = 0; // set the root to zero.

			for (int i = 0 ; i < amount; i++){



				int note; 

				if (i == 0) { // if this is the first note, then make this the root.
					int[] scale = {0,2,3,5,7,8,10,12}; // the new note, - the old note, has to match one of these numbers. 
					note = scale[Random.Range(0, scale.Length-1)]; // note is this amount.
					root = 0; // set the root to this number.
					thisStream.Add(note); // add to the list of notes.
				} else{

					int jump = ClosetJumpCorrectedForScale(thisStream[i-1], root); // send the prev note, the amount to jump, and the root to find closest jump corrected for scale.

					note = thisStream[i-1] + jump; // make new note.
					thisStream.Add(note); // add note to list.
				}

				// create vis of note.
				Vector3 pos = new Vector3(i+spacing,note+(height*(x+1)),0); 
				GameObject thisDot = Instantiate(dot, pos, Quaternion.identity) as GameObject;
				thisDot.name = "stream_" + x + " mel_" + i;
				thisDot.tag = "dot";
				thisDot.transform.GetComponent<Renderer>().material.color = streamColor;
			}

			streamsList.Add(thisStream);
		}
		streamsGenerated = true;
	}

	void OscToMax(string address){

		string reset = "";
		if (address == "/melody") reset = "/resetMels";
		if (address == "/mel2") reset = "/resetMel2";

//		oscCode.SendOsc(reset, "clear");
		foreach (int i in melNotes){

//			oscCode.SendOsc(address, i.ToString());
		}

	}

	int ClosetJumpCorrectedForScale(int prev, int root){

		float g = MathsAndMaths.NextGaussian();
		int x = Mathf.RoundToInt(g*relativeJumpDistance); // an amount to jump, based on normal dist.

		int jump = relativeJumps.Aggregate((m,n) => Mathf.Abs(m-x) < Mathf.Abs(n-x) ? m : n); // gets the closet jump value from list of jumps in seed songs.

		int PossibleNote = prev + jump; // the possible next note is the previous note plus this new jump.
		bool inScale = false; // set a bool to check the scale.
		int[] scale = {0,2,3,5,7,8,10,12}; // the new note, - the old note, has to match one of these numbers. 

		int test; // value to test.
		if (PossibleNote < root) test = 12 - (root - PossibleNote); // if possible note is lower than the root, it needs to be 12 - (root - possible NOte)/
		else test = PossibleNote - root; // if possible new note is above the root

		for (int i = 0; i < scale.Length; i++){

			if (test == scale[i]) inScale = true;
		}

		if (inScale) return jump;
		else {
			jump = ClosetJumpCorrectedForScale(prev, root);
		}

		return jump;
	}


	// CALCULATE THE NEXT NOTE JUMP.

//	int ClosetJumpCorrectedForScale(int root){
//
//
//
//		float g = MathsAndMaths.NextGaussian();
//		int x = Mathf.RoundToInt(g*relativeJumpDistance); // an amount to jump, based on normal dist.
//
//		int jump = relativeJumps.Aggregate((m,n) => Mathf.Abs(m-x) < Mathf.Abs(n-x) ? m : n); // gets the closet jump value from list of jumps in seed songs.
//		
//		int PossibleNote = root + jump; // the possible next note is the previous note plus this new jump.
//		bool inScale = false; // set a bool to check the scale.
//		int[] scale = {0,2,3,5,7,8,10,12}; // the new note, - the old note, has to match one of these numbers. 
//
//		int test; // value to test.
//		if (PossibleNote < root) test = 12 - (root - PossibleNote); // if possible note is lower than the root, it needs to be 12 - (root - possible NOte)/
//		else test = PossibleNote - root; // if possible new note is above the root
//
//		for (int i = 0; i < scale.Length; i++){
//
//			if (test == scale[i]) inScale = true;
//		}
//
//		if (inScale) return jump+12;
//		else {
//			jump = ClosetJumpCorrectedForScale(root);
//		}
//
//		return jump+12;
//	}
}
