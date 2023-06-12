using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
       cam = OVRManager.instance.GetComponent<OVRCameraRig>().centerEyeAnchor;
    }

    void LateUpdate()
    {
        transform.LookAt(cam);
    }
}
