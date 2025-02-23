using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManagerScript : MonoBehaviour
{
    private int _soldiersSaved;
    private int _soldiersOnBoard;
    private UpdateUIContentScript _uiManagerScript;
    
    void Start()
    {
        _uiManagerScript = GameObject.Find("UIManager").GetComponent<UpdateUIContentScript>();
    }

    public void AddSoldiersOnBoard()
    {
        if (_soldiersOnBoard >= 3)
        {
            Debug.Log("Too many soldiers on board! Take them to the Hospital and try again.");
            return;
        }
        
        _soldiersOnBoard += 1;
        _uiManagerScript.UpdateSoldiersOnBoardDisplay(GetSoldiersOnBoard());
    }

    public int GetSoldiersOnBoard()
    {
        return _soldiersOnBoard;
    }

    public void AddSavedSoldiers()
    {
        if (_soldiersOnBoard <= 0)
        {
            return;
        }
        
        _soldiersSaved += _soldiersOnBoard;
        _soldiersOnBoard = 0;
        _uiManagerScript.UpdateSoldiersSavedDisplay();
        _uiManagerScript.UpdateSoldiersOnBoardDisplay(0);
    }

    public int GetSavedSoldiers()
    {
        return _soldiersSaved;
    }
    
    
}
