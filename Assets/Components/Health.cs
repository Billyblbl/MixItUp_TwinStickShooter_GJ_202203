using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable enable

public class Health : MonoBehaviour {
	public float maxValue = 100;
	[SerializeField] private float _current = 100;
	public float current {get => _current; set {
		_current = value;
		OnValueChange?.Invoke(current);
		if (current <= 0f) OnDepleted?.Invoke();
	}}

	public UnityEvent<float>	OnValueChange = new UnityEvent<float>();
	public UnityEvent	OnDepleted = new UnityEvent();

	public List<(float, float end)> dots = new List<(float, float)>();

	private void Update() {
		dots.RemoveAll((dot) => dot.end < Time.time);
		foreach (var (dmg, _) in dots) current -= dmg * Time.deltaTime;
	}

	public void SelfDestroy(float time = 0f) {
		Destroy(gameObject, time);
	}

}
