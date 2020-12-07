using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public SheetEditorController sheetController;
    public GridGenerator gridGenerator;
    public Music music;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sheetController.isScrolling && !sheetController.isLeftCtrl)
            Scroll();
    }
    
    void Scroll()
    {
        float movePos;
        movePos = music.BeatPerSec32rd * gridGenerator.ScrollSnapAmount;

        if (sheetController.scrollDir < 0f)
        {
            Debug.Log(movePos + " 초 뒤로");
            music.ChangePos(movePos);
        }
        else if (sheetController.scrollDir > 0f)
        {
            Debug.Log(movePos + " 초 앞으로");
            music.ChangePos(-movePos);
        }
    }
}
