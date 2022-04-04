using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable enable

public class Health : MonoBehaviour {

	public float maxValue = 100;
	[SerializeField] private float _current = 100;
	public float regen = 0f;
	public float regenDelay = 0f;

	float lastLoss = 0f;

	public float current {get => _current; set {
		if (value < _current) lastLoss = Time.time;
		_current = Mathf.Clamp(value, -1, maxValue);
		OnValueChange?.Invoke(current);
		OnFractionChange?.Invoke(current / maxValue);
		if (current <= 0f) {
			OnDepleted?.Invoke();
			OnDepleted?.RemoveAllListeners();
		}
	}}

	public UnityEvent<float>	OnValueChange = new UnityEvent<float>();
	public UnityEvent<float>	OnFractionChange = new UnityEvent<float>();
	public UnityEvent	OnDepleted = new UnityEvent();

	public List<(float, float end)> dots = new List<(float, float)>();

	private void Update() {
		if (current < maxValue && lastLoss + regenDelay < Time.time) {
			current = Mathf.Clamp(current + regen * Time.deltaTime, 0, maxValue);
		}


		dots.RemoveAll((dot) => dot.end < Time.time);
		foreach (var (dmg, _) in dots) current -= dmg * Time.deltaTime;
	}

	public void SelfDestroy(float time = 0f) {
		Destroy(gameObject, time);
	}

}
