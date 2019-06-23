using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private int score = 0;
    private int combo = 0;
    private float multiplier = 1f;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI comboText;

    [SerializeField]
    private Pickup redPickup;
    [SerializeField]
    private Pickup bluePickup;
    [SerializeField]
    private Pickup greenPickup;
    [SerializeField]
    private Pickup missedPickup;
    
    void Start()
    {
    }
    
    void Update()
    {
        int hit = 0;
        if(missedPickup.PlayNote() != 0)
        {
            Debug.Log("missed a note");
            this.combo = 0;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Q))
        {
            hit = this.redPickup.PlayNote();
            if (hit > 0)
            {
                this.ScoreUp(hit);
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.W))
        {
            hit = this.greenPickup.PlayNote();
            if (hit > 0)
            {
                this.ScoreUp(hit);
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.E))
        {
            hit = this.bluePickup.PlayNote();
            if (hit > 0)
            {
                this.ScoreUp(hit);
            }
        }
    }

    void ScoreUp(int hit)
    {
        this.combo++;
        this.multiplier = Mathf.Clamp(this.combo % 4f, 1, 10);
        switch(hit)
        {
            case 1:
                this.score += (int) this.multiplier * 100; 
                break;
            case 2:
                this.score += (int) this.multiplier * (int) this.multiplier * 100;
                break;
        }
    }

    private void OnGUI()
    {
        this.scoreText.SetText("Score: \n {0}", this.score);
        this.comboText.SetText("Combo: \n {0}", this.combo);
    }
}
