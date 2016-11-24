using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float rotateSpeed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	 
	private float nextFire;

	void Update ()
	{
		if (Input.GetKey (KeyCode.Space) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			shot.GetComponent<Done_Mover> ().direction = transform.forward;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource> ().Play ();
		}
	}

	void FixedUpdate ()
	{
		float angle = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = transform.forward * moveVertical;

		rotate (angle * rotateSpeed);
		GetComponent<Rigidbody> ().velocity = movement * speed;


		GetComponent<Rigidbody> ().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax)
		);
	}

	void rotate (float angles)
	{
		transform.RotateAround (transform.position, transform.up, angles);
	}
}