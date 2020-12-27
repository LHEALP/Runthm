using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class FileIO : MonoBehaviour
{
    public Sheet sheet;
    public SheetParser sheetParser;
    public SheetWriter SheetWriter;
    public SheetEditor sheetEditor;
    public GridGenerator gridGenerator;
    public NoteGenerator noteGenerator;
    public Music music;
    public InputField musicName;
    string basePath;

    private void Start()
    {
        basePath = Application.dataPath + "/Resources/" + sheet.fileName;
    }

    public void SetBasePath()
    {
        sheet.fileName = musicName.text;
        basePath = Application.dataPath + "/Resources/" + sheet.fileName;
    }

    public void Save()
    {
        sheet.noteLine1.Sort();
        sheet.noteLine2.Sort();
        sheet.noteLine3.Sort();
        sheet.noteLine4.Sort();

        DirectoryInfo directoryInfo = new DirectoryInfo(basePath);
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        using (StreamWriter streamWriter = new StreamWriter(new FileStream(basePath + "/" + sheet.fileName + "_data.txt", FileMode.Create, FileAccess.Write), System.Text.Encoding.Unicode))
        {
            streamWriter.Write(SheetWriter.WriteSheetInfo());
            streamWriter.Write(SheetWriter.WriteContentInfo());
            streamWriter.Write(SheetWriter.WriteNoteInfo());
        }
    }

    public void Load()
    {
        string data = "";
        sheet.Init();

        using (StreamReader streamReader = new StreamReader(basePath + "/" + sheet.fileName + "_data.txt"))
        {
            while ((data = streamReader.ReadLine()) != null)
            {
                sheetParser.Parse(data);
            }

            music.Init();
            sheetEditor.Init();
            gridGenerator.Init();
            noteGenerator.GenNote();
        }
        sheetParser.isfirstRead = false;
    }
}
