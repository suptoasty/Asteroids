using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public GameObject projectile;
	public float moveSpeed = 5;
	public float rotationSpeed = 5;
	private float shootCoolDown = 0.35f;
	public int lives = 3;
	private float hitCoolDown = 1.0f;

	// Update is called once per frame
	void FixedUpdate()
	{
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		body.MoveRotation(body.rotation + Input.GetAxis("Horizontal")*rotationSpeed);
		body.AddForce(transform.up * moveSpeed * Input.GetAxis("Vertical"));
		if(Input.GetKeyDown("space") && shootCoolDown <= 0.0f) {
			shoot();
			shootCoolDown = 0.35f;
		}
		shootCoolDown -= Time.fixedDeltaTime;
		hitCoolDown -= Time.fixedDeltaTime;

		if(Input.GetKeyDown("e")) {
			warp();
		}

		if(lives < 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log(col);
		if(col.gameObject.tag == "asteroid" && hitCoolDown <= 0.0f) {
			lives--;
			hitCoolDown = 1.0f;
		}
	}

	void OnBecameInvisible() {
		var screenPos = Camera.main.WorldToViewportPoint(transform.position);
		var pos = transform.position;
		if (screenPos.x > 1 || screenPos.x < 0) pos.x = -pos.x;
		if (screenPos.y > 1 || screenPos.y < 0) pos.y = -pos.y;
		transform.position = pos;
	}

	void warp() {
		transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Camera.main.nearClipPlane + 15.0f));
	}

	void shoot() {
		Instantiate(projectile, transform.position, transform.rotation);
	}

}
