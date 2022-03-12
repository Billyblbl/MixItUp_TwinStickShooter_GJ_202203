using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#nullable enable

public class Parallax : MonoBehaviour {


	[System.Serializable] public class Layer {
		public SpriteRenderer?	renderer;
		public Vector2	parallaxScale;
		[System.NonSerialized] public Vector2	bounds;
		[System.NonSerialized] public Vector2 startPos;
	};

	public List<Layer> layers = new List<Layer>();

	private void Start() {
		foreach (var layer in layers) {
			if (layer.renderer == null) throw new UnityException("Missing parallax layer sprite attachement");
			layer.startPos = layer.renderer!.transform.position;
			layer.bounds = layer.renderer!.bounds.size;
		}
	}

	private void Update() {
		foreach (var layer in layers) {
			var temp = Vector2.Scale(transform.position, Vector2.one - layer.parallaxScale);
			var offset = Vector2.Scale(transform.position, layer.parallaxScale);
			layer.renderer!.transform.position = layer.startPos + offset;

			if (temp.x > layer.startPos.x + layer.bounds.x) layer.startPos.x += layer.bounds.x;
			if (temp.x < layer.startPos.x - layer.bounds.x) layer.startPos.x -= layer.bounds.x;
			if (temp.y > layer.startPos.y + layer.bounds.y) layer.startPos.y += layer.bounds.y;
			if (temp.y < layer.startPos.y - layer.bounds.y) layer.startPos.y -= layer.bounds.y;
		}
	}



}
