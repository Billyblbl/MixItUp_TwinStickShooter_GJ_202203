using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#nullable enable

public class TSSControler : ShipController {
	public Transform?	cursor;
	public Image?	dashIncicatorOverlay;
	public float dashSpeed = 2f;
	public float dashTime = .1f;
	public float dashCooldown = 5f;

	float dashStart = 0f;

	private void Update() {
		movement.x = Input.GetAxis("Horizontal");
		movement.y = Input.GetAxis("Vertical");

		UpdateAim();

		if (Input.GetMouseButton(0) && CanFire()) Fire();
		if (Input.GetKeyDown(KeyCode.LeftShift) && dashStart + dashCooldown < Time.time) {
			dashStart = Time.time;
		}

		dashIncicatorOverlay!.fillAmount = Mathf.InverseLerp(dashStart + dashCooldown, dashStart, Time.time);
	}

	private void FixedUpdate() {
		var dashFactor = (dashStart + dashTime > Time.time) ? dashSpeed : 1f;
		UpdateMovement(dashFactor);
	}

	public void OnDeath() {
		StartCoroutine(Die());
	}

	IEnumerator	Die() {
		yield return new WaitForSeconds(.6f);
		gameObject.SetActive(false);
	}
}
