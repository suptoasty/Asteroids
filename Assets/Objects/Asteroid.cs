using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public Sprite[] spriteList;
	public float moveSpeed = 100;
	public int lives = 5;
	public Asteroid asteroid;
	public GameManager gm;

	// Start is called before the first frame update
	void Start()
	{
		lives = (int)Random.Range(0, 5);
		moveSpeed = Random.Range(100, 300);
		int index = Random.Range(0, spriteList.Length);
		GetComponent<SpriteRenderer>().sprite = spriteList[index];
		transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
		GetComponent<Rigidbody2D>().AddForce(transform.up*moveSpeed);
	}
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "bullet") {
			Destroy(col.gameObject);
			if(lives > 0) {
				gm.addScore((int)Random.Range(1.0f, 100.0f));
				int times = (int)Random.Range(0.0f, 5.0f);
				for(int i = 0; i < times; i++) SpawnChild();
			}
			gm.asteroidCount--;
			Destroy(this.gameObject);
		}
	}
	void OnBecameInvisible() {
		var screenPos = Camera.main.WorldToViewportPoint(transform.position);
        var pos = transform.position;
        if (screenPos.x > 1 || screenPos.x < 0) pos.x = -pos.x;
        if (screenPos.y > 1 || screenPos.y < 0) pos.y = -pos.y;
        transform.position = pos;
	 }

	void SpawnChild() {
		Asteroid child = Instantiate(asteroid, transform.position, Quaternion.identity);
		child.lives = lives-1;
		gm.asteroidCount++;
	}
}
