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

	private void OnTriggerEnter2D(Collider2D other) {
		var damages = other.GetComponents<Damage>();
		foreach (var damage in damages) Hit(damage);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		var damages = other.collider.GetComponents<Damage>();
		foreach (var damage in damages) Hit(damage);
	}

}
