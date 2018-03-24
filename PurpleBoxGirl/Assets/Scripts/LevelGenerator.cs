using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public List<GameObject> LevelList = new List<GameObject>();

    public Camera mainCamera;

    public int NextGenerate;

    public int LevelPos;

    public bool Restart;

	public static int levelWidth = 26;

	private List<int> previouslySeenLevels = new List<int>();
	private static int numberOfPreviousLevelsToTrack = 3;

	void Start () {
		LevelPos = levelWidth;
		NextGenerate = levelWidth/2;
		Restart = false;

		for (int i = previouslySeenLevels.Count; i < numberOfPreviousLevelsToTrack; i++)
		{
			previouslySeenLevels.Add(-1);
		}
    }
	
	void Update () {
        if (Restart)
        {
			Start ();
        }
        else
        {
			float CameraPos = Mathf.Round(mainCamera.transform.position.x);

            if (CameraPos > NextGenerate)
            {
				bool previouslySeen;
				int LevelIndex;

				int loops = 0;

				/*
				 * Loops until it finds a level index that isn't stored in
				 * previouslySeenLevels.
				 */
				do {
					loops++;

					if(loops > 20){
						break;
					}
					previouslySeen = false;

					LevelIndex = UnityEngine.Random.Range (0, LevelList.Count);

					for (int i = 0; i < previouslySeenLevels.Count; i++) {
						if(previouslySeenLevels[i] == LevelIndex){
							previouslySeen = true;
						}
					}

				} while (previouslySeen);

				// Removes the least recent level index from previouslySeenLevels
				previouslySeenLevels.RemoveAt(0);

				// Adds the new level index to previouslySeenLevels
				previouslySeenLevels.Add(LevelIndex);

                Instantiate(LevelList[LevelIndex], new Vector3(LevelPos, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f), transform);

				LevelPos = LevelPos + levelWidth;

				NextGenerate = NextGenerate + levelWidth;
            }
        }

    }
}
