using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private int score;
    private int combo;
    private float multiplier = 1f;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI comboText;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int GetScore()
    {
        return score;
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

    private void OnGUI()
    {
        this.scoreText.SetText("{0}", this.score);
        this.comboText.SetText("{0}", this.combo);
    }
}
