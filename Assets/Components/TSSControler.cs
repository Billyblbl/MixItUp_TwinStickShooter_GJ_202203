using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#nullable enable

public class TSSControler : MonoBehaviour {

	public Rigidbody2D?	rb;
	public Transform?	cursor;
	public float speed = 10f;
	public float aimSpring = 10f;

	private void Update() {
		var inputs = new Vector2(
			Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical")
		);
		rb!.velocity = Vector2.ClampMagnitude(inputs, 1f) * speed;
		var aim = cursor!.position - transform.position;
		var targetRotation = Quaternion.Euler(Vector3.forward * Vector2.SignedAngle(Vector2.up, aim));
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * aimSpring);
	}
}
