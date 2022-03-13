using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class HealthBar : MonoBehaviour {

	public RectTransform?	fill;

	public float value { set {
		fill!.localScale = new Vector3(value, fill.localScale.y, fill.localScale.z);
	}}
}
