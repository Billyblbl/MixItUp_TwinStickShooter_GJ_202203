using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#nullable enable

public class WavesManager : MonoBehaviour {

	public enum Stage : int{
		Playing = 0,
		PauseMenu = 1,
		GameOver = 2
	}

	const int EnemiesPerTiers = 5;

	public SpawnZone? spawnZone;
	public Text?	waveLabel;
	public Text?	enemiesLabel;

	public Image?	fade;
	public Gradient	fadeGradient = new Gradient();
	public RectTransform? swappingsMenu;
	public RectTransform? gameOver;
	public float menusFadeThreshold = 0.9f;

	public Text? killCountText;
	string killCountFormat = "";

	public Stage currentStage = Stage.Playing;

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

	RectTransform?[] menus = new RectTransform?[System.Enum.GetValues(typeof(Stage)).Length];

	private void Start() {

		menus[(int)Stage.Playing] = swappingsMenu;
		menus[(int)Stage.PauseMenu] = null;
		menus[(int)Stage.GameOver] = gameOver;

		baseFixedDT = Time.fixedDeltaTime;
		killCountFormat = killCountText!.text;
	}

	private void Update() {
		if (spawnZone != null && enemyCount == 0) {
			bool pausing = !paused;
			paused = true;
			if (pausing) OnNewWave?.Invoke(currentWave);
		}

		UpdateTimeScales(paused);
		UpdateFade(paused);

		if (paused && Input.GetKeyDown(KeyCode.Space)) {
			paused = !paused;
			if (spawnZone != null) LaunchWave();
		}
	}

	public void GameOver() {
		currentStage = Stage.GameOver;
		paused = true;
		var sum = 0f;
		for (int i = 0; i < currentWave; i++) sum += EnemiesPerTiers*i;
		killCountText!.text = string.Format(killCountFormat, sum - enemyCount);
	}

	void LaunchWave() {
			currentWave++;
			var enemies = spawnZone!.Spawn(SpawnCount(currentWave));
			foreach (var enemy in enemies) {
				enemy.GetComponentInChildren<Health>()?.OnDepleted.AddListener(() => {
					enemyCount--;
					Debug.LogFormat("Killing {0}", enemy.gameObject.name);
				});
				enemy.GetComponentInChildren<Health>()!.rules = GetComponent<RulesManager>();
				enemy.GetComponentInChildren<ShipController>()!.rules = GetComponent<RulesManager>();
			}
			enemyCount = enemies.Count;
			Debug.LogFormat("Enemy count = {0}", enemyCount);
	}

	void UpdateTimeScales(bool isPaused) {
		var target = isPaused ? 0 : 1;
		Time.timeScale = Mathf.Lerp(Time.timeScale, target, Time.unscaledDeltaTime * 3f);
		Time.fixedDeltaTime = baseFixedDT * Time.timeScale;
	}


	float fadeValue = 0f;


	void UpdateFade(bool isPaused) {
		var targetAlpha = isPaused ? 1f : 0f;
		fadeValue = Mathf.Lerp(fadeValue, targetAlpha, Time.unscaledDeltaTime * 2f);
		fade!.color = fadeGradient.Evaluate(fadeValue);
		foreach (var menu in menus) menu?.gameObject.SetActive(false);
		menus[(int)currentStage]?.gameObject.SetActive(fadeValue > menusFadeThreshold);
	}

	int SpawnCount(int level) => level * EnemiesPerTiers;

	public void ReloadScene() {
		Debug.Log("Retry");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GoToMainMenu() {
		Debug.Log("Main Menu");
		SceneManager.LoadScene("MainMenu");
	}

	public void Quit() {
		Debug.Log("Quit");
		Application.Quit();
	}

	public UnityEvent<int> OnNewWave = new UnityEvent<int>();

}
