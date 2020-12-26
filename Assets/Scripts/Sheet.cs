﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet : MonoBehaviour
{
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

    public void Init()
    {
        noteLine1.Clear();
        noteLine2.Clear();
        noteLine3.Clear();
        noteLine4.Clear();
    }
}
