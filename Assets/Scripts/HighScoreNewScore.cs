using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreNewScore : MonoBehaviour
{
    HighScore highScoreScript;
    private string initials = "";

    void Awake()
    {
        highScoreScript = FindObjectOfType<HighScore>();       
    }

    //public void addInitial(string c) {
    //    initials.Concat(c);
    //}

    void completeScoreAndContinue(Score playersScore) {
        // Uncomment this before enabling!
        playersScore.initials = initials;
        highScoreScript.recordScoreAndContinue(playersScore);
        Destroy(gameObject);
    }



        private string nextButton = "Fire1";
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.-?!+*(=)";
        private int stepper = 0;
        private int letterSelect = 0;
        public TextMesh[] Letters = null;
        public float moveDelay = 1.0f;
        private bool readyToMove = true;
        private Color selectedColor = Color.yellow;
        //private GameObject nextText;
 
        void Start ()
        {
                Letters [letterSelect].text = alphabet [stepper].ToString ();
                Input.ResetInputAxes ();
                Cursor.visible = false;
                //CameraFade cF = FindObjectOfType (typeof(CameraFade)) as CameraFade;
                //if (cF)
                //        cF.FadeIn (2.5f);
                //nextText = GameObject.Find ("button");
        }
 
        void Update ()
        {
                if (SystemInfo.deviceType == DeviceType.Handheld && readyToMove || Input.GetKey ("up") && readyToMove) {
                        if (stepper < alphabet.Length - 1) {
                                stepper++;
                                Letters [letterSelect].text = alphabet [stepper].ToString ();
                                readyToMove = false;
                                Invoke ("ResetReadyToMove", moveDelay);
                        }
                }
                if (SystemInfo.deviceType == DeviceType.Handheld && readyToMove || Input.GetKey ("down") && readyToMove) {
                        if (stepper > 0) {
                                stepper--;
                                Letters [letterSelect].text = alphabet [stepper].ToString ();
                                readyToMove = false;
                                Invoke ("ResetReadyToMove", moveDelay);
                        }
                }
                if (Input.GetButton (nextButton) && readyToMove) {
                        if (letterSelect <= Letters.Length - 1) {
                                initials = initials + alphabet [stepper].ToString (); // add current letter to string
                                // if the last letter is reached then add initials
                                if (letterSelect == Letters.Length - 1) {
                                        letterSelect = 3; // breaks loop then sets name
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
                                completeScoreAndContinue(new Score(999, initials, System.DateTime.Now.ToString()));
                        }
                }
        }
 
        void ResetReadyToMove ()
        {
                readyToMove = true;
        }
}
