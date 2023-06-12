using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Mirror;

public class Elevator : NetworkBehaviour
    
{
    public Transform Player;

    public GameObject elevator;
    public Vector3 posisiBawah;
    public Vector3 posisiAtas;

    private Coroutine elevatorCoroutine;
    public Rigidbody rig;
    public Action OnElevatorUp;
    public Action OnElevatorDown;

    private PhotonView phView;
    
    public GameObject particleLift;
    private bool isUP;
    private bool isDOWN;
   

    private void Update()
    {
      //particleLift.SetActive(elevator.activeInHierarchy);
        if ( Input.GetKeyDown(KeyCode.UpArrow))
        {
            CmdElevatorIsUp();
            
            
        }
        
        
    }
    private void Start()
    {
       // phView = GetComponent<PhotonView>();
    }

    public override void OnSerialize(NetworkWriter writer, bool initialState)
    {
        base.OnSerialize(writer, initialState);
    }

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
     {
         if (stream.IsWriting)
         {
             stream.SendNext(elevator.activeInHierarchy);
             stream.SendNext(isUP);
             stream.SendNext(isDOWN);
         }
         else
         {
             elevator.SetActive((bool)stream.ReceiveNext());
             isUP = (bool)stream.ReceiveNext();
             isDOWN = (bool)stream.ReceiveNext();
             if (isUP)
             {
                 if (elevatorCoroutine == null)
                 {
                     elevatorCoroutine = StartCoroutine(ElevatorUp());
                     isUP = true;
                 }

             }
             if (isDOWN)
             {
                 if (elevatorCoroutine == null)
                 {
                     elevatorCoroutine = StartCoroutine(ElevatorDownward());
                     isDOWN = true;
                 }
             }

         }
     }*/
    

public void ElevatorIsUp()
    {
        
        if (elevatorCoroutine == null)
        {
          //phView.TransferOwnership(PhotonNetwork.LocalPlayer);
            elevatorCoroutine = StartCoroutine(ElevatorUp());
            isUP = true;
            Debug.Log("Elevator naik");
        }
        SetSyncVarDirtyBit(1);
        
    }

    [Command]
    public void CmdElevatorIsUp()
    {
        ElevatorIsUp();
        
        
        
    }

    public IEnumerator ElevatorUp()
    {

        Player.SetParent(elevator.transform);
        PlayerManagerMD.instance.transform.SetParent(elevator.transform);
        for (float t = 0; t < 1.0f; t += Time.deltaTime/15)
        {
            Vector3 targetPositionLocal = Vector3.Lerp(posisiBawah, posisiAtas, Mathf.SmoothStep(0 , 1, t ));
            Vector3 targetPositionGlobal = rig.transform.parent.TransformPoint(targetPositionLocal);
            rig.position = targetPositionGlobal;

            yield return null;
        }

        Player.SetParent(null);
        PlayerManagerMD.instance.transform.SetParent(null);
        elevatorCoroutine = null;
        isUP = false;

        OnElevatorUp?.Invoke();
    }

    public void ElevatorIsDown()
    {
        if (elevatorCoroutine == null)
        {
          //phView.TransferOwnership(PhotonNetwork.LocalPlayer);
            elevatorCoroutine = StartCoroutine(ElevatorDownward());
            isDOWN = true;
        }
    }
    [Command]
    public void CmdElevatorIsDown()
    {
        ElevatorIsDown();
    }

    private IEnumerator ElevatorDownward()
    {

        Player.SetParent(elevator.transform);
        PlayerManagerMD.instance.transform.SetParent(elevator.transform);
        for (float t = 0; t < 1.0f; t += Time.deltaTime/15)
        {
            Vector3 targetPositionLocal = Vector3.Lerp(posisiAtas, posisiBawah, Mathf.SmoothStep(0, 1, t));
            Vector3 targetPositionGlobal = rig.transform.parent.TransformPoint(targetPositionLocal);
            rig.position = targetPositionGlobal;

            yield return null;
        }
        Player.SetParent(null);
        elevatorCoroutine = null;
        PlayerManagerMD.instance.transform.SetParent(null);
        isDOWN = false;
        OnElevatorDown?.Invoke();
    }
}
