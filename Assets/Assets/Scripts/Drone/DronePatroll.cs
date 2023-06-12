using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePatroll : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;

    private int wayPointIndex;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        wayPointIndex = 0;
        transform.LookAt(waypoints[wayPointIndex].position);
    }

    enum State
    {
        Idle,
        walk,
        turn
    }


    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[wayPointIndex].position);
        if(dist < 0.1f)
        {
            IncreaseIndex();
        }
        Patrol();
    }
    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        wayPointIndex++;
        if(wayPointIndex >= waypoints.Length)
        {
            wayPointIndex = 0;
        }
        transform.LookAt(waypoints[wayPointIndex].position);
    }   
}
