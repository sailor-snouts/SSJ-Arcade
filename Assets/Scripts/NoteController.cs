using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float speed = 1f;
    
    void Update()
    {
        this.transform.position -= transform.up * this.speed * Time.deltaTime;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
