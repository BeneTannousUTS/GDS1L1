using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateUIContentScript : MonoBehaviour
{
    static readonly string SoldiersOnBoardText = "Soldiers on board = ";
    private int _soldiersOnBoardQty = 0;
    
    static readonly string SoldiersSavedText = "Soldiers saved = ";
    private int _soldiersSavedQty = 0;
        
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("OnBoardText").GetComponent<TMP_Text>().text = (SoldiersOnBoardText + _soldiersOnBoardQty);
        GameObject.Find("SoldiersSavedText").GetComponent<TMP_Text>().text = (SoldiersSavedText + _soldiersSavedQty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSoldiersOnBoardDisplay(int soldiersOnBoard)
    {
        _soldiersOnBoardQty = soldiersOnBoard;
        GameObject.Find("OnBoardText").GetComponent<TMP_Text>().text = (SoldiersOnBoardText + _soldiersOnBoardQty);
    }

    public void UpdateSoldiersSavedDisplay()
    {
        _soldiersSavedQty += _soldiersOnBoardQty;
        GameObject.Find("SoldiersSavedText").GetComponent<TMP_Text>().text = (SoldiersSavedText + _soldiersSavedQty);
    }
    
}
