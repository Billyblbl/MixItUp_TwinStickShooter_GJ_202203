using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

#nullable enable

public class RulesManager : MonoBehaviour {

	public enum Trigger {
		Shoot,
		Damage,
		Kill,
		Dash,
		Collision,
		PlayerMovement
	}

	// public enum Reaction {
	// 	Explosion,
	// 	LightningChain,
	// 	HealZone,
	// 	Shield,
	// 	SpawnPlayerBullets,
	// 	SpawnEnemyBullets,
	// 	EMP
	// }

	// [System.Serializable] public struct ReactionObj {
	// 	public string name;
	// 	public GameObject effect;

	// }

	[System.Serializable] public class Rule {
		public Trigger trigger;
		public GameObject? reaction;
		// public ReactionObj reactionObj;
	}

	public List<Rule>	rules = new List<Rule>();
	public int maxRules = 5;

	public void RandomizeRuleSet() {
		if (rules.Count < maxRules) NewRandomRule();
		else for (int i = 0; i < maxRules; i++) {
			RandomizeRule(i);
		}
	}

	public void NotifyRule(Trigger trigger, Vector2 position) {
		foreach (var rule in rules.Where((rule) => rule.trigger == trigger)) {
			Vector3 spawnPoint = position;
			Instantiate(rule.reaction, spawnPoint, Quaternion.identity);
		}
	}

	public VerticalLayoutGroup? layout;
	public RuleUIElement?	ruleUI;

	static Trigger[] TriggerValues = (Trigger[])System.Enum.GetValues(typeof(Trigger));
	public List<GameObject> Effects = new List<GameObject>();

	public void RandomizeRule(int index) {
		Debug.LogFormat("{0} rules, randomizing {1}", rules.Count, index);
		rules[index].trigger = TriggerValues[Random.Range(0, TriggerValues.Length)];
		rules[index].reaction = Effects[Random.Range(0, Effects.Count)];
		UpdateUI();
	}

	public void UpdateUI() {
		foreach (Transform child in layout!.transform) {
			GameObject.Destroy(child.gameObject);
		}

		for (int i = 0; i < rules.Count; i++) {
			var ui = Instantiate(ruleUI, layout!.transform);
			if (ui != null) {
				ui.Fill(rules[i]);
				ui.ruleIndex = i;
				ui.randomizeButton?.onClick.AddListener(() => RandomizeRule(ui.ruleIndex));
			}
		}
	}

	public void NewRandomRule() {
		rules.Add(new Rule());
		RandomizeRule(rules.Count - 1);
	}

}
