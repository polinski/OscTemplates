using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGrid : MonoBehaviour {

	public int rows;
	public int columns;

	public float space;

	public GameObject emptyGrid;

	public Vector3 startPos;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < rows; i++){
			for (int j = 0; j < columns; j++){

				Vector3 pos = startPos;
				pos += new Vector3(i*space, j*space, 0);
				GameObject thisCell = Instantiate(emptyGrid, pos, Quaternion.identity) as GameObject;
				thisCell.name = "cell_" + j + "_" + i;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
