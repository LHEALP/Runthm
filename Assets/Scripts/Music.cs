using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public SheetEditor sheetEditor;
    public MusicController musicController;
    public AudioSource audioSource;
    AudioClip audioClip;

    public int Min { get; private set; }
    public int Sec { get; private set; }

    public int Bpm { get; set; } = 155;
    public int Frequency { get; set; } = 44100;
    public float Offset { get; set; } = 2495;

    // 박자표
    public int TimeSignatures_numerator { get; set; } = 4;
    public int TimeSignatures_denominator { get; set; } = 4;

    // 1 마디
    public float BarPerSec { get; set; }
    public int BarPerTimeSample { get; set; }
    // 1 박자
    public float BeatPerSec { get; set; }
    public int BeatPerTimeSample { get; set; }
    // 32 비트
    public float BeatPerSec32rd { get; set; }
    public int BeatPerTimeSample32rd { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        Init();
        SetMusicLength();

        BarPerSec = 240f / Bpm; // 4/4기준 = 60*4, 3/4 = 60*3 추후 각 박자표에 대해 정의
        BarPerTimeSample = (int)BarPerSec * Frequency;

        BeatPerSec = 60f / Bpm;
        BeatPerTimeSample = (int)BeatPerSec * Frequency;

        BeatPerSec32rd = BeatPerSec / 8f;
        BeatPerTimeSample32rd = (int)BeatPerSec32rd * Frequency;


        //Offset *= 0.001f;
    }

    void Init()
    {
        audioSource.GetComponent<AudioSource>();

        audioClip = Resources.Load("Milky Way") as AudioClip;
        audioSource.clip = audioClip;
    }

    public void Play()
    {
        Debug.Log(audioSource.clip);
        Debug.Log("현재 타임샘플 포지션 : " + audioSource.timeSamples);
        Debug.Log("타임샘플 전체 : " + audioClip.samples);
        Debug.Log("클립 주파수 : " + audioClip.frequency);

        audioSource.volume = 0.2f;
        //GenNote();
        audioSource.Play();

        sheetEditor.isPlay = true;
    }

    public void Stop()
    {
        audioSource.timeSamples = 0;
        audioSource.Stop();

        sheetEditor.isPlay = false;
    }

    public void Puase()
    {
        audioSource.Pause();

        sheetEditor.isPlay = false;
    }

    public void ChangePos(float time)
    {
        float currentTime = audioSource.time;

        currentTime += time;
        currentTime = Mathf.Clamp(currentTime, 0f, audioClip.length - 0.0001f); // 클립 길이에 딱 맞게 자르면 오류가 발생하여 끄트머리 조금 싹뚝
       
        audioSource.time = currentTime; Debug.Log("현재 음악 위치 " + audioSource.time);
    }

    public void ChangePosByProgressBar(float pos)
    {
        float time = audioClip.length * pos;

        audioSource.time = time;
    }

    void SetMusicLength()
    {
        int audioLength = (int)audioSource.clip.length;

        Min = audioLength / 60;
        Sec = audioLength - Min * 60;
    }
}
