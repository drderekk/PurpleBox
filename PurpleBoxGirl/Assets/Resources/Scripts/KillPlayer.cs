using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    public LevelManager LevelManager;

    void Start () {
		LevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.name == "Player") {
			LevelManager.PlayerDeath();
		}
    }
}
