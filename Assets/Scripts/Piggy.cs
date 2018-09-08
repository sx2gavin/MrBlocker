using UnityEngine;
using System.Collections;

public class Piggy : MonoBehaviour {
	
	public float speed = 1f;
	public float speedIncrease = 0.5f;
	public float spiningSpeed = 1f;
	public GameController gameController;
	public GameObject explosion;
	public GameObject piggyDead;
	public GameObject piggyGhost;
	

	Vector3 movingDirection = new Vector3(-1f, 0f, 0f);
	
	// Use this for initialization
	void Start () {
		movingDirection = movingDirection.normalized;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = this.transform.position + movingDirection * speed * Time.deltaTime;
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag ("Wall")) {
			Vector3 rotate = col.transform.eulerAngles; 
			Vector3 original = new Vector3 (1f, 0f, 0f);
			Vector3 normal = Quaternion.Euler (0f, 0f, rotate.z + 90f) * original;
			normal = normal.normalized;
			
			if (Vector3.Dot (movingDirection, normal) > 0f) {
				normal = -normal;
			}
			
			movingDirection = movingDirection - 2 * normal * Vector3.Dot (movingDirection, normal);
			speed = speed + speedIncrease;
			
			Destroy (col.gameObject);
			Instantiate (explosion, col.contacts [0].point, Quaternion.identity);
			gameController.RayDestroyed ();
		} else if (col.gameObject.CompareTag ("Boundary")) {
			Dead();
		} else if (col.gameObject.CompareTag ("Enemy")) {
			Instantiate (explosion, col.contacts [0].point, Quaternion.identity);
			Dead();
		}
	}

	void Dead() 
	{
		Debug.Log ("Game Over!");
		Destroy (gameObject);
		gameController.GameOver ();
		Instantiate (piggyDead, transform.position, Quaternion.identity);
		Instantiate (piggyGhost, transform.position, Quaternion.identity);
	}
}
