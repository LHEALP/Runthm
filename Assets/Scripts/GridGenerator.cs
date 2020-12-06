using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public SheetEditor sheetEditor;
    public GameObject grid_Beatbar;

    List<int> times = new List<int>();
    List<GameObject> grids = new List<GameObject>();

    int currentTime = 0;
    int currentTimeSample = 0;
    int currentBar = 6;
    public int maxBeatCnt = 32;

    int nextTime = 2;
    int previousTime = 6;
    int gridDistance;
    // 그리드 생성파괴 좌표
    public float nextPos;
    public float previousPos;

    public float scrollSpeed = 4f; // 그리드 내려가는 속도
    float snapAmount = 8f; // 마우스 스크롤시 그리드 움직이는 양 1 (32비트), 2 (16비트), 4(8비트), 8(4비트), 16(2비트), 32(1비트=1그리드) 
    public float ScrollSnapAmount 
    {
        get { return snapAmount; }
        set { snapAmount = Mathf.Clamp(value, 1f, 32f); }
    }

    // 곡 전역 설정부분 추후 분리
    int bpm = 155;
    int frequency = 44100;
    float offset = 2495;

    // 박자표
    int timeSignatures_numerator = 4;
    int timeSignatures_denominator = 4;

    public float barPerSec;
    int barPerTimeSample;

    float beatPerSec;
    int beatPerTimeSample;

    public float beatPerSec32rd;
    int beatPerTimeSample32rd;

    // Start is called before the first frame update
    void Start()
    {
        barPerSec = 240f / bpm; // 4/4기준 = 60*4, 3/4 = 60*3 추후 각 박자표에 대해 정의
        barPerTimeSample = (int)barPerSec * frequency;

        beatPerSec = 60f / bpm;
        beatPerTimeSample = (int)beatPerSec * frequency;

        beatPerSec32rd = beatPerSec / 8f;
        beatPerTimeSample32rd = (int)beatPerSec32rd * frequency;

        gridDistance = previousTime - nextTime;
        nextPos = barPerSec * scrollSpeed * nextTime * -1f;
        previousPos = barPerSec * scrollSpeed * previousTime;
        //offset *= 0.001f;

        InitGeneral();
        Create();
        Init();

        ChangeSnap();
    }

    void InitGeneral()
    {
        GameObject next = transform.GetChild(0).gameObject;
        GameObject previous = transform.GetChild(1).gameObject;

        next.transform.position = new Vector3(0f, barPerSec * -scrollSpeed * nextTime, 0f);
        previous.transform.position = new Vector3(0f, barPerSec * scrollSpeed * previousTime, 0f);
    }

    // 그리드 생성
    void Create()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject obj = Instantiate(grid_Beatbar, new Vector3(0f, i * 6f * scrollSpeed, 0f), Quaternion.identity);
            Grid grid = obj.GetComponent<Grid>();
            grid.barNumber = i;

            grids.Add(obj);
            obj.SetActive(false);
        }
    }

    // 그리드 좌표 초기화
    void Init()
    {
        for(int i = 0; i < grids.Count; i++)
        {
            GameObject obj = grids[i];
            obj.transform.position = new Vector3(0f, barPerSec * i * scrollSpeed, 0f);
            BoxCollider coll = obj.GetComponent<BoxCollider>();
            coll.size = new Vector3(10f, barPerSec * scrollSpeed, 0.1f);

            Process32rd(obj);
            obj.SetActive(true);
        }
    }

    void ProcessBar(GameObject grid, int dir)
    {
        if(dir == 1)
            grid.transform.position = new Vector3(0f, barPerSec * previousTime * scrollSpeed, 0f);
        else
            grid.transform.position = new Vector3(0f, barPerSec * -nextTime * scrollSpeed, 0f);
    }

    // 각 그리드들의 32개의 비트바 포지션을 처리한다.
    void Process32rd(GameObject grid)
    {
        for(int i = 0; i < maxBeatCnt; i++)
        {
            GameObject obj = grid.transform.GetChild(i).gameObject;
            obj.transform.localPosition = new Vector3(0f, beatPerSec32rd * i * scrollSpeed, -0.1f);
        }
    }

    public void ChangePos(float dir)
    {
        for (int i = 0; i < grids.Count; i++)
        {
            GameObject obj = grids[i];
            obj.transform.Translate(new Vector3(0f, dir * beatPerSec32rd * scrollSpeed * snapAmount, 0f));
        }
    }

    public void ChangeSnap()
    {
        int maxSnapAmount = maxBeatCnt / (int)snapAmount;
        int index = 0;
        
        for (int i = 0; i < grids.Count; i++)
        {
            GameObject obj = grids[i];
            for(int j = 0; j < maxBeatCnt; j++) // 전부 지웠다가
            {
                GameObject child = obj.transform.GetChild(j).gameObject;
                child.SetActive(false);
            }
            for (int j = 0; j < maxSnapAmount; j++) // 필요한 부분만 그려준다
            {
                index = j * (int)snapAmount;

                GameObject child = obj.transform.GetChild(index).gameObject;
                child.SetActive(true);
            }
        }
    }

    public void Next(GameObject grid = null)
    {
        currentBar++;
        currentTime += (int)barPerSec * 1000;

        ProcessBar(grid, 1);
    }

    public void Previous(GameObject grid = null)
    {
        currentBar--;
        currentTime -= (int)barPerSec * 1000;

        ProcessBar(grid, -1);
    }
}
