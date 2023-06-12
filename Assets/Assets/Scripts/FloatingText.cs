using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float destroyTime = 2;
    private Vector3 Offset = new Vector3(0, 1.5f, 0);
    private Vector3 randomIntensity = new Vector3(2, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
       /* 
                transform.localPosition += Offset;
                transform.localPosition += new Vector3(Random.Range(-randomIntensity.x, randomIntensity.x),
                Random.Range(-randomIntensity.y, randomIntensity.y),
                Random.Range(-randomIntensity.z, randomIntensity.z));
            }

            // Update is called once per frame
            /*void LateUpdate()
            {
                var cameraToLookAt = Camera.main;
                transform.LookAt(cameraToLookAt.transform);
                transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
            }*/
    }
}
