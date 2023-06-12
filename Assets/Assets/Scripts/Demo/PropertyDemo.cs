using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropertyDemo : MonoBehaviour
{
    [Header("Finger")]
    public Collider RightIndexFinger;
    public Collider LeftIndexFinger;

    [Header("Buttons")]
    public Collider weapon1Btn;
    public Collider weapon2Btn;
    public Collider weapon3Btn;
    public Collider weapon4Btn;
    public Collider weapon5Btn; 
    public Collider weapon6Btn;

    [Space()]
    public Collider menuButton;
    
    [Header("NPCS")]
    public GameObject weapon1Obj;
    public GameObject weapon2Obj;
    public GameObject weapon3Obj;
    public GameObject weapon4Obj;
    public GameObject weapon5Obj;
    public GameObject weapon6Obj;

    void Start()
    {
        weapon1Obj.SetActive(false);
        weapon2Obj.SetActive(false);
        weapon3Obj.SetActive(false);
        weapon4Obj.SetActive(false);
        weapon5Obj.SetActive(false);
        weapon6Obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTouched(weapon1Btn))
        {
            weapon1Obj.SetActive(true);
            weapon2Obj.SetActive(false);
            weapon3Obj.SetActive(false);
            weapon4Obj.SetActive(false);
            weapon5Obj.SetActive(false);
            weapon6Obj.SetActive(false);
        }

        if (IsTouched(weapon2Btn))
        {
            weapon1Obj.SetActive(false);
            weapon2Obj.SetActive(true);
            weapon3Obj.SetActive(false);
            weapon4Obj.SetActive(false);
            weapon5Obj.SetActive(false);
            weapon6Obj.SetActive(false);
        }

        if (IsTouched(weapon3Btn))
        {
            weapon1Obj.SetActive(false);
            weapon2Obj.SetActive(false);
            weapon3Obj.SetActive(true);
            weapon4Obj.SetActive(false);
            weapon5Obj.SetActive(false);
            weapon6Obj.SetActive(false);
        }

        if (IsTouched(weapon4Btn))
        {
            weapon1Obj.SetActive(false);
            weapon2Obj.SetActive(false);
            weapon3Obj.SetActive(false);
            weapon4Obj.SetActive(true);
            weapon5Obj.SetActive(false);
            weapon6Obj.SetActive(false);
        }

        if (IsTouched(weapon5Btn))
        {
            weapon1Obj.SetActive(false);
            weapon2Obj.SetActive(false);
            weapon3Obj.SetActive(false);
            weapon4Obj.SetActive(false);
            weapon5Obj.SetActive(true);
            weapon6Obj.SetActive(false);
        }

        if (IsTouched(weapon6Btn))
        {
            weapon1Obj.SetActive(false);
            weapon2Obj.SetActive(false);
            weapon3Obj.SetActive(false);
            weapon4Obj.SetActive(false);
            weapon5Obj.SetActive(false);
            weapon6Obj.SetActive(true);
        }

        if (IsTouched(menuButton))
        {
            SceneManager.LoadScene("Demo");
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
