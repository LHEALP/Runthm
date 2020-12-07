using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public SheetEditorController sheetController;
    public Music music;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }
    
    void Scroll()
    {
        if (sheetController.isScrolling)
            music.ChangePos(1000f);
    }
}
