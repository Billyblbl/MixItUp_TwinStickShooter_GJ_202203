using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D other) {
		Destroy(gameObject);
	}
}
