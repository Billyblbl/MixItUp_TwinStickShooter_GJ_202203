using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class EnemyController : ShipController {

	public Animator? animator = null;

	public enum EnemyType : int {
		Light = 0,
		Heavy = 1,
		Specialist = 2
	}

	public EnemyType enemyType;

	private void Start() {
		float typeF = (int)enemyType;
		animator?.SetFloat("Type", typeF);
	}

	private void OnEnable() {
		target = FindObjectOfType<TSSControler>().transform;
	}

	public float targetDistance = 2f;
	public bool	maintainDistance = false;
	public float aimCone = 20f;
	public float maxRange = 4f;

	void ChooseMovement() {
		movement = Vector2.zero;
		var delta = target!.position - transform.position;
		if (delta.magnitude > targetDistance || maintainDistance) {
			var dir = delta.normalized;
			var targetDelta = delta - dir * targetDistance;
			movement = Vector2.ClampMagnitude(targetDelta, 1f);
		}
	}

	bool AimedShot() {
		var delta = target!.position - transform.position;
		if (delta.magnitude > maxRange) return false;
		var angle = Vector2.Angle(delta, transform.up);
		return angle * 2 < aimCone;
	}

	private void Update() {
		UpdateAim();
		ChooseMovement();
		if (AimedShot() && CanFire()) Fire();
	}

	private void FixedUpdate() {
		UpdateMovement();
	}

}
