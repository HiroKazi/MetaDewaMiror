using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    

    void LateUpdate()
    {
        transform.LookAt(cam.position);
        transform.localEulerAngles += new Vector3(0, 180, 0);
    }
}
