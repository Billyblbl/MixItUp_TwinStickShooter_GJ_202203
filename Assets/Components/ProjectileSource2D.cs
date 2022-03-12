using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable enable

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProjectileSource2D : MonoBehaviour {
	public Rigidbody2D?	projectile = null;
	public Vector2		velocity = Vector2.up;
	public Vector2		recoil;
	public Rigidbody2D?	inertia = null;
	public UnityEvent?	OnFire = null;

	public void FireProjectile() {
		launch(spawn());
		Debug.Log("shoot");
		OnFire?.Invoke();
	}

	public Vector2 offset = Vector2.zero;

	public Rigidbody2D spawn() => UnityObjectExtension.InstantiateOff(projectile!, transform.TransformPoint(offset), transform.rotation);
	public void launch(Rigidbody2D rb, bool applyRecoil = true) {
		rb.gameObject.SetActive(true);
		rb.velocity = Vector3.zero;
		if (inertia != null) {
			rb.velocity = inertia.velocity;
			if (applyRecoil) inertia.AddForce(transform.TransformVector(recoil));
		}
		Vector2 vel = transform.TransformVector(velocity);
		rb.velocity += vel;
	}
}
