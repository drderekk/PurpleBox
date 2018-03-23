using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour {

    public GameObject LevelStartPoint;
    public Vector3 CameraStartPoint;

    private LevelGenerator LevelGenerator;
    public GameObject RespawnCanvas;
    public GameObject ScoreDisplayCanvas;

    GameObject[] Levels;

    private PlayerBehaviour Player;
    private AudioController Audio;

    public int Score;
    private bool PlayerAlive;

    private CameraMovement CameraMovement;

    private Bonus Bonus;

    void Start () {
        Bonus = FindObjectOfType<Bonus>();
        ScoreDisplayCanvas = GameObject.Find("ScoreTextCanvas");
        RespawnCanvas = GameObject.Find("RespawnCanvas");
        RespawnCanvas.gameObject.SetActive(false);
        CameraMovement = FindObjectOfType<CameraMovement>();
        LevelGenerator = FindObjectOfType<LevelGenerator>();
        Player = FindObjectOfType<PlayerBehaviour>();
        Audio = FindObjectOfType<AudioController>();
        CameraStartPoint = Camera.main.transform.position;
        LevelStartPoint = GameObject.Find("StartPoint");
        Audio.Music.Play();
        PlayerAlive = true;
        StartCoroutine("ScoreCount");
    }

    public bool IsBoxActive;
	void Update () {
        if (!Player.gameObject.activeInHierarchy)
        {
            IsBoxActive = false;
        }
        else IsBoxActive = true;
	}

    public void PlayerDeath ()
    {
        Player.gameObject.SetActive(false);
        ScoreDisplayCanvas.gameObject.SetActive(false);
        PlayerAlive = false;
        CameraMovement.Move = false;
		CameraMovement.shakeCamera(15, 15);
        RespawnCanvas.gameObject.SetActive(true);

    }

    void DestroyLevels()
    {
        GameObject[] Levels = GameObject.FindGameObjectsWithTag("Level1");
        foreach (GameObject Level in Levels)
            Destroy(Level);
    }

    public void RespawnPlayer() {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        Score = 0;
        Player.transform.position = LevelStartPoint.transform.position;

		RespawnCanvas.gameObject.SetActive(false);
        Bonus.Reset();

        // Sets the amount of time the camera should take to return to the start based on its distance from the start
        float distance = Camera.main.transform.position.x - CameraStartPoint.x;
		float timeToReturnToStart = Mathf.Min(0.8f, distance/100f);

		Debug.Log (timeToReturnToStart);

		Vector3 cameraPosition = Camera.main.transform.position;
		float timeElapsed = 0;

		/*
		 * Moves the camera from its current position to the start position
		 * based on the time in seconds set by the variable timeToReturnToStart
		 */
		while (timeElapsed < timeToReturnToStart)
		{
			Camera.main.transform.position = Vector3.Lerp(cameraPosition,  CameraStartPoint, (timeElapsed/timeToReturnToStart));
			timeElapsed += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

        Camera.main.transform.position = CameraStartPoint;
        DestroyLevels();
        yield return new WaitForSeconds(0.5f);
        Audio.Respawn.Play();
        Player.gameObject.SetActive(true);
        ScoreDisplayCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        LevelGenerator.Restart = true;
        CameraMovement.StartMove();
        PlayerAlive = true;
        StartCoroutine("ScoreCount");
        Audio.Music.Play();
    }

    public IEnumerator ScoreCount()
    {
        while (PlayerAlive)
        {
            Score++;
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
}
