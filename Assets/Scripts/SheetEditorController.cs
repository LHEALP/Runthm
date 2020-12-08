using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetEditorController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject cursurObj;

    float rayDistance = 15f;
    public RaycastHit mRay;
    public int mClickNum;
    public bool isScrolling = false;
    public float scrollDir;
    public bool isLeftCtrl = false;
    public bool isKeySpace = false;

    public Vector3 CursurEffectPos { get; set; }

    public LayerMask Layer { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Layer = LayerMask.GetMask("Grid");
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseClick();
        OnMouseScroll();
        OnLeftCtrl();
        OnKeySpace();
    }
    void LateUpdate()
    {
        OnMouseRay(Layer);
        OnCursurEffect();
    }

    void OnMouseRay(LayerMask layerMask)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCam.farClipPlane;
        Vector3 dir = mainCam.ScreenToWorldPoint(mousePos);

        RaycastHit hit;
        Debug.DrawRay(transform.position, dir, Color.red, 0.2f);
        if (Physics.Raycast(transform.position, dir, out hit, rayDistance))// Layer))
        {
            mRay = hit;
            //Debug.Log("월드 마우스 : " + hit.point);
            //Debug.Log("그리드 포지 : " + hit.transform.position);
        }
    }

    void OnCursurEffect()
    {
        cursurObj.transform.position = CursurEffectPos;
    }

    void OnMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) mClickNum = 0;
        else if (Input.GetMouseButtonDown(1)) mClickNum = 1;
        else if (Input.GetMouseButtonDown(2)) mClickNum = 2;
        else mClickNum = -1;
    }

    void OnMouseScroll()
    {
        if (Input.mouseScrollDelta.y < 0) // 아래
        {
            isScrolling = true;
            scrollDir = -1f;
        }
        else if (Input.mouseScrollDelta.y > 0) // 위
        {
            isScrolling = true;
            scrollDir = 1f;
        }
        else
        {
            isScrolling = false;
            scrollDir = 0f;
        }
    }

    void OnLeftCtrl()
    {
        if (Input.GetKey(KeyCode.LeftControl)) isLeftCtrl = true;
        else isLeftCtrl = false;
    }

    void OnKeySpace()
    {
        if (Input.GetKeyDown(KeyCode.Space)) isKeySpace = true;
    }
}
