using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SheetEditor : MonoBehaviour
{
    public SheetEditorController sheetController;
    public GridGenerator gridGenerator;
    public Music music;

    public GameObject cursurObj;
    public GameObject note;

    GameObject seletedObject; // 배치 단계에서 선택된 오브젝트

    public List<int> noteLine1;
    public List<int> noteLine2;
    public List<int> noteLine3;
    public List<int> noteLine4;

    public GameObject realNote;

    public bool isPlay = false;

    public float Speed { get; set; } = 4f;

    // 스냅
    Vector3 snapPos;
    float snapAmount;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DisposePreObject();
    }

    public void SelectObject(string name)
    {
        if (name == "Note")
            sheetController.cursurObj = cursurObj;
    }

    // 노트 사전 배치(스냅)
    void DisposePreObject()
    {
        if (sheetController.mRay.transform != null)
        {
            GameObject gridObject;
            Grid grid;
            gridObject = sheetController.mRay.transform.gameObject; // 레이받은(클릭된) 그리드의 게임오브젝트를 가져온다.
            grid = gridObject.GetComponent<Grid>(); // 그리고 해당 오브젝트의 스크릡트를 가져옴
                                                    //Debug.Log("마디 번호 : " + grid.barNumber);

            Vector3 hitToGrid; // 레이좌표(마우스월드좌표)에서 레이받은 그리드의 좌표를 빼준다. 그래야 항상 올바른 상대적 좌표를 가져올 수 있다.
            hitToGrid = sheetController.mRay.point - gridObject.transform.position;

            //Debug.Log("월드 마우스 : " + sheetController.mRay.point);
            //Debug.Log("그리드 포지 : " + sheetController.mRay.transform.position);
            //Debug.Log("히트투그리드 : " + hitToGrid);
            //Debug.Log("노트 포지 : " + (hitToGrid.y + (gridGenerator.barPerSec * grid.barNumber * gridGenerator.scrollSpeed)));

            ProcessSnapPos(hitToGrid, gridObject);
            sheetController.CursurEffectPos = snapPos;

            if (sheetController.mClickNum == 0)
                DisposeObject(gridObject);
            else if (sheetController.mClickNum == 1)
                UnDisposeObject(gridObject);
        }
    }

    // 오브젝트가 존재하는지 확인
    bool CheckObject(GameObject gridObject)
    {
        GameObject noteContainer = gridObject.transform.GetChild(32).gameObject;
        bool isOverlap = false;

        if (noteContainer.transform.childCount == 0)
        {
            //Debug.Log("0인데 추가");
            return isOverlap;
        }
        else
        {
            for (int i = 0; i < noteContainer.transform.childCount; i++)
            {
                isOverlap = false;

                if (noteContainer.transform.GetChild(i).transform.position == snapPos)
                {
                    Debug.Log("이미 노트가 있습니다.");
                    seletedObject = noteContainer.transform.GetChild(i).transform.gameObject;
                    isOverlap = true;
                    break;
                }
            }
            return isOverlap;
        }
    }
    
    // 오브젝트 배치
    void DisposeObject(GameObject gridObject)
    {
        float time = 0f;// Music.audioSource.time * 1000f;

        GameObject noteContainer = gridObject.transform.GetChild(32).gameObject;

        if(!CheckObject(gridObject))
            Instantiate(note, snapPos, Quaternion.identity, noteContainer.transform);

        // 리스트에 저장
        if (sheetController.mRay.point.x > -5f && sheetController.mRay.point.x < -2.5f)
            noteLine1.Add((int)time);
        else if (sheetController.mRay.point.x > -2.5f && sheetController.mRay.point.x < 0f)
            noteLine2.Add((int)time);
        else if (sheetController.mRay.point.x > 0f && sheetController.mRay.point.x < 2.5f)
            noteLine3.Add((int)time);
        else if (sheetController.mRay.point.x > 2.5f && sheetController.mRay.point.x < 5f)
            noteLine4.Add((int)time);        
    }
    // 오브젝트 언배치
    void UnDisposeObject(GameObject gridObject)
    {
        if (CheckObject(gridObject))
        {
            Debug.Log("지웠습니다.");
            Destroy(seletedObject);
        }
    }


    void ProcessSnapPos(Vector3 hitToGrid, GameObject gridObject)
    {
        // 현재 스냅양에 따라 스냅될 위치를 계산한다. (x값)
        float snapPosX = 0f;
        if (sheetController.mRay.point.x > -5f && sheetController.mRay.point.x < -2.5f)
            snapPosX = -3.75f;
        else if (sheetController.mRay.point.x > -2.5f && sheetController.mRay.point.x < 0f)
            snapPosX = -1.25f;
        else if (sheetController.mRay.point.x > 0f && sheetController.mRay.point.x < 2.5f)
            snapPosX = 1.25f;
        else if (sheetController.mRay.point.x > 2.5f && sheetController.mRay.point.x < 5f)
            snapPosX = 3.75f;

        // 현재 스냅양에 따라 스냅될 위치를 계산한다. (y값)
        float snapAmount = gridGenerator.ScrollSnapAmount * music.BeatPerSec32rd * Speed;
        float halfSnapAmount = snapAmount / 2;

        float snapPosY = hitToGrid.y;
        for (int i = 0; i < 32 / gridGenerator.ScrollSnapAmount; i++)
        {
            if (snapPosY >= (snapAmount * i) - halfSnapAmount && snapPosY <= (snapAmount * i) + halfSnapAmount)
            {
                //Debug.Log("최소 : " + ((snapAmount * i) - halfSnapAmount) + " 최대 : " + ((snapAmount * i) + halfSnapAmount));
                //Debug.Log("걸린 곳 : " + i);
                snapPos = new Vector3(snapPosX, gridObject.transform.position.y + i * snapAmount, -0.1f);

                break;
            }
        }
    }

    void GenNote()
    {
        float convertedTime;

        foreach (int time in noteLine1)
        {
            convertedTime = time * 0.001f;

            Instantiate(realNote, new Vector3(-3.75f, convertedTime * Speed, 0f), Quaternion.identity);
        }
        foreach (int time in noteLine2)
        {
            convertedTime = time * 0.001f;

            Instantiate(realNote, new Vector3(-1.25f, convertedTime * Speed, 0f), Quaternion.identity);
        }
        foreach (int time in noteLine3)
        {
            convertedTime = time * 0.001f;

            Instantiate(realNote, new Vector3(1.25f, convertedTime * Speed, 0f), Quaternion.identity);
        }
        foreach (int time in noteLine4)
        {
            convertedTime = time * 0.001f;

            Instantiate(realNote, new Vector3(3.75f, convertedTime * Speed, 0f), Quaternion.identity);
        }
    }

    public void Save()
    {
        string data = "";

        using (StreamWriter streamWriter = new StreamWriter(new FileStream(Application.dataPath + "/Resources/" + "Milky Way.txt", FileMode.Create, FileAccess.Write), System.Text.Encoding.Unicode))
        {
            foreach (int note in noteLine1)
            {
                data = note.ToString() + ",1";
                streamWriter.WriteLine(data);
            }
            foreach (int note in noteLine2)
            {
                data = note.ToString() + ",2";
                streamWriter.WriteLine(data);
            }
            foreach (int note in noteLine3)
            {
                data = note.ToString() + ",3";
                streamWriter.WriteLine(data);
            }
            foreach (int note in noteLine4)
            {
                data = note.ToString() + ",4";
                streamWriter.WriteLine(data);
            }
        }
    }

    public void Load()
    {
        string data = "";

        using (StreamReader streamReader = new StreamReader(Application.dataPath + "/Resources/" + "Milky Way.txt"))
        {
            while ((data = streamReader.ReadLine()) != null)
            {
                Parse(data);
            }
        }
    }

    void Parse(string rawData) // 추후 스크립트 분리
    {
        string[] splitedData = new string[2];
        int time;
        int lineNumber;

        splitedData = rawData.Split(',');

        time = int.Parse(splitedData[0]);
        lineNumber = int.Parse(splitedData[1]);

        if (lineNumber == 1)
            noteLine1.Add(time);
        else if (lineNumber == 2)
            noteLine2.Add(time);
        else if (lineNumber == 3)
            noteLine3.Add(time);
        else if (lineNumber == 4)
            noteLine4.Add(time);
    }
}
