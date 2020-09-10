using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typingNote : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    static GameManager gameManager;
    float time;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim.speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
            gameManager.ID_p.Remove(gameObject.GetInstanceID());
            Destroy(gameObject);
            gameManager.ConnectNote();
        }
    }

    public void Initalize(Transform Parent, Vector2 size, char text) {
        gameObject.transform.SetParent(Parent, false);
        gameObject.GetComponent<RectTransform>().sizeDelta = size;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = text.ToString();
        gameObject.name = "Note_" + gameObject.GetInstanceID();
    }
}
