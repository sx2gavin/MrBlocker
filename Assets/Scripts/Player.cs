using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 6f;
	public GameObject walls;
	public GameController gameController;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Vector3 movement = new Vector3(h, v, 0f);
		movement = movement.normalized * speed * Time.deltaTime;
		this.transform.position = this.transform.position + movement;

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -15.5f, 15.5f), Mathf.Clamp (transform.position.y, -9.5f, 9.5f), 0f);

		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPosition.z = 0f;

		Vector3 playerToMouse = mouseWorldPosition - transform.position;
		playerToMouse = playerToMouse.normalized;
		Vector3 xAxis = new Vector3 (1f, 0f, 0f);

		float rotateAngle = Vector3.Angle (xAxis, playerToMouse);

		if (playerToMouse.y < 0f) {
			rotateAngle = 360 - rotateAngle;
		}

		transform.rotation = Quaternion.Euler(0f, 0f, rotateAngle);

		if (Input.GetMouseButtonDown (0) && gameController.CanMakeRay()) {
			GetComponent<AudioSource>().Play();
			Instantiate(walls, this.transform.position + 20.5f * playerToMouse, Quaternion.Euler (0f, 0f, rotateAngle));
			gameController.AddRay();
		}
	}
	
}