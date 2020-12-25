using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet : MonoBehaviour
{
    FileIO fileIO;

    [Header("Sheet Info")]
    public string fileName;
    public string imgFileName;
    public int previewTime;
    public float bpm;
    public float offset;

    [Header("Content Info")]
    public string title;
    public string artist;
    public string source;
    public string sheet;
    public string diff;

    [Header("Note Info")]
    public List<int> noteLine1;
    public List<int> noteLine2;
    public List<int> noteLine3;
    public List<int> noteLine4;

    public void Create()
    {
        fileIO.Save();
    }
}
