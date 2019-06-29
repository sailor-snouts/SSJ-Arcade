using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] runes;
    [SerializeField]
    private GameObject endNote;
    [SerializeField]
    private Transform redNoteSpawn;
    [SerializeField]
    private Transform greenNoteSpawn;
    [SerializeField]
    private Transform blueNoteSpawn;
    [SerializeField]
    private Transform yellowNoteSpawn;
    [SerializeField]
    private TextAsset notesDataFile;

    private float bpm = 60;
    private float velocity = 10;

    void Start()
    {
        string[] lines = this.notesDataFile.text.Split('\n');
        this.bpm = float.Parse(lines[0]);
        this.velocity = float.Parse(lines[1]);

        float offset = (60 / this.bpm) * this.velocity;

        int i;
        for (i = 2; i < lines.Length; i++)
        {
            if(lines[i][0] == '1')
            {
                GameObject red = Instantiate(this.runes[0], this.redNoteSpawn);
                red.GetComponent<NoteController>().speed = this.velocity;
                red.transform.position += Vector3.up * i * offset;
            }
            if (lines[i][1] == '1')
            {
                GameObject green = Instantiate(this.runes[1], this.greenNoteSpawn);
                green.GetComponent<NoteController>().speed = this.velocity;
                green.transform.position += Vector3.up * i * offset;
            }
            if (lines[i][2] == '1')
            {
                GameObject blue = Instantiate(this.runes[2], this.blueNoteSpawn);
                blue.GetComponent<NoteController>().speed = this.velocity;
                blue.transform.position += Vector3.up * i * offset;
            }
            if (lines[i][3] == '1')
            {
                GameObject yellow = Instantiate(this.runes[3], this.yellowNoteSpawn);
                yellow.GetComponent<NoteController>().speed = this.velocity;
                yellow.transform.position += Vector3.up * i * offset;
            }
        }

        GameObject end = Instantiate(this.endNote, this.redNoteSpawn);
        end.GetComponent<NoteController>().speed = this.velocity;
        end.transform.position += Vector3.up * i * offset;
    }
}
