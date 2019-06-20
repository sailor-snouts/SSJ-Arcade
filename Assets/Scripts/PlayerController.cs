using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private int combo;

    private int playerId = 0;
    private Player player;

    [SerializeField]
    private Pickup redPickup;
    [SerializeField]
    private Pickup bluePickup;
    [SerializeField]
    private Pickup greenPickup;
    [SerializeField]
    private Pickup orangePickup;
    [SerializeField]
    private Pickup pinkPickup;
    [SerializeField]
    private Pickup blackPickup;

    // Start is called before the first frame update
    void Start()
    {
        this.player = ReInput.players.GetPlayer(this.playerId);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.player.GetButtonDown("Red"))
        {
            if(this.redPickup.PlayNote())
            {
                this.combo++;
                this.score++;
            }
        }
        if (this.player.GetButtonDown("Blue"))
        {
            if (this.bluePickup.PlayNote())
            {
                this.combo++;
                this.score++;
            }
        }
        if (this.player.GetButtonDown("Green"))
        {
            if (this.greenPickup.PlayNote())
            {
                this.combo++;
                this.score++;
            }
        }
        if (this.player.GetButtonDown("Orange"))
        {
            if (this.orangePickup.PlayNote())
            {
                this.combo++;
                this.score++;
            }
        }
        if (this.player.GetButtonDown("Pink"))
        {
            if (this.pinkPickup.PlayNote())
            {
                this.combo++;
                this.score++;
            }
        }
        if (this.player.GetButtonDown("Black"))
        {
            if (this.blackPickup.PlayNote())
            {
                this.combo++;
                this.score++;
            }
        }
    }
}
