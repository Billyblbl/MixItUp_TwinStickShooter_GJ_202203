using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class ShipController : MonoBehaviour {

	public RulesManager? rules;
	public Rigidbody2D?	rb;
	public Transform?	target;
	public ProjectileSource2D?	source;
	public float speed = 10f;
	public float aimSpring = 10f;
	[Tooltip("RPM")] public float fireRate = 100f;

	float lastShot = 0f;
	protected Vector2 movement;

	protected bool CanFire() => Time.time > lastShot + 60/fireRate;
	protected void Fire() {
		lastShot = Time.time;
		source?.FireProjectile();
		rules?.NotifyRule(RulesManager.Trigger.Shoot, transform.position);
	}
	protected void UpdateMovement(float speedMul = 1f) => rb!.velocity = Vector2.ClampMagnitude(movement, 1f) * speed * speedMul;

	protected void UpdateAim() {

		var aim = target!.position - transform.position;
		var targetRotation = Quaternion.Euler(Vector3.forward * Vector2.SignedAngle(Vector2.up, aim));
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * aimSpring);

	}

	private void OnCollisionEnter2D(Collision2D other) {
		rules?.NotifyRule(RulesManager.Trigger.Collision, transform.position);
	}
}
