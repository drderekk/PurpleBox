using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    private SpriteRenderer Sprite;

    private LevelManager LevelManager;

    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;

    public Vector2 Pos1;
    public Vector2 Pos2;
    public Vector3 Pos3;

    public bool IsPos1;
    public bool IsPos2;
    public bool IsPos3;

    void Start ()
    {
        Pos1 = gameObject.transform.position;
        Pos2 = gameObject.transform.Find("Pos2").transform.position;
        Pos3 = gameObject.transform.Find("Pos3").transform.position;
        LevelManager = FindObjectOfType<LevelManager>();
        Sprite = GetComponent<SpriteRenderer>();
        IsPos1 = true;
        Sprite.sprite = Sprite1;
    }

    public void Reset ()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = Pos1;
        Sprite.sprite = Sprite1;
        IsPos1 = true;
        IsPos2 = false;
        IsPos3 = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            if (IsPos1)
            {
                gameObject.transform.position = Pos2;
                Sprite.sprite = Sprite2;
                IsPos1 = false;
                IsPos2 = true;
            }
            else if (IsPos2)
            {
                gameObject.transform.position = Pos3;
                Sprite.sprite = Sprite3;
                IsPos2 = false;
                IsPos3 = true;
            }
            else if (IsPos3)
            {
                IsPos3 = false;
                IsPos1 = true;
                gameObject.SetActive(false);
                LevelManager.Score = LevelManager.Score + 5;
            }
        }

    }
}
