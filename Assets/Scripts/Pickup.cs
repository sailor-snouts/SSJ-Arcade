using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private List<GameObject> notes;

    private void Start()
    {
        this.notes = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision trigger");
        
        if(collision.gameObject.tag == "Note")
        {
            this.notes.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Note")
        {
            for(int i = 0; i < this.notes.Count; i++)
            {
                if(this.notes[i] == collision.gameObject)
                {
                    this.notes.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public bool PlayNote()
    {
        if(this.notes.Count > 0)
        {
            return true;
        }

        return false;
    }
}
