using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public List<GameObject> LevelList = new List<GameObject>();

    public float CameraPos;

    public int NextGenerate;

    public int LevelPos;

    public bool Restart;

	public static int levelWidth = 26;

	void Start () {
		LevelPos = levelWidth;
		NextGenerate = levelWidth/2;
		Restart = false;
    }
	
	void Update () {
        if (Restart)
        {
			Start ();
        }
        else
        {
            CameraPos = Mathf.Round(FindObjectOfType<Camera>().transform.position.x);

            int LevelIndex = UnityEngine.Random.Range(0, 5);

            if (CameraPos > NextGenerate)
            {
                Instantiate(LevelList[LevelIndex], new Vector3(LevelPos, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f), transform);

				LevelPos = LevelPos + levelWidth;

				NextGenerate = NextGenerate + levelWidth;
            }
        }

    }
}
