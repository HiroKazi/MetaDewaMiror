using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterButtons : MonoBehaviour
{
    [Header("Finger")]
    public Collider RightIndexFinger;
    public Collider LeftIndexFinger;

    [Header("Buttons")]
    public Collider CharchoButton;
    public Collider FlyingFishButton;
    public Collider EarthTitanButton;
    public Collider DroneButton;
    public Collider SoldierButton;
    public Collider MenuButton;

    [Header("NPCS")]
    public GameObject Charcho;
    public GameObject FlyingFish;
    public GameObject EarthTitan;
    public GameObject Drone;
    public GameObject Soldier;

    void Start()
    {
        Charcho.SetActive(false);
        FlyingFish.SetActive(false);
        EarthTitan.SetActive(false);
        Drone.SetActive(false);
        Soldier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTouched(CharchoButton))
        {
            Charcho.SetActive(true);
            FlyingFish.SetActive(false);
            EarthTitan.SetActive(false);
            Drone.SetActive(false);
            Soldier.SetActive(false);
        }

        if (IsTouched(FlyingFishButton))
        {
            Charcho.SetActive(false);
            FlyingFish.SetActive(true);
            EarthTitan.SetActive(false);
            Drone.SetActive(false);
            Soldier.SetActive(false);
        }

        if (IsTouched(EarthTitanButton))
        {
            Charcho.SetActive(false);
            FlyingFish.SetActive(false);
            EarthTitan.SetActive(true);
            Drone.SetActive(false);
            Soldier.SetActive(false);
        }

        if (IsTouched(DroneButton))
        {
            Charcho.SetActive(false);
            FlyingFish.SetActive(false);
            EarthTitan.SetActive(false);
            Drone.SetActive(true);
            Soldier.SetActive(false);
        }

        if (IsTouched(SoldierButton))
        {
            Charcho.SetActive(false);
            FlyingFish.SetActive(false);
            EarthTitan.SetActive(false);
            Drone.SetActive(false);
            Soldier.SetActive(true);
        }

        if (IsTouched(MenuButton))
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
