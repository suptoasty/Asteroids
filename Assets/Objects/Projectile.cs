using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 100;
    public float lifeTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed);
        Destroy(this, lifeTime);
    }

    void OnBecameInvisible()
	{
		var screenPos = Camera.main.WorldToViewportPoint(transform.position);
		var pos = transform.position;
		if (screenPos.x > 1 || screenPos.x < 0) pos.x = -pos.x;
		if (screenPos.y > 1 || screenPos.y < 0) pos.y = -pos.y;
		transform.position = pos;
	}
}
