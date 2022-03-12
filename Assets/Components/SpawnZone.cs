using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class SpawnZone : MonoBehaviour {
	public float minDistanceToPlayer = 5f;

	Transform? player;

	public GameObject? blueprint;

	private void Start() {
		player = FindObjectOfType<TSSControler>().transform;
		Spawn(100);
	}

	public void SpawnOne() {
		Vector2 direction;
		RaycastHit2D hit;
		Vector2 origin = player?.position ?? Vector2.zero;

		var angle = Random.Range(0f, 360f);
		direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		hit = Physics2D.Raycast(origin, direction, float.MaxValue, 1 << LayerMask.NameToLayer("Bounds"));

		if (hit.collider != null && hit.distance < minDistanceToPlayer) for (int i = 1; i < angle + 360; i++) {
			direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle + i));
			hit = Physics2D.Raycast(origin, direction, float.MaxValue, 1 << LayerMask.NameToLayer("Bounds"));
			if (hit.distance > minDistanceToPlayer) break;
		}

		var distance = (minDistanceToPlayer < hit.distance) ? Random.Range(minDistanceToPlayer, hit.distance) : hit.distance;
		Instantiate(blueprint, origin + direction * distance, Quaternion.identity);
	}

	public void Spawn(int number) {
		for (int i = 0; i < number; i++) SpawnOne();
	}
}
