using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public SheetEditorController sheetController;
    public GridGenerator gridGenerator;
    public Music music;

    public Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sheetController.isScrolling && !sheetController.isLeftCtrl)
            Scroll();

        MoveProgressBarPos();
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

    public void MoveProgressBarPos() // 음악진행에 의한
    {
        progressBar.value = music.audioSource.time / music.audioSource.clip.length;
    }

    public void ControlProgressBarPos() // 사용자 조작에 의한
    {
        float pos = progressBar.value;

        music.ChangePosByProgressBar(pos);
    }
}
