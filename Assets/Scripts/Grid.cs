using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public SheetEditor sheetEditor;
    public GridGenerator gridGenerator;

    public int barNumber;

    // Update is called once per frame
    void Update()
    {
        
        if(sheetEditor.isPlay)
            transform.Translate(Vector3.down * Time.smoothDeltaTime * sheetEditor.Speed);
    }
}