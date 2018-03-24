using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConvertObjectsIntoPrefabs : MonoBehaviour {
	public Transform[] objectsToConvert;
	public GameObject thingToReplaceWith;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			foreach (Transform nestedPrefab in objectsToConvert) {
				if (child.name == nestedPrefab.name) {
					Vector3 childPos = child.position;
					Quaternion childRot = child.rotation;

					Destroy(child.gameObject);
					Instantiate(thingToReplaceWith, childPos, childRot, transform);
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
