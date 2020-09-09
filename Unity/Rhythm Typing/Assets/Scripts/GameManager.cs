using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public Queue<int> IDs;
    void Start()
    {
        IDs = new Queue<int>();
        StartCoroutine("CreateNote");
    }

    // Update is called once per frame
    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyUp)
        {
            int id = IDs.Dequeue();
            GameObject note = GameObject.Find("Note" + id.ToString());
            Destroy(note);
        }
    }

    IEnumerator CreateNote() {
        while (true) {
            float x = Random.Range(-8.5f, 8.5f);
            float y = Random.Range(-4.5f, 4.5f);
            GameObject note = Instantiate(prefab, new Vector3(x, y, 0), prefab.transform.rotation);
            int id = note.GetInstanceID();
            note.name = "Note" + id.ToString();
            IDs.Enqueue(id);
            yield return new WaitForSeconds(1f);
        }
    }
}