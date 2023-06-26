using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class ElevatorMirror : NetworkBehaviour
{
    public GameObject elevator;
    public Transform Player;
    public GameObject buttonUp, buttonDown;

    public Vector3 posisiBawah;
    public Vector3 posisiAtas;
    public Rigidbody rig;
    private Coroutine elevatorCoroutine;

    
    
    public Action OnElevatorUp;
    public Action OnElevatorDown;

    

    [SyncVar] 
    int Elevator_number;
    bool isElevatorUp;
    bool isElevatorDown;
    
    int floor;

    [Command (requiresAuthority = false)]
    public void CmdLiftNaik()
    {
        
        isElevatorUp = true;
        Elevator_number = 1;
        floor = 1;
        Debug.Log("floor" + floor);
    }
    [Command(requiresAuthority = false)]
    public void CmdLiftTurun()
    {

        isElevatorDown = true;

        Elevator_number = 2;
        floor = 2;
        Debug.Log("floor" + floor);
    }

    private void Start()
    {
        
        buttonDown.SetActive (false);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            CmdLiftNaik();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            CmdLiftTurun();
        }
        if(Elevator_number == 1)
        {
            if (elevatorCoroutine == null)
            {
                elevatorCoroutine = StartCoroutine(ElevatorUp());
                
                Debug.Log("Elevator naik");
            }
            
           
            
        }
        else if(Elevator_number == 2)
        {
            if (elevatorCoroutine == null)
            {

                elevatorCoroutine = StartCoroutine(ElevatorDownward());
                
            }
            
        }
    }
    public IEnumerator ElevatorUp()
    {

        Player.SetParent(elevator.transform);
        buttonDown.SetActive(true);
        buttonUp.SetActive(false);
        PlayerManagerMD.instance.transform.SetParent(elevator.transform);
        for (float t = 0; t < 1.0f; t += Time.deltaTime / 15)
        {
            Vector3 targetPositionLocal = Vector3.Lerp(posisiBawah, posisiAtas, Mathf.SmoothStep(0, 1, t));
            Vector3 targetPositionGlobal = rig.transform.parent.TransformPoint(targetPositionLocal);
            rig.position = targetPositionGlobal;
            
            yield return null;
        }

        Player.SetParent(null);
        PlayerManagerMD.instance.transform.SetParent(null);
        elevatorCoroutine = null;
        isElevatorUp = false;
        Elevator_number = 0;
        
        OnElevatorUp?.Invoke();
    }
    private IEnumerator ElevatorDownward()
    {

        Player.SetParent(elevator.transform);
        buttonUp.SetActive(true);
        buttonDown.SetActive(false);
        PlayerManagerMD.instance.transform.SetParent(elevator.transform);
        for (float t = 0; t < 1.0f; t += Time.deltaTime / 15)
        {
            Vector3 targetPositionLocal = Vector3.Lerp(posisiAtas, posisiBawah, Mathf.SmoothStep(0, 1, t));
            Vector3 targetPositionGlobal = rig.transform.parent.TransformPoint(targetPositionLocal);
            rig.position = targetPositionGlobal;

            yield return null;
        }
        Player.SetParent(null);
        elevatorCoroutine = null;
        PlayerManagerMD.instance.transform.SetParent(null);
        isElevatorDown = false;
        Elevator_number = 0;
        floor = 0;
        OnElevatorDown?.Invoke();
    }
}
