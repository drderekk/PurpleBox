using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceTaggedObjectsInLevelWithPrefab : MonoBehaviour {
	public string[] tagsInOrder;
	public GameObject[] prefabsInOrder;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			Vector3 childPos = child.position;
			Quaternion childRot = child.rotation;

			for (int i = 0; i < tagsInOrder.Length; i++)
			{
				if(child.CompareTag(tagsInOrder[i])){
					Destroy (child.gameObject);
					Instantiate (prefabsInOrder[i], childPos, childRot, transform);
					break;
				}
			}

		}

	}

}
