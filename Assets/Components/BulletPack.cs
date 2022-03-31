using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class BulletPack : MonoBehaviour {
	public ProjectileSource2D?	source;
	public int shots = 10;

	private void Start() {
		for (int i = 0; i < shots; i++) {
			var angle = Random.Range(0, 360);
			transform.eulerAngles = Vector3.forward * angle;
			source?.FireProjectile();
		}
	}
}
