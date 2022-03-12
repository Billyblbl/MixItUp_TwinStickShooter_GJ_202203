using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#nullable enable

public class TSSControler : MonoBehaviour {

	public Rigidbody2D?	rb;
	public Transform?	cursor;
	public ProjectileSource2D?	source;
	public float speed = 10f;
	public float aimSpring = 10f;
	public float dashSpeed = 2f;
	public float dashTime = .1f;

	float dashStart = 0f;
	Vector2 inputs;

	private void Update() {
		inputs.x = Input.GetAxisRaw("Horizontal");
		inputs.y = Input.GetAxisRaw("Vertical");

		var aim = cursor!.position - transform.position;
		var targetRotation = Quaternion.Euler(Vector3.forward * Vector2.SignedAngle(Vector2.up, aim));
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * aimSpring);

		if (Input.GetMouseButtonDown(0)) {
			source?.FireProjectile();
		}

		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			dashStart = Time.time;
		}
	}

	private void FixedUpdate() {
		var dashFactor = (dashStart + dashTime > Time.time) ? dashSpeed : 1f;
		rb!.velocity = Vector2.ClampMagnitude(inputs, 1f) * speed * dashFactor;
		Debug.LogFormat("Velocity = {0}", rb!.velocity);
	}
}
