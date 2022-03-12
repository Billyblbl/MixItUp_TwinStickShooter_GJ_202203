using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#nullable enable

public class TSSControler : MonoBehaviour {

	public Rigidbody2D?	rb;
	public Transform?	cursor;
	public ProjectileSource2D?	source;
	public Image?	dashIncicatorOverlay;
	public float speed = 10f;
	public float aimSpring = 10f;
	public float dashSpeed = 2f;
	public float dashTime = .1f;
	public float dashCooldown = 5f;
	[Tooltip("RPM")] public float fireRate = 100f;

	float dashStart = 0f;
	Vector2 inputs;

	float lastShot = 0f;

	private void Update() {
		inputs.x = Input.GetAxis("Horizontal");
		inputs.y = Input.GetAxis("Vertical");

		var aim = cursor!.position - transform.position;
		var targetRotation = Quaternion.Euler(Vector3.forward * Vector2.SignedAngle(Vector2.up, aim));
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * aimSpring);

		if (Input.GetMouseButton(0) && Time.time > lastShot + 60/fireRate) {
			lastShot = Time.time;
			source?.FireProjectile();
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) && dashStart + dashCooldown < Time.time) {
			dashStart = Time.time;
		}

		dashIncicatorOverlay!.fillAmount = Mathf.InverseLerp(dashStart + dashCooldown, dashStart, Time.time);
	}

	private void FixedUpdate() {
		var dashFactor = (dashStart + dashTime > Time.time) ? dashSpeed : 1f;
		rb!.velocity = Vector2.ClampMagnitude(inputs, 1f) * speed * dashFactor;
	}
}
