using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

public class WavesManager : MonoBehaviour {

	public SpawnZone? spawnZone;
	public Text?	waveLabel;
	public Text?	enemiesLabel;

	// List<GameObject>	enemies = new List<GameObject>();
	
	
	int _currentWave = 0;
	int currentWave { get => _currentWave; set {
		waveLabel!.text = string.Format("Wave [{0}]", value);
		_currentWave = value;
	}}

	int _enemyCount = 0;
	int enemyCount { get => _enemyCount; set {
		enemiesLabel!.text = string.Format("Enemies : [{0}]", value);
		_enemyCount = value;
	}}

	bool paused = false;

	float baseFixedDT;

	private void Start() {
		baseFixedDT = Time.fixedDeltaTime;
	}

	private void Update() {
		if (spawnZone != null && enemyCount == 0) {
			paused = true;
			//TODO display UI
		}

		UpdateTimeScales(paused);

		if (paused && Input.GetKeyDown(KeyCode.Space)) {
			paused = !paused;
			if (spawnZone != null) LaunchWave();
		}

	}

	void LaunchWave() {
			currentWave++;
			var enemies = spawnZone!.Spawn(SpawnCount(currentWave));
			foreach (var enemy in enemies)
				enemy.GetComponentInChildren<Health>()?.OnDepleted.AddListener(() => enemyCount--);
			enemyCount = enemies.Count;
			Debug.LogFormat("Enemy count = {0}", enemyCount);
	}

	void UpdateTimeScales(bool isPaused) {
		var target = isPaused ? 0 : 1;
		Time.timeScale = Mathf.Lerp(Time.timeScale, target, Time.unscaledDeltaTime * 3f);
		Time.fixedDeltaTime = baseFixedDT * Time.timeScale;
	}

	int SpawnCount(int level) => level * 5;

}
