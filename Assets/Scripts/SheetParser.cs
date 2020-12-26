using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetParser : MonoBehaviour
{
    public Sheet sheet;

    enum State
    {
        SheetInfo,
        ContentInfo,
        NoteInfo
    }
    State state;
    public bool isfirstRead;

    public void Parse(string data)
    {
        CheckCurrentMetadata(data);

        if (state == State.SheetInfo)
            ParseSheetInfo(data);
        else if (state == State.ContentInfo)
            ParseContentInfo(data);
        else if (state == State.NoteInfo)
            ParseNoteInfo(data);
    }

    void CheckCurrentMetadata(string data)
    {
        if (data == "[SheetInfo]") state = State.SheetInfo;
        else if (data == "[ContentInfo]") state = State.ContentInfo;
        else if (data == "[NoteInfo]") state = State.NoteInfo;
    }

    public void ParseSheetInfo(string data)
    {
        string[] splitedData = new string[2];
        splitedData = data.Split('=');

        if (splitedData[0] == "AudioFileName")
            sheet.fileName = splitedData[1];
        else if (splitedData[0] == "AudioViewTime")
            sheet.previewTime = int.Parse(splitedData[1]);
        else if (splitedData[0] == "ImageFileName")
            sheet.imgFileName = splitedData[1];
        else if (splitedData[0] == "BPM")
            sheet.bpm = float.Parse(splitedData[1]);
        else if (splitedData[0] == "Offset")
            sheet.offset = float.Parse(splitedData[1]);
    }

    public void ParseContentInfo(string data)
    {
        string[] splitedData = new string[2];
        splitedData = data.Split('=');

        if (splitedData[0] == "Title")
            sheet.title = splitedData[1];
        else if (splitedData[0] == "Artist")
            sheet.artist = splitedData[1];
        else if (splitedData[0] == "Source")
            sheet.source = splitedData[1];
        else if (splitedData[0] == "Sheet")
            sheet.sheet = splitedData[1];
        else if (splitedData[0] == "Difficult")
            sheet.diff = splitedData[1];
    }

    public void ParseNoteInfo(string data)
    {
        if(!isfirstRead) // [NoteInfo] 문자열은 무시한다.
        {
            isfirstRead = true;
            return;
        }

        string[] splitedData = new string[2];
        int time = 0;
        int lineNumber = 1;
        splitedData = data.Split(',');

        int.TryParse(splitedData[0], out time);
        int.TryParse(splitedData[1], out lineNumber);

        if (lineNumber == 1)
            sheet.noteLine1.Add(time);
        else if (lineNumber == 2)
            sheet.noteLine2.Add(time);
        else if (lineNumber == 3)
            sheet.noteLine3.Add(time);
        else if (lineNumber == 4)
            sheet.noteLine4.Add(time);
    }
}
