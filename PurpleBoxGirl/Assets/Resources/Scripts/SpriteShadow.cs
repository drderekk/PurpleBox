using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {
	public Vector2 shadowOffset = new Vector2(-0.3f, -0.1f);

	private SpriteRenderer sprRndCaster;
	private SpriteRenderer sprRndShadow;

	private Transform transformCaster;
	private Transform transformShadow;

	public Material shadowMaterial;
	public Color shadowColor;

	public bool updatePosition = true;

	// Use this for initialization
	void Start () {
		transformCaster = transform;
		transformShadow = new GameObject().transform;
		transformShadow.parent = transformCaster;
		transformShadow.gameObject.name = "Shadow";
		transformShadow.localRotation = Quaternion.identity;

		sprRndCaster = GetComponent<SpriteRenderer> ();
		sprRndShadow = transformShadow.gameObject.AddComponent<SpriteRenderer> ();

		sprRndShadow.material = shadowMaterial;
		sprRndShadow.color = shadowColor;
		sprRndShadow.sortingLayerName = "Background";
		sprRndShadow.sortingOrder = 1;

		transformShadow.position = new Vector2 (transformCaster.position.x + shadowOffset.x, transformCaster.position.y + shadowOffset.y);
		sprRndShadow.sprite = sprRndCaster.sprite;
		transformShadow.localScale = new Vector3(1.2f,1.1f,0f);
	}

	void LateUpdate () {
		if (updatePosition) {
			transformShadow.position = new Vector2 (transformCaster.position.x + shadowOffset.x, transformCaster.position.y + shadowOffset.y);
			sprRndShadow.sprite = sprRndCaster.sprite;
		}
	}
}
