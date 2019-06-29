using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreNewScore : MonoBehaviour
{
    private string initials = "";
    // Change this value to the key to confirm initial selection
    private string nextButton = "Fire1";
    // List of valid characters
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.-?!+*(=)";
    private int stepper = 0;
    private int letterSelect = 0;
    public Text[] Letters = null;
    public float moveDelay = 1.0f;
    private bool readyToMove = true;
    private Color selectedColor = Color.yellow;
    private int playerScore = 0;

    void Awake()
    {   
        // ADD PULL FROM PERSISTED SCORE CLASS HERE, SET playerScore WITH IT    
    }

    void completeScoreAndContinue(Score playersScore) 
    {
        FindObjectOfType<HighScore>().recordScoreAndContinue(playersScore);
        Destroy(gameObject);
    }
    void Start ()
    {
        Letters [letterSelect].text = alphabet [stepper].ToString ();
        Input.ResetInputAxes ();
        Cursor.visible = false;
    }

    void Update ()
    {
        if (Input.GetKey ("up") && readyToMove) {
                if (stepper < alphabet.Length - 1) {
                        stepper++;
                        letterChange();
                } else {
                        if(stepper == alphabet.Length - 1) {
                                stepper = 0;
                                letterChange();
                        }
                }
        }
        if (Input.GetKey ("down") && readyToMove) {
            if (stepper > 0) {
                stepper--;
                letterChange();
                } else {
                        if(stepper == 0) {
                                stepper = alphabet.Length - 1;
                                letterChange();
                        }
                }
        }
        if (Input.GetButton (nextButton) && readyToMove) {
                if (letterSelect <= Letters.Length - 1) {
                        initials = initials + alphabet [stepper].ToString (); // add current letter to string
                        // if the last letter is reached then add initials
                        if (letterSelect == Letters.Length - 1) {
                                letterSelect = 3; // breaks loop then sets name
                                completeScoreAndContinue(new Score(playerScore, initials, System.DateTime.Now.ToString()));
                        }
                        // keep on till the very last letter
                        if (letterSelect < Letters.Length - 1) {
                                letterSelect++;
                                Letters [letterSelect].color = Color.white; // resets alpha
                                Letters [letterSelect - 1].color = selectedColor;
                                readyToMove = false;
                                Invoke ("ResetReadyToMove", moveDelay);
                        }
                        stepper = 0; // stepper is reset for next run
                }
        }
    }

    void letterChange() 
    {
        Letters [letterSelect].text = alphabet [stepper].ToString ();
        readyToMove = false;
        Invoke ("ResetReadyToMove", moveDelay);
    }
 
    void ResetReadyToMove ()
    {
        readyToMove = true;
    }
}
