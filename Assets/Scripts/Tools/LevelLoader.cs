using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelLoader
{
    [Serializable]
    private class LevelsData
    {
        public Level[] levels;
    }

    private LevelsData levelsData;

    public LevelLoader(TextAsset levelsData)
    {
        LoadLevelsFromAsset(levelsData);
    }

    private void LoadLevelsFromAsset(TextAsset levelsDataFile)
    {
        levelsData = JsonUtility.FromJson<LevelsData>(levelsDataFile.ToString());
    }

    public Level LoadLevel(int levelIndex)
    {
        if (levelIndex > levelsData.levels.Length)
        {
            throw new ArgumentException($"Level {levelIndex} not found");
        }

        return levelsData.levels[levelIndex];
    }

    public int GetNumberOfLevels()
    {
        return levelsData.levels.Length;
    }
}
