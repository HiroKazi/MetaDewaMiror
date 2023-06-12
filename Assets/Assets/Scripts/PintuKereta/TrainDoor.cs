using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDoor : MonoBehaviour
{
    public Transform pintuKereta;
    public Vector3 rotateBuka;
    public Vector3 rotateTutup;
    private bool run;
    private Coroutine bukaPintu;
    private Coroutine tutupPintu;

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {

            if (run == false)
            {
                bukaPintu = StartCoroutine(openDoor());
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {

            if (run == false)
            {
                tutupPintu = StartCoroutine(closeDoor());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (run == false)
        {
            if (other.tag == "Player")
            {
                bukaPintu = StartCoroutine(openDoor());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (run == false)
        {
            if (other.tag == "Player")
            {
                tutupPintu = StartCoroutine(closeDoor());
            }
        }
    }
    public IEnumerator openDoor()
    {
        run = true;
        for (float i = 0; i < 0.2f; i += Time.deltaTime)
        {
            Debug.Log("Buka" + i/10);
            transform.localEulerAngles = Vector3.Lerp(rotateTutup, rotateBuka, i/0.2f);
            
            yield return null;
        }
        run = false;
    }
    public IEnumerator closeDoor()
    {
        run = true;
        for (float i = 0; i < 1.0f; i += Time.deltaTime)
        {
            Debug.Log("Tutup" + i / 10);
            transform.localEulerAngles = Vector3.Lerp(rotateBuka, rotateTutup, i / 0.2f);

            yield return null;
        }
        run = false;
    }
}
