using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboReset : MonoBehaviour
{
    [SerializeField] private ScoreController score;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Note") && other.gameObject.GetComponent<NoteController>().hit == false)
        {
                Debug.Log(other.gameObject);
                this.score.ComboBreak();
        }
    }
}
