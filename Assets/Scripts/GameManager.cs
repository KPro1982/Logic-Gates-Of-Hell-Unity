using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject WireStartGate
    {
        get => _wireStartGate;
        set => _wireStartGate = value;
    }

    public GameObject WireEndGate
    {
        get => _wireEndGate;
        set => _wireEndGate = value;
    }

    private GameObject _wireStartGate;
    private GameObject _wireEndGate;
    private string _wireEndInputTerminal;

    public string WireEndInputTerminal
    {
        get => _wireEndInputTerminal;
        set => _wireEndInputTerminal = value;
    }

    public GameObject levelController;
    public GameObject AndGatePrefab;
    public GameObject OrGatePrefab;
    public GameObject NotGatePrefab;
    public GameObject XorGatePrefab;
    public GameObject XNorGatePrefab;
    public GameObject NAndGatePrefab;
    public GameObject NorGatePrefab;

    public int AndGatePoints;
    public int OrGatePoints;
    public int NotGatePoints;
    public int XorGatePoints;
    public int XNorGatePoints;
    public int NAndGatePoints;
    public int NorGatePoints;

    public Text PointsText;
    public Button ResetButton;
    private int startingPoints;
    
    
    
    
    public GameObject[] LevelList;

    public int successCount = 0;
    
    private List<GameObject> PlayerGates = new List<GameObject>();
    private Vector3 mOffset;
    private float mZCoord;


    public int GetPlayerPoints()
    {
        return LevelList[gameLevel].GetComponent<LevelControl>().PlayerPoints;
    }

    public void DebitPlayerPoints(int points)
    {
        LevelList[gameLevel].GetComponent<LevelControl>().PlayerPoints -= points;
        UpdatePoints();
    }

    public void UpdatePoints()
    {
        PointsText.text =
            $"Points: {LevelList[gameLevel].GetComponent<LevelControl>().PlayerPoints}";
    }
    public bool DrawingWire
    {
        get => _drawingWire;
        set => _drawingWire = value;
    }

    private bool _drawingWire;
    private LineRenderer wire;
    private Vector3 mousePos;
    public Material material;
    private int currLines = 0;
    public Button continueButton;
    public GameObject SpritePanel;
    
    
    
    int gameLevel = 0;


    // Start is called before the first frame update
    private void Start()
    {
        LevelList[gameLevel].GetComponent<LevelControl>().CreateLevel();
        startingPoints = LevelList[gameLevel].GetComponent<LevelControl>().PlayerPoints;
        UpdatePoints();
       
    }

    // Update is called once per frame
    private void Update()
    {
        DrawWire();
        if (successCount < LevelList[gameLevel].GetComponent<LevelControl>().Finish.Length)
        {
            continueButton.enabled = false;

        }
        else
        {
            continueButton.enabled = true;
        }
        
    }
    public void OnContinueButton()
    {
        continueButton.GetComponent<AudioSource>().Play(1);
        gameLevel++;
        LevelReset();
        LevelList[gameLevel].GetComponent<LevelControl>().CreateLevel();
        
    }

    public void OnResetButton()
    {
        LevelReset();
        
    }

    void DestroyPrefabsInScene() {
        GameObject[] prefabs = GameObject.FindObjectsOfType<GameObject>();
        foreach  (GameObject prefab in prefabs)
        {
            if (prefab.name.Contains("Prefab") || prefab.name.Contains("Line"))
            {
                Destroy(prefab);
            }

        }
    }
    public void SpawnGate(GateType type)
    {
        GameObject gObj = null;
        CalcOffset();
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPos.z = 0;
        mouseWorldPos.x += 100;

        if (type == GateType.And)
        {
            gObj = Instantiate(AndGatePrefab, mouseWorldPos, Quaternion.identity);
        }

        if (type == GateType.Or)
        {
            gObj = Instantiate(OrGatePrefab, mouseWorldPos, Quaternion.identity);
        }

        if (type == GateType.Not)
        {
            gObj = Instantiate(NotGatePrefab, mouseWorldPos, Quaternion.identity);
        }

        if (type == GateType.Xor)
        {
            gObj = Instantiate(XorGatePrefab, mouseWorldPos, Quaternion.identity);
        }

        if (type == GateType.XNor)
        {
            gObj = Instantiate(XNorGatePrefab, mouseWorldPos, Quaternion.identity);
        }

        if (type == GateType.Nand)
        {
            gObj = Instantiate(NAndGatePrefab, mouseWorldPos, Quaternion.identity);
        }

        if (type == GateType.Nor)
        {
            gObj = Instantiate(NorGatePrefab, mouseWorldPos, Quaternion.identity);
        }


        PlayerGates.Add(gObj);
        Debug.Log("Spawned");
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

    void createLine()
    {
        wire = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        wire.material = material;
        wire.positionCount = 2;
        wire.startWidth = 2f;
        wire.endWidth = 2f;
        wire.useWorldSpace = false;
        wire.numCapVertices = 50;
    }

    void LevelReset()
    {
        /*foreach (var go in PlayerGates)
        {
            Destroy(go);
            
        }*/
        DestroyPrefabsInScene();
        PlayerGates.Clear();
        LevelList[gameLevel].GetComponent<LevelControl>().PlayerPoints = startingPoints;
        UpdatePoints();
        successCount = 0;
        continueButton.enabled = false;
    }

    private void DrawWire()
    {
        
    
        
        if (Input.GetMouseButton(1) && DrawingWire && wire == null) // after right click on target
        {

                createLine();

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                wire.SetPosition(0, mousePos);

        }

        if (Input.GetMouseButton(1) && wire != null )  // dragging a wire
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            wire.SetPosition(1, mousePos);
        }

        if (Input.GetMouseButtonUp(1) && !DrawingWire)  // dropped wire on connection
        {
            var endGateScript = (Gate)WireEndGate.GetComponent<Gate>();
            var startGateScript = (Gate)WireStartGate.GetComponent<Gate>();
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            if (wire != null)
            {
                wire.SetPosition(1, mousePos);
            }

           if (WireEndInputTerminal == "InputA")
            {


                    startGateScript.Output = WireEndGate;
                    startGateScript.wireOut = wire;
                    endGateScript.InputA = WireStartGate;
                    endGateScript.wireA = wire;
             
            }
            else
            {

                    startGateScript.Output = WireEndGate;
                    startGateScript.wireOut = wire;
                    endGateScript.InputB = WireStartGate;
                    endGateScript.wireB = wire;
                

            }
            wire = null;
            currLines++;
            

        }

        if (Input.GetMouseButtonUp(1) && DrawingWire)
        {
            AudioSource clip =   GetComponent<AudioSource>();
            clip.Play(1);
            if (wire != null)
            {
                wire.positionCount = 0;
                wire = null;
                DrawingWire = false;
            }
        }
    }
}


public enum GateType
{
    And,
    Or,
    Not,
    Xor,
    XNor,
    Nand,
    Nor
}