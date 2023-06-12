using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncObject2 : NetworkBehaviour
{
    public GameObject jantung;
    public GameObject paruParu;
      
     public void Jantung()
     {
        jantung.gameObject.SetActive(true);
        paruParu.gameObject.SetActive(false);
        Debug.Log("jantung aktif");
     }


     public void ParuParu()
     {
        paruParu.gameObject.SetActive(true);
        jantung.gameObject.SetActive(false);
        Debug.Log("paru2 aktif");
     }
/*
    [ClientRpc]
    public void RpcJantung()
    {
        jantung.gameObject.SetActive(true);
        paruParu.gameObject.SetActive(false);
        //CmdJantung();
        Debug.Log("jantung rpc");
    }
    [ClientRpc]
    public void RpcParuParu()
    {
        paruParu.gameObject.SetActive(true);
        jantung.gameObject.SetActive(false);
       // CmdParuParu();
        Debug.Log("paru paru rpc");
    }*/

    [Command(requiresAuthority = false)]
    public void CmdJantung()
    {
        jantung.gameObject.SetActive(true);
        paruParu.gameObject.SetActive(false);      
        Debug.Log("jantung cmd");
    }
    [Command(requiresAuthority = false)]
    public void CmdParuParu()
    {
        paruParu.gameObject.SetActive(true);
        jantung.gameObject.SetActive(false);       
        Debug.Log("paru cmd");
    }

    [Client]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Jantung();
            CmdJantung();
           
            Debug.Log("jantung button");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ParuParu();
            CmdParuParu();
            Debug.Log("paru paru button");
        }
    }
    
   
}
