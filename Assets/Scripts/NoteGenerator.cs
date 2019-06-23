using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject RedNote;
    [SerializeField]
    private GameObject GreenNote;
    [SerializeField]
    private GameObject BlueNote;
    [SerializeField]
    private Transform RedNoteSpawn;
    [SerializeField]
    private Transform GreenNoteSpawn;
    [SerializeField]
    private Transform BlueNoteSpawn;
    [SerializeField]
    private TextAsset notesDataFile;

    private int bpm = 60;
    private int velocity = 10;

    void Start()
    {
        string[] lines = this.notesDataFile.text.Split('\n');
        this.bpm = int.Parse(lines[0]);
        this.velocity = int.Parse(lines[1]);

        float offset = (60 / this.bpm) * this.velocity;

        for (int i = 1; i < lines.Length; i++)
        {
            if(lines[i][0] == '1')
            {
                GameObject red = Instantiate(this.RedNote, this.RedNoteSpawn);
                red.GetComponent<NoteController>().speed = this.velocity;
                red.transform.position += Vector3.up * i * offset;
            }
            if (lines[i][1] == '1')
            {
                GameObject green = Instantiate(this.GreenNote, this.GreenNoteSpawn);
                green.GetComponent<NoteController>().speed = this.velocity;
                green.transform.position += Vector3.up * i * offset;
            }
            if (lines[i][2] == '1')
            {
                GameObject blue = Instantiate(this.BlueNote, this.BlueNoteSpawn);
                blue.GetComponent<NoteController>().speed = this.velocity;
                blue.transform.position += Vector3.up * i * offset;
            }
        }
    }
}
