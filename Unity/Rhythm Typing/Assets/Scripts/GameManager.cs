using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;

    void Start()
    {
        StartCoroutine("CreateNote");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Detected key code: " + Event.current);
    }

    IEnumerator CreateNote() {
        while (true) {
            float x = Random.Range(-8.5f, 8.5f);
            float y = Random.Range(-4.5f, 4.5f);
            Instantiate(prefab, new Vector3(x, y, 0), prefab.transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }
}