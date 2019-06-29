using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score;
    private int combo;
    private float multiplier = 1f;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int GetScore()
    {
        return score;
    }

    public int GetCombo()
    {
        return this.combo;
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            this.score = 0;
            this.combo = 0;
        }
    }

    public void ScoreUp(int hit)
    {
        this.combo++;
        this.multiplier = Mathf.Clamp(this.combo % 4f, 1, 10);
        switch (hit)
        {
            case 1:
                this.score += (int)this.multiplier * 100;
                break;
            case 2:
                this.score += (int)this.multiplier * (int)this.multiplier * 100;
                break;
        }
    }

    public void ComboBreak()
    {
        this.combo = 0;
    }
}
