using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

public class RuleUIElement : MonoBehaviour {
	public TextMeshProUGUI?	triggerLabel;
	public TextMeshProUGUI?	reactionLabel;
	public Button?	randomizeButton;
	public int ruleIndex;

	public void Fill(RulesManager.Rule rule) {
		triggerLabel!.text = rule.trigger.ToString();
		reactionLabel!.text = rule.reaction.ToString();
	}
}
