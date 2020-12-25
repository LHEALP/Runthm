using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileIO : MonoBehaviour
{
    public Sheet sheet;
    public SheetParser sheetParser;
    public SheetWriter SheetWriter;
    public NoteGenerator noteGenerator;
    string basePath;

    private void Start()
    {
        basePath = Application.dataPath + "/Resources/" + sheet.fileName;
    }

    public void Save()
    {
        string data = "";

        sheet.noteLine1.Sort();
        sheet.noteLine2.Sort();
        sheet.noteLine3.Sort();
        sheet.noteLine4.Sort();

        DirectoryInfo directoryInfo = new DirectoryInfo(basePath);
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        using (StreamWriter streamWriter = new StreamWriter(new FileStream(basePath + "/" + sheet.fileName + ".txt", FileMode.Create, FileAccess.Write), System.Text.Encoding.Unicode))
        {
            streamWriter.Write(SheetWriter.WriteSheetInfo());
            streamWriter.Write(SheetWriter.WriteContentInfo());
            streamWriter.Write(SheetWriter.WriteNoteInfo());
        }
    }

    public void Load()
    {
        string data = "";

        using (StreamReader streamReader = new StreamReader(basePath + "/" + sheet.fileName + ".txt"))
        {
            while ((data = streamReader.ReadLine()) != null)
            {
                sheetParser.Parse(data);
            }

            noteGenerator.GenNote();
        }
    }
}
