using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetParser : MonoBehaviour
{
    public Sheet sheet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Parse(string rawData)
    {
        string[] splitedData = new string[2];
        int time;
        int lineNumber;

        splitedData = rawData.Split(',');

        time = int.Parse(splitedData[0]);
        lineNumber = int.Parse(splitedData[1]);

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
