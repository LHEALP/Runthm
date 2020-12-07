using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;
    AudioClip audioClip;

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
        audioSource.GetComponent<AudioSource>();

        audioClip = Resources.Load("Milky Way") as AudioClip;
        audioSource.clip = audioClip;

        BarPerSec = 240f / Bpm; // 4/4기준 = 60*4, 3/4 = 60*3 추후 각 박자표에 대해 정의
        BarPerTimeSample = (int)BarPerSec * Frequency;

        BeatPerSec = 60f / Bpm;
        BeatPerTimeSample = (int)BeatPerSec * Frequency;

        BeatPerSec32rd = BeatPerSec / 8f;
        BeatPerTimeSample32rd = (int)BeatPerSec32rd * Frequency;


        //Offset *= 0.001f;
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

        //isPlay = true;
    }

    public void Stop()
    {
        audioSource.timeSamples = 0;
        audioSource.Stop();

        //isPlay = false;
    }

    public void Puase()
    {
        audioSource.Pause();

        //isPlay = false;
    }

    public void ChangePos(float time)
    {
        time = Mathf.Clamp(time, 0f, audioClip.length - 0.01f); // 클립 길이에 딱 맞게 자르면 오류가 발생하여 일부 삭제
        Debug.Log(time);
        audioSource.time = time;
    }
}
