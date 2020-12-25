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

    float convertedTime;
    float standardTime;
    GameObject gridObj;
    GameObject noteContainer;
    int index = 0;

    public void GenNote()
    {
        note = sheetEditor.note;
        interpolValue = sheetEditor.InterpolValue;
        speed = sheetEditor.Speed;

        standardTime = music.BarPerSec - interpolValue;

        SetNotePosAtContainer(sheet.noteLine1, 1);
        SetNotePosAtContainer(sheet.noteLine2, 2);
        SetNotePosAtContainer(sheet.noteLine3, 3);
        SetNotePosAtContainer(sheet.noteLine4, 4);
    }

    void SetNotePosAtContainer(List<int> notes, int lineNumber)
    {
        float pos = 0f;
        if (lineNumber == 1)
            pos = -3.75f;
        else if (lineNumber == 2)
            pos = -1.25f;
        else if (lineNumber == 3)
            pos = 1.25f;
        else if (lineNumber == 4)
            pos = 3.75f;

        for (int i = 0; i < notes.Count; i++)
        {
            convertedTime = notes[i] * 0.001f;

            if (convertedTime >= standardTime)
            {
                index++;
                standardTime *= index + 1;
            }

            gridObj = gridGenerator.grids[index];
            noteContainer = gridObj.transform.GetChild(32).gameObject;

            GameObject obj = Instantiate(note, new Vector3(pos, music.Offset + convertedTime * speed, 0f), Quaternion.identity, noteContainer.transform);
            obj.SetActive(true);
        }
        index = 0;
        standardTime = music.BarPerSec - interpolValue;
    }
}
