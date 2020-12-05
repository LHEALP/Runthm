using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public SheetEditor sheetEditor;
    public SheetEditorController sheetController;
    public GridGenerator gridGenerator;

    // Update is called once per frame
    void Update()
    {
        Scroll();
        ChangeSnapAmount();
    }

    void Scroll()
    {
        if(sheetController.isScrolling && !sheetController.isLeftCtrl)
            gridGenerator.ChangePos(sheetController.scrollDir);
    }

    void ChangeSnapAmount()
    {
        if (sheetController.isLeftCtrl && sheetController.isScrolling)
        {
            if (sheetController.scrollDir > 0)
                gridGenerator.ScrollSnapAmount *= 0.5f;
            else if (sheetController.scrollDir < 0)
                gridGenerator.ScrollSnapAmount *= 2f;

            gridGenerator.ChangeSnap();

            //Debug.Log("그리드 스냅 : " + gridGenerator.ScrollSnapAmount);
        }
    }
}
