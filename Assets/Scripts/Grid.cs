using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public SheetEditor sheetEditor;
    public GridGenerator gridGenerator;

    public int barNumber;

    //public bool isChangePos = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(sheetEditor.isPlay)
            transform.Translate(Vector3.down * Time.smoothDeltaTime * sheetEditor.Speed);
            /*
        if (transform.position.y <= gridGenerator.nextPos)// && isChangePos)
        {
            //isChangePos = false;
            gridGenerator.Next(gameObject);
        }
        else if (transform.position.y >= gridGenerator.previousPos)// && isChangePos)
        {
            //isChangePos = false;
            gridGenerator.Previous(gameObject);
        }*/
    }

    // 클릭되었을 때 자기 위치 계산해서 반환
}