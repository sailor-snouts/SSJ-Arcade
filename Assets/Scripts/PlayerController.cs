using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    private int score;
    private int combo;

    private int playerId = 0;
    private Player player;

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
            Debug.Log(this.player + "Pressed Red");
        }
        if (this.player.GetButtonDown("Blue"))
        {
            Debug.Log(this.player + "Pressed Blue");
        }
        if (this.player.GetButtonDown("Green"))
        {
            Debug.Log(this.player + "Pressed Green");
        }
        if (this.player.GetButtonDown("Orange"))
        {
            Debug.Log(this.player + "Pressed Orange");
        }
        if (this.player.GetButtonDown("Pink"))
        {
            Debug.Log(this.player + "Pressed Pink");
        }
        if (this.player.GetButtonDown("Black"))
        {
            Debug.Log(this.player + "Pressed Black");
        }
    }
}
