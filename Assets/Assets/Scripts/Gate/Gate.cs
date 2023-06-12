using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject rightDoor;
    public GameObject leftDoor;

    public Vector3 closePosR;
    public Vector3 openPosR;
    public Vector3 closePosL;
    public Vector3 openPosL;

    private Coroutine opening;
    private Coroutine closing;

    private bool run;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
           if( run == false)
            {
                opening = StartCoroutine(bukaPintu());
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (run == false)
            {
                closing = StartCoroutine(tutupPintu());
            }
        }
    }
    private IEnumerator bukaPintu()
    {
        run = true;
        for(float i = 0;i < 5.0f;i += Time.deltaTime)
        {
            Vector3 posisipintuAbaru = Vector3.Lerp(closePosR, openPosR, i / 5);
            leftDoor.transform.localPosition = posisipintuAbaru;

            Vector3 posisipintuBbaru = Vector3.Lerp(closePosL, openPosL, i / 5);
            rightDoor.transform.localPosition = posisipintuBbaru;

            yield return null;
        }
        run = false;
    }
    private IEnumerator tutupPintu()
    {
        run = true;
        for (float i = 0; i < 5.0f; i += Time.deltaTime)
        {
            Vector3 posisipintuAbaru = Vector3.Lerp(openPosR, closePosR, i / 5);
            leftDoor.transform.localPosition = posisipintuAbaru;

            Vector3 posisipintuBbaru = Vector3.Lerp(openPosL, closePosL, i / 5);
            rightDoor.transform.localPosition = posisipintuBbaru;

            yield return null;
        }
        run = false;
    }
}
