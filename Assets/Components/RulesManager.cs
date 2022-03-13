using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

#nullable enable

public class RulesManager : MonoBehaviour {

	public enum Trigger {
		Shoot,
		Kill,
		Dash,
		Collision
	}

	public enum Reaction {
		Explosion,
		// LightningChain,
		HealZone,
		Shield,
		SpawnPlayerBullets,
		SpawnEnemyBullets,
	}

	[System.Serializable] public class Rule {
		public Trigger trigger;
		public Reaction reaction;
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
			Instantiate(spawns[rule.reaction], spawnPoint, Quaternion.identity);
		}
	}

	public GameObject? explosion;
	public GameObject? heal;
	public GameObject? lightningChain;
	public GameObject? shield;
	public GameObject? enemyBulletPack;
	public GameObject? playerBulletPack;

	public VerticalLayoutGroup? layout;
	public RuleUIElement?	ruleUI;

	Dictionary<Reaction, GameObject>	spawns = new Dictionary<Reaction, GameObject>();

	static Trigger[] TriggerValues = (Trigger[])System.Enum.GetValues(typeof(Trigger));
	static Reaction[] ReactionValues = (Reaction[])System.Enum.GetValues(typeof(Reaction));

	public void RandomizeRule(int index) {
		rules[index].trigger = TriggerValues[Random.Range(0, TriggerValues.Length)];
		rules[index].reaction = ReactionValues[Random.Range(0, ReactionValues.Length)];
		UpdateUI();
	}

	public void UpdateUI() {
		foreach (Transform child in layout!.transform) {
			GameObject.Destroy(child.gameObject);
		}

		for (int i = 0; i < rules.Count; i++) {
			var ui = Instantiate(ruleUI, layout!.transform);
			ui?.Fill(rules[i]);
			ui?.randomizeButton?.onClick.AddListener(() => RandomizeRule(i));
		}
	}

	public void NewRandomRule() {
		rules.Add(new Rule());
		RandomizeRule(rules.Count - 1);
	}

	private void Start() {
		spawns = new Dictionary<Reaction, GameObject>{
			{Reaction.Explosion, explosion!},
			{Reaction.HealZone, heal!},
			// {Reaction.LightningChain, lightningChain!},
			{Reaction.Shield, shield!},
			{Reaction.SpawnEnemyBullets, enemyBulletPack!},
			{Reaction.SpawnPlayerBullets, playerBulletPack!}
		};
	}

}
