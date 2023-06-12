using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManagerMD : MonoBehaviour
{
    
    public static PlayerManagerMD instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
