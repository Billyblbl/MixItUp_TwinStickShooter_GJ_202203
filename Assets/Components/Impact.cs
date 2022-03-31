using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Impact : MonoBehaviour {

	public float deleteOffset = 0f;
	public UnityEvent OnImpact;

	public bool triggerOnSolidCollision = true;
	public bool triggerOnZoneCollision = true;

	private void OnCollisionEnter2D(Collision2D other) {
		OnImpact?.Invoke();
		if (triggerOnSolidCollision) Destroy(gameObject, deleteOffset);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		OnImpact?.Invoke();
		if (triggerOnZoneCollision) Destroy(gameObject, deleteOffset);
	}

}
