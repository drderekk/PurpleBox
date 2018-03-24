using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {
	//public Camera camera;
	public Vector2 shadowOffset = new Vector2(-0.3f, -0.1f);

	private SpriteRenderer sprRndCaster;
	private SpriteRenderer sprRndShadow;

	private Transform transformCaster;
	private Transform transformShadow;

	public Material shadowMaterial;
	public Color shadowColor;

	// Use this for initialization
	void Start () {
		//camera = FindObjectOfType<Camera>();
		transformCaster = transform;
		transformShadow = new GameObject ().transform;
		transformShadow.parent = transformCaster;
		transformShadow.gameObject.name = "Shadow";
		transformShadow.localRotation = Quaternion.identity;

		sprRndCaster = GetComponent<SpriteRenderer> ();
		sprRndShadow = transformShadow.gameObject.AddComponent<SpriteRenderer> ();

		sprRndShadow.material = shadowMaterial;
		sprRndShadow.color = shadowColor;
		sprRndShadow.sortingLayerName = "Background";
		sprRndShadow.sortingOrder = 1;
	}

	void LateUpdate () {
		/*
		Vector3 cameraPos = camera.transform.position;
		cameraPos.x = cameraPos.x;

		float distance = transformCaster.position.x - cameraPos.x;

		//shadowOffset.x = Vector3.MoveTowards(transformCaster.position, cameraPos, distance/3f).x;
		shadowOffset.x = (transformCaster.position.x - cameraPos.x)/10f;
		shadowOffset.y = (transformCaster.position.y - cameraPos.y)/10f;
		*/
		transformShadow.position = new Vector2 (transformCaster.position.x + shadowOffset.x, transformCaster.position.y + shadowOffset.y);
		transformShadow.localScale = new Vector3(1.2f,1.2f,0f);
		sprRndShadow.sprite = sprRndCaster.sprite;
	}
}
