using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElevatorButtonDown : MonoBehaviour
{
    
    public Action onButtonDownPress;

    

    
   

    public void ElevatorIsDown()
    {
        onButtonDownPress?.Invoke();   
    }
   
       
}
