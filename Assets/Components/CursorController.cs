using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

	Camera cam;


	private void Start() {
		cam = Camera.main;
	}

	private void Update() {
		Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
		transform.position = pos;
	}
}
