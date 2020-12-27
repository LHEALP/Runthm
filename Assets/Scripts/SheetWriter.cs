using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetWriter : MonoBehaviour
{
    public Sheet sheet;

    public string WriteSheetInfo()
    {
        string data = "";

        data += "[SheetInfo]" +
            "\nAudioFileName=" + sheet.fileName +
            "\nAudioViewTime=" + sheet.previewTime +
            "\nImageFileName=" + sheet.imgFileName + "_Img" +
            "\nBPM=" + sheet.bpm +
            "\nOffset=" + sheet.offset +
            "\nBeat=44\nBit=32\nBar=80\n\n";

        return data;
    }

    public string WriteContentInfo()
    {
        string data = "";

        data += "[ContentInfo]" +
            "\nTitle=" + sheet.title +
            "\nArtist=" + sheet.artist +
            "\nSource=" + sheet.source +
            "\nSheet=" + sheet.sheet +
            "\nDifficult=" + sheet.diff +
            "\n\n";

        return data;
    }

    public string WriteNoteInfo()
    {
        string data = "";

        data += "[NoteInfo]\n";

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
