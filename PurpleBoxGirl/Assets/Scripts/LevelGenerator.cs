using System;
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

		/*
		 * Next level is generated when camera reaches midpoint of level
		 */
		NextGenerate = levelWidth / 2;

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
			return;
        }

		generateNextLevel();
    }

	void generateNextLevel()
    {
		float cameraXPos = Mathf.Round(mainCamera.transform.position.x);

		// If camera has passed the position for generating the next level
		if(cameraXPos > NextGenerate)
        {
			int levelIndex = 0;

			int maxNumOfLoops = 20;

			for(int i = 0; i < maxNumOfLoops; i++)
            {
				// Uses random number generator to generate index of next level to generate
				levelIndex = UnityEngine.Random.Range(0, LevelList.Count);

				// Exits loop if level index hasn't been recently seen
                if (!checkIfLevelPreviouslySeen(levelIndex))
                {
					break;
                }
			}

			// Removes the least recent level index from previouslySeenLevels
			previouslySeenLevels.RemoveAt(0);

			// Adds the new level index to previouslySeenLevels
			previouslySeenLevels.Add(levelIndex);

			// Initialises the new level at the correct position
			Instantiate(LevelList[levelIndex], new Vector3(LevelPos, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f), transform);

			LevelPos = LevelPos + levelWidth;
			NextGenerate = NextGenerate + levelWidth;
		}
	}

	/**
	 * Returns 'true' if a given level index is already stored within
	 * the 'previouslySeenLevels' list.
	 */
	bool checkIfLevelPreviouslySeen(int levelIndex)
    {
		return previouslySeenLevels.Contains(levelIndex);
    }
}
