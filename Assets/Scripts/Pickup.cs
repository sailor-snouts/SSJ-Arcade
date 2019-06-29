using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private List<GameObject> notes;
    private SceneChange sceneChange;
    [SerializeField]
    private Animator beam;
    [SerializeField]
    private string nextScene = "Score";

    private void Start()
    {
        this.notes = new List<GameObject>();
        this.sceneChange = FindObjectOfType<SceneChange>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.tag == "Note")
        {
            this.notes.Add(collision.gameObject);
        }
        else if(collision.gameObject.tag == "EndNote")
        {
            this.sceneChange.FadeToLevel(this.nextScene);
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
            if (collision.gameObject.GetComponent<BoxCollider2D>().enabled)
            {
                this.beam.SetTrigger("Miss");
            }
        }
    }

    public int PlayNote()
    {
        float dist = 0;

        if(this.notes.Count > 0)
        {
            for (int i = 0; i < this.notes.Count; i++)
            {
                dist = Vector2.Distance(this.gameObject.transform.position, this.notes[i].gameObject.transform.position);
                this.notes[i].GetComponent<NoteController>().Pulse();
            }

            this.notes = new List<GameObject>();

            // perfect hit
            if(dist < 0.1f)
            {
                this.beam.SetTrigger("Hit");
                return 2;
            }

            this.beam.SetTrigger("Hit");
            return 1;
        }

        return 0;
    }
}
