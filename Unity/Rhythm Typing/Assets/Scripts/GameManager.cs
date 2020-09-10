using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public Canvas canvas;
    public LineRenderer noteConnector;
    public Stage stage;
    List<int> IDs;
    public List<int> ID_p {
        get { return IDs; }
        set { IDs = value; }
    }
    List<Vector3> notePoses;
    void Start()
    {
        IDs = new List<int>();
        notePoses = new List<Vector3>();
        StartCoroutine("CreateNote");
    }

    // Update is called once per frame
    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyUp)
        {
            if (IDs.Count > 0)
            {
                int id = IDs[0];
                IDs.Remove(id);
                GameObject note = GameObject.Find("Note_" + id.ToString());
                Destroy(note);
                ConnectNote();
            }
        }
    }

    IEnumerator CreateNote() {
        float canvasWidth = 0, canvasHeight = 0;
        float noteX = 0, noteY = 0;
        float prevNoteX = 0, prevNoteY = 0;
        float noteWidth = 0, noteHeight = 0;
        float time = 0;
        char text = 'A';

        for (int i = 0; i < stage.delays.Length; i++) {
            PreSetting(ref canvasWidth, ref canvasHeight, ref noteWidth, ref noteHeight, ref text, ref noteX, ref noteY, ref prevNoteX, ref prevNoteY, ref time, i);

            GameObject note = Instantiate(prefab, new Vector3(noteX, noteY, 0), prefab.transform.rotation);

            note.GetComponent<typingNote>().Initalize(canvas.transform, new Vector2(noteWidth, noteHeight), text);
            IDs.Add(note.GetInstanceID());

            ConnectNote();
            yield return new WaitForSeconds(time);
        }
    }

    public void ConnectNote() {
        foreach (int id in IDs)
        {
            GameObject note = GameObject.Find("Note_" + id);
            if (note != null)
            {
                Vector3 notePos = note.GetComponent<RectTransform>().localPosition / canvas.GetComponent<RectTransform>().rect.width * 32;
                notePoses.Add(notePos);
            }
        }
        noteConnector.positionCount = IDs.Count;
        noteConnector.SetPositions(notePoses.ToArray());
        notePoses.Clear();
    }

    void PreSetting(ref float canvasWidth, ref float canvasHeight, ref float noteWidth, ref float noteHeight, ref char text, ref float x, ref float y, ref float prevX, ref float prevY, ref float time, int i) {
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        noteWidth = canvasWidth / 10;
        noteHeight = noteWidth;
        canvasWidth -= noteWidth;
        canvasHeight -= noteHeight;
        x = Random.Range(-canvasWidth / 2, canvasWidth / 2);
        y = Random.Range(-canvasHeight / 2, canvasHeight / 2);
        while (x >= prevX - noteWidth * 2 && x <= prevX + noteWidth * 2 && y >= prevY - noteWidth * 2 && y <= prevY + noteWidth * 2) {
            x = Random.Range(-canvasWidth / 2, canvasWidth / 2);
            y = Random.Range(-canvasHeight / 2, canvasHeight / 2);
        }
        prevX = x;
        prevY = y;
        text = stage.texts[i];
        time = stage.delays[i];
    }
}