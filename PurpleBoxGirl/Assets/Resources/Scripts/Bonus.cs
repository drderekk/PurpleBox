using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {
    private GameObject BonusExplosion;

    private SpriteRenderer Sprite;
    private AudioController Audio;

    private LevelManager LevelManager;

    public int BonusScore = 5;

    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;

    public Vector2 Pos1;
    public Vector2 Pos2;
    public Vector3 Pos3;

    private int currentPos;

    void Start ()
    {
        BonusExplosion = Resources.Load<GameObject>("Particles/BonusExplosion");

        Audio = FindObjectOfType<AudioController>();
        Pos1 = gameObject.transform.position;
        Pos2 = gameObject.transform.Find("Pos2").transform.position;
        Pos3 = gameObject.transform.Find("Pos3").transform.position;
        LevelManager = FindObjectOfType<LevelManager>();
        Sprite = GetComponent<SpriteRenderer>();

        currentPos = 1;
        Sprite.sprite = Sprite1;
    }

    public void Reset ()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = Pos1;
        Sprite.sprite = Sprite1;

        currentPos = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            Vector3 ExplosionPosition = Pos1;

            switch (currentPos)
            {
                case 1:
                    gameObject.transform.position = Pos2;
                    ExplosionPosition = Pos1;
                    Sprite.sprite = Sprite2;
                    currentPos++;
                    break;
                case 2:
                    gameObject.transform.position = Pos3;
                    ExplosionPosition = Pos2;
                    Sprite.sprite = Sprite3;
                    currentPos++;
                    break;
                case 3:
                    currentPos = 1;
                    ExplosionPosition = Pos3;
                    gameObject.transform.position = new Vector3(0, -100, 0);
                    LevelManager.Score = LevelManager.Score + BonusScore;
                    break;
            }

            GameObject explosion = Instantiate(BonusExplosion, ExplosionPosition, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f), transform);
            ParticleSystem explosionParticles = explosion.GetComponent<ParticleSystem>();
            float totalDuration = explosionParticles.main.duration + explosionParticles.main.startLifetime.constantMax;

            Destroy(explosionParticles, totalDuration);

            Audio.PlayBonusSound(currentPos);
        }

    }
}
