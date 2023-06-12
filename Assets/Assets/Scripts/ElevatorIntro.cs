using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorIntro : MonoBehaviour
{
    public Transform Player;
    public GameObject elevator;
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    
    private Coroutine elevatorCoroutine;
    public Rigidbody rig;

    public Action OnElevatorUp;

    private AudioSource audioFX;

    private void Start()
    {
        if (elevatorCoroutine == null)
        {
            elevatorCoroutine = StartCoroutine(ElevatorUp());
        }

        audioFX = elevator.GetComponent<AudioSource>();
    }

    private IEnumerator ElevatorUp()
    {
        Player.SetParent(elevator.transform);
        Vector3 playerPos = Player.localPosition;
        playerPos.y = 0;
        Player.localPosition = playerPos;

        if(audioFX == null)
            audioFX = elevator.GetComponent<AudioSource>();

        
        for (float t = 0; t < 10.0f; t += Time.deltaTime)
        {
            
            Vector3 targetPositionLocal = Vector3.Lerp(initialPosition, targetPosition, t / 10);
            Vector3 targetPositionGlobal = rig.transform.parent.TransformPoint(targetPositionLocal);
            rig.position = targetPositionGlobal;
           
            yield return null;
        }
        

        
        Player.SetParent(null);
        elevatorCoroutine = null;

        OnElevatorUp?.Invoke();
    }
}
