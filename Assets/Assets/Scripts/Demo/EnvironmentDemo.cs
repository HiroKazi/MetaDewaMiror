using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentDemo : MonoBehaviour
{
    [Header("Hand Collider")]
    public Collider RightIndexFinger;
    public Collider LeftIndexFinger;

    [Header("Buttons")]
    public Collider ButtonEnvi1;
    public Collider ButtonEnvi2;
    public Collider ButtonEnvi3;
    public Collider ButtonEnvi6;


    [Space()]
    public Collider ButtonBack;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsTouched(ButtonEnvi1))
        {
            SceneManager.LoadScene("gate");
        }

        if (IsTouched(ButtonEnvi2))
        {
            SceneManager.LoadScene("trainGate");
        }

        if (IsTouched(ButtonEnvi3))
        {

        }

        if (IsTouched(ButtonBack))
        {
            SceneManager.LoadScene("Demo");
        }
        if (IsTouched(ButtonEnvi6))
        {
            SceneManager.LoadScene("PortalHangar_Demo");
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
