using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Impact : MonoBehaviour {

	public float deleteOffset = 0f;
	public UnityEvent OnImpact;

	private void OnCollisionEnter2D(Collision2D other) {
		Destroy(gameObject, deleteOffset);
	}

}
