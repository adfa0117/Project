using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    float angle = 0;
    float prevAng, nextAng;

    // Start is called before the first frame update
    void Start()
    {
        angle = 0;
        StartCoroutine("Spin");
    }

    // Update is called once per frame
    void Update()
    {
        angle = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
        transform.position += new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0)* Time.deltaTime*20;


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "InBox")
        {
            prevAng = transform.rotation.eulerAngles.z % 360;
            Vector3 pos = transform.position;
            if (Mathf.Abs(pos.x) >= 13.5)
            {
                nextAng = -prevAng;
            }
            else if (Mathf.Abs(pos.y) >= 6.5)
            {
                nextAng = 180 - prevAng;
            }
            transform.Rotate(0, 0, nextAng - prevAng);
        }
        else if (collision.transform.tag == "MiddleBox") {
            prevAng = transform.rotation.eulerAngles.z % 360;
            while (prevAng < 0) prevAng += 360;
            prevAng %= 180;
            Vector3 pos = transform.position;
            if (Mathf.Abs(pos.x) >= 14.5)
            {
                if (prevAng == 90)
                    transform.Rotate(0, 0, 180);
                else if (prevAng < 90)
                    transform.Rotate(0, 0, -90);
                else if (prevAng > 90)
                    transform.Rotate(0, 0, 90);
            }
            else if (Mathf.Abs(pos.y) >= 7.5)
            {
                if (prevAng == 0)
                    transform.Rotate(0, 0, 180);
                else if (prevAng < 90)
                    transform.Rotate(0, 0, 90);
                else if (prevAng > 90)
                    transform.Rotate(0, 0, -90);
            }
        }
        else if (collision.transform.tag == "OutBox")
        {
            transform.Rotate(0, 0, 180);
        }

    }

    IEnumerator Spin() {
        float delay;
        while (true) {
            delay = Random.Range(0.2f, 1.0f);
            transform.Rotate(0, 0, Random.Range(-60.0f, 60.0f));

            yield return new WaitForSeconds(delay);
        }
    }
}
