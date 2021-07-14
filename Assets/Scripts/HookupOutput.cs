using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookupOutput : MonoBehaviour
{
    private GameManager _gameManager;
    // Start is called before the first frame update
    private void Awake()
    {
        // initialize variables
      
        _gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(1))  // capture right mouse button down
        {
            Gate gateScript = gameObject.transform.parent.gameObject.GetComponent<Gate>();
            if (gateScript.Output == null)
            {
                _gameManager.DrawingWire = true;
                _gameManager.WireStartGate = gameObject.transform.parent.gameObject;
            }
        }
        
    }
}
