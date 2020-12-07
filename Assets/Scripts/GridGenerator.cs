using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public SheetEditor sheetEditor;
    public GameObject grid_Beatbar;
    public Music music;

    List<GameObject> grids = new List<GameObject>();

    public int maxBeatCnt = 32;

    float scrollSpeed = 4f; // 그리드 내려가는 속도
    float snapAmount = 8f; // 마우스 스크롤시 그리드 움직이는 양 1 (32비트), 2 (16비트), 4(8비트), 8(4비트), 16(2비트), 32(1비트=1그리드) 
    public float ScrollSnapAmount 
    {
        get { return snapAmount; }
        set { snapAmount = Mathf.Clamp(value, 1f, 32f); }
    }

    // Start is called before the first frame update
    void Start()
    {
        //offset *= 0.001f;

        scrollSpeed = sheetEditor.Speed;

        Create();
        Init();

        ChangeSnap();
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
            obj.transform.position = new Vector3(0f, music.BarPerSec * i * scrollSpeed, 0f);
            BoxCollider coll = obj.GetComponent<BoxCollider>();
            coll.size = new Vector3(10f, music.BarPerSec * scrollSpeed, 0.1f);

            Process32rd(obj);
            obj.SetActive(true);
        }
    }

    // 각 그리드들의 32개의 비트바 포지션을 처리한다.
    void Process32rd(GameObject grid)
    {
        for(int i = 0; i < maxBeatCnt; i++)
        {
            GameObject obj = grid.transform.GetChild(i).gameObject;
            obj.transform.localPosition = new Vector3(0f, music.BeatPerSec32rd * i * scrollSpeed, -0.1f);
        }
    }

    public void ChangePos(float dir)
    {
        for (int i = 0; i < grids.Count; i++)
        {
            GameObject obj = grids[i];

            obj.transform.Translate(new Vector3(0f, dir * music.BeatPerSec32rd * scrollSpeed * snapAmount, 0f));
        }
        InterpolPos(dir);
    }
    
    void InterpolPos(float dir)
    {
        // 첫번째 그리드가 더이상 올라가면 안되는데 위로 스크롤했을때 위치를 보정해준다.
        if ((grids[0].transform.position.y == 0f || grids[0].transform.position.y >= 0f) && dir > 0f)
        {
            float interpolPosY = -grids[0].transform.position.y;
            for (int i = 0; i < grids.Count; i++)
            {
                GameObject obj = grids[i];

                obj.transform.Translate(new Vector3(0f, interpolPosY, 0f));
            }
        }
        // 마지막 그리드가 더이상 내려가면 안되는데 아래로 스크롤했을때 위치를 보정해준다.
        if ((grids[grids.Count - 1].transform.position.y == 0f || grids[grids.Count - 1].transform.position.y <= 0f) && dir < 0f)
        {
            float interpolPosY = -grids[grids.Count - 1].transform.position.y;
            for (int i = 0; i < grids.Count; i++)
            {
                GameObject obj = grids[i];

                obj.transform.Translate(new Vector3(0f, interpolPosY, 0f));
            }
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
}
