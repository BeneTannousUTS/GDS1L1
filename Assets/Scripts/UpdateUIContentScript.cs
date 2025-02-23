using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateUIContentScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winPanel;
        
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("OnBoardText").GetComponent<TMP_Text>().text = ("Soldiers on board = 0");
        GameObject.Find("SoldiersSavedText").GetComponent<TMP_Text>().text = ("Soldiers saved = 0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSoldiersOnBoardDisplay(int soldiersOnBoard)
    {
        GameObject.Find("OnBoardText").GetComponent<TMP_Text>().text = ("Soldiers on board = " + soldiersOnBoard);
    }

    public void UpdateSoldiersSavedDisplay(int soldiersSaved)
    {
        GameObject.Find("SoldiersSavedText").GetComponent<TMP_Text>().text = ("Soldiers saved = " + soldiersSaved);
    }

    public void SetGameOverActiveState(bool active)
    {
        gameOverPanel.SetActive(active);
    }
    
    public void SetWinScreenActiveState(bool active)
    {
        winPanel.SetActive(active);
    }
    
}
