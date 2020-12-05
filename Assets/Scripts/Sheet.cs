using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sheet
{
    [Header("zzzz")]
    public string fileName;

    [Header("General")]
    public string songName;
    public string artist;

    [Header("Tempo")]
    public float bpm;
    public int timeSignatures_numerator = 4;
    public int timeSignatures_denominator = 4;

    [Header("Note datas")]
    public List<int> noteLine1;
    public List<int> noteLine2;
    public List<int> noteLine3;
    public List<int> noteLine4;

}
