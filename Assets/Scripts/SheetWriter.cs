using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetWriter : MonoBehaviour
{
    public Sheet sheet;

    public string WriteNoteInfo()
    {
        string data = "";

        foreach (int note in sheet.noteLine1)
        {
            data += note.ToString() + ",1\n";
        }
        foreach (int note in sheet.noteLine2)
        {
            data += note.ToString() + ",2\n";
        }
        foreach (int note in sheet.noteLine3)
        {
            data += note.ToString() + ",3\n";
        }
        foreach (int note in sheet.noteLine4)
        {
            data += note.ToString() + ",4\n";
        }

        return data;
    }
}
