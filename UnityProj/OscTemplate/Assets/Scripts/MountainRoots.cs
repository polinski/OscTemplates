using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MountainRoots : MonoBehaviour {

	public GameObject dot; // for the vis

	public int amount; // number of root notes to generate
	public float spacing = 0.1f; // spacing for the vis.

	public List<List<int>> songRoots = new List<List<int>>(); // master list of song lists

	// song chord progressions
	public List<int> radio = new List<int>();
	public List<int> japan = new List<int>();
	public List<int> supermoon = new List<int>();
	public List<int> heatdeath = new List<int>();
	public List<int> sleepwalk = new List<int>();

	public List<int> relativeJumps = new List<int>(); // list of the jumps.
	public float relativeJumpDistance;  // the mean jump distance.

	private int overallDirection = 0; // tally of whether song generally rises or falls. **UNUSED DATE RIGHT NOW**

	public List<int> notes = new List<int>(); // list of the generated root notes.

	public bool parsed = false;
	public bool rootsGenerated = false;

	OscCodeSender oscCode; // to send code.

	// Use this for initialization
	void Start () {

		oscCode = GameObject.Find("OscIO").GetComponent<OscCodeSender>(); // get osc script.

		// add songs.
		songRoots.Add(radio);
		songRoots.Add(japan);
		songRoots.Add(supermoon);
		songRoots.Add(heatdeath);
		songRoots.Add(sleepwalk);
	
		ParseSongs(); // parse all the songs.

	}

	void Update(){

	}

	// go through each song... calculate rise and fall, although this is currently unused. calculate jump distances and also add them to the list of possible jumps.
	void ParseSongs(){

		foreach (List<int> l in songRoots){

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

		void OscToMax(){

		oscCode.SendOsc("/resetNotes", "clear");
		foreach (int i in notes){

			Debug.Log("sending osc: " + i);
			oscCode.SendOsc("/notes", i.ToString());
		}

	}

	public void GenerateRoots(){

		// delete any previous visualisation.
		GameObject[] dots = GameObject.FindGameObjectsWithTag("dot");

		notes.Clear();

		for (int i = 0 ; i < dots.Length; i++){
			Destroy(dots[i]);
		}

		int root = 0; // set the root to zero.

		for (int i = 0 ; i < amount; i++){

			int note; 

			if (i == 0) { // if this is the first note, then make this the root.
				note = 0; // note is this amount.
				root = 0; // set the root to this number.
				notes.Add(note); // add to the list of notes.
			} else{

				int jump = ClosetJumpCorrectedForScale(notes[i-1], root); // send the prev note, the amount to jump, and the root to find closest jump corrected for scale.
	
				note = notes[i-1] + jump; // make new note.
				notes.Add(note); // add note to list.
			}

			// create vis of note.
			Vector3 pos = new Vector3(i+spacing,note,0); 
			GameObject thisDot = Instantiate(dot, pos, Quaternion.identity) as GameObject;
			thisDot.tag = "dot";
		}
		rootsGenerated = true;
	}

	
	// CALCULATE THE NEXT NOTE JUMP.

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
}
