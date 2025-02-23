using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManagerScript : MonoBehaviour
{
    private int _soldiersToSave;
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
            return;
        }
        
        _soldiersOnBoard += 1;
        _uiManagerScript.UpdateSoldiersOnBoardDisplay(_soldiersOnBoard);
    }

    public void SetSoldiersToSave(int soldiersSpawned)
    {
        _soldiersToSave = soldiersSpawned;
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
        _uiManagerScript.UpdateSoldiersSavedDisplay(_soldiersSaved);
        _uiManagerScript.UpdateSoldiersOnBoardDisplay(_soldiersOnBoard);

        if (_soldiersSaved == _soldiersToSave)
        {
            GameObject.FindWithTag("Player").GetComponent<MovementScript>().setInputsDisabled(true);
            _uiManagerScript.SetWinScreenActiveState(true);
        }
    }

    public int GetSavedSoldiers()
    {
        return _soldiersSaved;
    }

    public void ResetCounters()
    {
        _soldiersOnBoard = 0;
        _soldiersSaved = 0;
        _uiManagerScript.UpdateSoldiersOnBoardDisplay(_soldiersOnBoard);
        _uiManagerScript.UpdateSoldiersSavedDisplay(_soldiersOnBoard);
    }
}
