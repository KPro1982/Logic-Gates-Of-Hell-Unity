using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public GameObject[] Starts;
    public GameObject[] Finish;
    public int PlayerPoints;
    private const int MAXLEVELS = 10;


    public void Awake()
    {
 
    }

    public void CreateLevel()
    {
        int startX = 100*Screen.width/1070;
        /*Vector3[] corners = new Vector3[4]; 
        RectTransform rect = GameManager.SpritePanel.GetComponent<RectTransform>();
        rect.GetWorldCorners(corners);
        startX = (int) corners[2].x;*/
        
        GameObject gObj = null;
        CalcOffset();
        int i = 0;
        
        
        int startSpacing = Screen.height / MAXLEVELS;
        int startOffset = Screen.height/2 - Starts.Length * startSpacing;
        
       
        foreach (GameObject prefab in Starts)
        {
            i++;
            
            
            var targetWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(startX, startOffset + i*startSpacing, 0));
            targetWorldPos.z = 0;
            gObj = Instantiate(prefab, targetWorldPos, Quaternion.identity);
        }
        
         i = 0;
         int finishX = Screen.width - 100;
        int finishSpacing = Screen.height / MAXLEVELS;
        int finishOffset = Screen.height/2  - Finish.Length * finishSpacing;
        foreach (GameObject prefab in Finish)
        {
            i++;
            
            
            var targetWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(finishX, finishOffset + i*finishSpacing, 0));
            targetWorldPos.z = 0;
            gObj = Instantiate(prefab, targetWorldPos, Quaternion.identity);
        }
        
    }

    public bool CheckStarts()
    {
        bool result = true;
        foreach (GameObject gobj in Starts)
        {
            Gate gateScript = gobj.GetComponent<Gate>();
            if (gateScript.Output == null)
            {
                result = false;
            }  
        }

        return result;
    }

    public void CalcOffset()
    {
        mZCoord = Camera.main.WorldToScreenPoint(GetComponent<Transform>().position).z;
        mOffset = GetComponent<Transform>().position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


}