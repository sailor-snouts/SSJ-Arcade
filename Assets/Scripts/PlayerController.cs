using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ScoreController score;
    [SerializeField] private Pickup redPickup;
    [SerializeField] private Pickup greenPickup;
    [SerializeField] private Pickup bluePickup;
    [SerializeField] private Pickup yellowPickup;
    [SerializeField] private Animator eggAnimator;
    [SerializeField] private Animator pentagramAnimator;

    private AudioSource audio;
    private bool startedPlaying = false;

    private void Start()
    {
        this.score = FindObjectOfType<ScoreController>();
        this.audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!this.startedPlaying)
        {
            this.startedPlaying = true;
            this.audio.Play();
        }
        int hit = 0;

        if(Input.GetKey(KeyCode.Escape))
        {
            SceneChange scenechange = FindObjectOfType<SceneChange>();
            scenechange.FadeToLevel("Title");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Q))
        {
            if (this.redPickup.PlayNote() > 0) this.Hit();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.W))
        {
            if (this.greenPickup.PlayNote() > 0) this.Hit();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.E))
        {
            if (this.bluePickup.PlayNote() > 0) this.Hit();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.R))
        {
            if (this.yellowPickup.PlayNote() > 0) this.Hit();
        }
    }

    private void Hit()
    {
        this.score.ScoreUp(1);
        this.eggAnimator.SetTrigger("Hit");
        this.pentagramAnimator.SetTrigger("Hit");
    }
}
