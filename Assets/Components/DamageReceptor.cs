using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable enable

[RequireComponent(typeof(Collider2D))]
public class DamageReceptor : MonoBehaviour {
	public Health? health;

	public UnityEvent OnHit = new UnityEvent();
	public void Hit(Damage damage) {
		OnHit?.Invoke();
		if (health != null) {
			if (damage.dot) health.dots.Add((damage.value, Time.time + damage.dotDuration));
			else health.current -= damage.value;
		}
	}

	public bool triggerOnSolidCollision = false;
	public bool triggerOnZoneCollision = true;

	private void OnTriggerEnter2D(Collider2D other) {
		if (!triggerOnZoneCollision) return;
		var damages = other.GetComponents<Damage>();
		// Debug.LogFormat("Trigger Damage from {0}", other.gameObject.name);
		foreach (var damage in damages) Hit(damage);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (!triggerOnSolidCollision) return;
		var damages = other.collider.GetComponents<Damage>();
		// Debug.LogFormat("Collision Damage from {0}", other.gameObject.name);
		foreach (var damage in damages) Hit(damage);
	}

}
