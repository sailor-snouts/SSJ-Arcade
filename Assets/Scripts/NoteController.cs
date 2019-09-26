using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NoteController : MonoBehaviour
{
    public bool hit = false;
    public float speed = 1f;
    private Animator anim;
    private BoxCollider2D box;

    private void Start()
    {
        this.anim = GetComponent<Animator>();
        this.box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        this.transform.position -= transform.up * this.speed * Time.deltaTime;
    }

    public void Pulse()
    {
        this.hit = true;
        this.speed = 0;
        this.anim.SetTrigger("Hit");
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
