using UnityEngine;
using System.Collections;

public class PiggyGhost : MonoBehaviour {


	Vector3 movement;
	// Use this for initialization
	void Start () {
		movement = new Vector3 (0f, 2f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + movement * Time.deltaTime;
	}
}
