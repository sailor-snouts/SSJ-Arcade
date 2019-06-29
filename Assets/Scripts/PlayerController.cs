using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    ScoreController score;
    [SerializeField]
    private Pickup redPickup;
    [SerializeField]
    private Pickup greenPickup;
    [SerializeField]
    private Pickup bluePickup;
    [SerializeField]
    private Pickup yellowPickup;
    [SerializeField]
    private Pickup missedPickup;

    private void Start()
    {
        this.score = FindObjectOfType<ScoreController>();
    }

    void Update()
    {
        int hit = 0;

        if(Input.GetKey(KeyCode.Escape))
        {
            SceneChange scenechange = FindObjectOfType<SceneChange>();
            scenechange.FadeToLevel("Title");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Q))
        {
            hit = this.redPickup.PlayNote();
            if (hit > 0)
            {
                this.score.ScoreUp(hit);
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.W))
        {
            hit = this.greenPickup.PlayNote();
            if (hit > 0)
            {
                this.score.ScoreUp(hit);
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.E))
        {
            hit = this.bluePickup.PlayNote();
            if (hit > 0)
            {
                this.score.ScoreUp(hit);
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.R))
        {
            hit = this.yellowPickup.PlayNote();
            if (hit > 0)
            {
                this.score.ScoreUp(hit);
            }
        }
    }
}
