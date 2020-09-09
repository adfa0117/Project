using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class typingNote : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    static GameManager gameManger;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
            gameManger.IDs.Dequeue();
            Destroy(gameObject);
        }
    }
}
