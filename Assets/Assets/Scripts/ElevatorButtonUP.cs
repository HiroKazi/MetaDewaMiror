using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ElevatorButtonUP :  MonoBehaviour
{
    
    public Action onUpButtonPress;

    public void ElevatorIsUp()
    {
        onUpButtonPress?.Invoke();
        
    }
    
    
    
}
