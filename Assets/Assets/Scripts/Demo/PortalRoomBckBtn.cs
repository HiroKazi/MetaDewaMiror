using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class
    PortalRoomBckBtn : MonoBehaviour
{
    [Header("Hand Collider")]
    public Collider RightIndexFinger;
    public Collider LeftIndexFinger;

    [Space()]
    public Collider ButtonBack;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsTouched(ButtonBack))
        {
            SceneManager.LoadScene("EnviromentDemo");
        }
    }
    private bool IsTouched(Collider buttonCollider)
    {
        bool isRHandPointing = OVRInput.Get(OVRInput.RawButton.RHandTrigger, OVRInput.Controller.RTouch);
        bool isLHandPointing = OVRInput.Get(OVRInput.RawButton.LHandTrigger, OVRInput.Controller.LTouch);

        if (isRHandPointing || isLHandPointing)
        {
            if (buttonCollider.enabled)
                return RightIndexFinger.bounds.Intersects(buttonCollider.bounds) || LeftIndexFinger.bounds.Intersects(buttonCollider.bounds);
        }

        return false;
    }
}
