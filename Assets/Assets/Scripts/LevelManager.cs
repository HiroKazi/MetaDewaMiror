using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{

    [SerializeField] private Elevator elevator;
    

    private void Start()
    {
        elevator.OnElevatorUp += LoadLevelUp;
        elevator.OnElevatorDown += LoadLevelDown;
    }

   /* private void OnDestroy()
    {
      //  elevatorUpButton.OnElevatorUp -= LoadLevelUp;
    }    */

    private void LoadLevelUp()
    {
        if(SceneManager.GetActiveScene().name == "01-02-LobGamel")
        {
            CountDown.nextScene = "03-04-RamaBali";
        }
        
       // Debug.Log(SceneManager.GetActiveScene().name);
       // Debug.Log(CountDown.nextScene);
        SceneManager.LoadScene("03-04-RamaBali");
    }
    
    private void LoadLevelDown(){
        if (SceneManager.GetActiveScene().name == "01-02-LobGamel")
        {
            CountDown.nextScene = "01-02-LobGamel";
        }
        
        Debug.Log(SceneManager.GetActiveScene().name);
        Debug.Log(CountDown.nextScene);
        SceneManager.LoadScene("01-02-LobGamel");
    }
}
