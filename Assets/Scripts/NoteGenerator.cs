using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public Music music;
    public Sheet sheet;
    public SheetEditor sheetEditor;
    public GridGenerator gridGenerator;

    GameObject note;
    float interpolValue;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        note = sheetEditor.note;
        interpolValue = sheetEditor.InterpolValue;
        speed = sheetEditor.Speed;
    }

    public void GenNote()
    {
        float convertedTime;
        float standardTime;
        standardTime = music.BarPerSec - interpolValue;
        GameObject gridObj;
        GameObject noteContainer;

        int index = 0;

        for (int i = 0; i < sheet.noteLine1.Count; i++)
        {
            convertedTime = sheet.noteLine1[i] * 0.001f;

            if (convertedTime >= standardTime)
            {
                index++;
                standardTime *= index + 1;
            }

            gridObj = gridGenerator.grids[index];
            noteContainer = gridObj.transform.GetChild(32).gameObject;

            GameObject obj = Instantiate(note, new Vector3(-3.75f, music.Offset + convertedTime * speed, 0f), Quaternion.identity, noteContainer.transform);
            obj.SetActive(true);
        }
        for (int i = 0; i < sheet.noteLine2.Count; i++)
        {
            convertedTime = sheet.noteLine2[i] * 0.001f;

            if (convertedTime >= standardTime)
            {
                index++;
                standardTime *= index + 1;
            }

            gridObj = gridGenerator.grids[index];
            noteContainer = gridObj.transform.GetChild(32).gameObject;

            GameObject obj = Instantiate(note, new Vector3(-1.25f, music.Offset + convertedTime * speed, 0f), Quaternion.identity, noteContainer.transform);
            obj.SetActive(true);
        }
        for (int i = 0; i < sheet.noteLine3.Count; i++)
        {
            convertedTime = sheet.noteLine3[i] * 0.001f;

            if (convertedTime >= standardTime)
            {
                index++;
                standardTime *= index + 1;
            }

            gridObj = gridGenerator.grids[index];
            noteContainer = gridObj.transform.GetChild(32).gameObject;

            GameObject obj = Instantiate(note, new Vector3(1.25f, music.Offset + convertedTime * speed, 0f), Quaternion.identity, noteContainer.transform);
            obj.SetActive(true);
        }
        for (int i = 0; i < sheet.noteLine4.Count; i++)
        {
            convertedTime = sheet.noteLine4[i] * 0.001f;

            if (convertedTime >= standardTime)
            {
                index++;
                standardTime *= index + 1;
            }

            gridObj = gridGenerator.grids[index];
            noteContainer = gridObj.transform.GetChild(32).gameObject;

            GameObject obj = Instantiate(note, new Vector3(3.75f, music.Offset + convertedTime * speed, 0f), Quaternion.identity, noteContainer.transform);
            obj.SetActive(true);
        }
    }
}
