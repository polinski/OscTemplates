using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain {

	public List<int> roots;
	public List<List<int>> streams;

	public string rootString;
	public List<string> streamStrings;


	public Mountain (List<int> _roots, List<List<int>> _streams){

		roots = _roots;
		streams = _streams;

		rootString = "";
		streamStrings = new List<string>();

		foreach (int i in _roots){

			rootString += i;
			rootString += " ";
		}

		foreach (List<int> list in _streams){

			string thisStream = "";

			foreach (int i in list){

				thisStream += i;
				thisStream += " ";
			}
		
			streamStrings.Add(thisStream);
		}

	}



}
