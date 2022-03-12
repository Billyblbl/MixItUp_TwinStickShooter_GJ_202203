using System.Reflection;
using UnityEngine;

#nullable enable

public static class UnityObjectExtension {
	public static T InstantiateOff<T>(T unityObject, Vector3 position, Quaternion rotation) where T : UnityEngine.Object {

		//Find prefab gameObject
		var gameObject = unityObject as GameObject;
		var component = unityObject as Component;

		if (gameObject == null && component != null)
			gameObject = component.gameObject;

		//Save current prefab active state
		var isActive = false;
		if (gameObject != null) {
			isActive = gameObject.activeSelf;
			//Deactivate
			gameObject.SetActive(false);
		}

		//Instantiate
		var obj = Object.Instantiate(unityObject, position, rotation) as T;

		//Set active state to prefab state
		if (gameObject != null)
			gameObject.SetActive(isActive);

		return obj;
	}
}
