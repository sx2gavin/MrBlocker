using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed = 1f;
	public float speedIncrease = 0.5f;
	public float spiningSpeed = 1f;
	public GameController gameController;
	public GameObject explosion;

	float speedMax = 12f;
	Vector3 movingDirection = new Vector3(1f, 1f, 0f);

	// Use this for initialization
	void Start () {
		movingDirection = movingDirection.normalized;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = this.transform.position + movingDirection * speed * Time.deltaTime;
		transform.Rotate (0f, 0f, spiningSpeed);
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
			spiningSpeed = spiningSpeed * 2;

			Destroy (col.gameObject);
			Instantiate (explosion, col.contacts [0].point, Quaternion.identity);
			Instantiate(this, transform.position, Quaternion.identity);
			gameController.RayDestroyed ();
		} else if (col.gameObject.CompareTag ("Boundary")) {
			Vector3 rotate = col.transform.eulerAngles; 
			Vector3 original = new Vector3 (1f, 0f, 0f);
			Vector3 normal = Quaternion.Euler (0f, 0f, rotate.z + 90f) * original;
			normal = normal.normalized;
			
			if (Vector3.Dot (movingDirection, normal) > 0f) {
				normal = -normal;
			}
			
			movingDirection = movingDirection - 2 * normal * Vector3.Dot (movingDirection, normal);
			// Instantiate(this, transform.position, Quaternion.identity);
			if ( speed < speedMax ) {
				speed = speed + speedIncrease;
				spiningSpeed = spiningSpeed * 2;
			}
		}
	}
}
