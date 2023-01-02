using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : PersistentSingleton<LevelManager>
{
    [SerializeField] private int score;

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

}
