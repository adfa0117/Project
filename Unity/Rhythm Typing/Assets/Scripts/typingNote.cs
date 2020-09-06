using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class typingNote : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    static int recentNoteId;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
