using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MusicController : MonoBehaviour
{
    public SheetEditorController sheetController;
    public GridGenerator gridGenerator;
    public Music music;

    public Slider progressBar;
    public Text time;

    int min;
    int sec;

    // Update is called once per frame
    void Update()
    {
        if(sheetController.isScrolling && !sheetController.isLeftCtrl)
            Scroll();
        if (sheetController.isKeySpace)
            CheckHotkey();

        MoveProgressBarPos();
        
        //if(music.audioSource.isPlaying)
        ChangeProgressTimeText();
    }
    
    void Scroll()
    {
        float movePos;
        movePos = music.BeatPerSec32rd * gridGenerator.ScrollSnapAmount;

        if (sheetController.scrollDir < 0f)
        {
            //Debug.Log(movePos + " 초 뒤로");
            music.ChangePos(movePos);
        }
        else if (sheetController.scrollDir > 0f)
        {
            //Debug.Log(movePos + " 초 앞으로");
            music.ChangePos(-movePos);
        }
    }

    void CheckHotkey()
    {
        if (music.audioSource.isPlaying)
        {
            //Debug.Log("퍼즈");
            sheetController.isKeySpace = false;
            music.Puase();
        }
        else if (!music.audioSource.isPlaying)
        {
            //Debug.Log("플레이");
            sheetController.isKeySpace = false;
            if (music.audioSource.clip != null)
                music.Play();
        }
    }

    public void MoveProgressBarPos() // 음악진행에 의한
    {
        if(music.audioSource.clip != null)
            progressBar.value = music.audioSource.time / music.audioSource.clip.length;
    }

    public void ControlProgressBarPos() // 사용자 조작에 의한
    {
        float pos = progressBar.value;
        music.ChangePosByProgressBar(pos);
        CalculatePos(pos);
        Debug.Log("이거실행중임?");
    }

    void CalculatePos(float pos)
    {
        float value = music.audioSource.clip.length * pos;
        gridGenerator.ChangeFixedPos(-value);
    }

    public void ChangeProgressTimeText()
    {
        int currentTime = (int)music.audioSource.time;

        if (currentTime != 0)
        {
            min = currentTime / 60;
            sec = currentTime - min * 60;
        }

        time.text = string.Format("{0}:{1} / {2}:{3}", min, sec, music.Min, music.Sec);
    }
}
