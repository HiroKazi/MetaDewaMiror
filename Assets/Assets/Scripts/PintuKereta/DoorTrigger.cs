using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : TrainDoor
{
    
    bool isOpened = false;
    private Coroutine bukaPintu;
    private Coroutine tutupPintu;
    
    
    void OnTriggerEnter(Collider col)
    {
        if (!isOpened)
        {
            isOpened = true;
            bukaPintu = StartCoroutine(openDoor());
        }
    }

}
