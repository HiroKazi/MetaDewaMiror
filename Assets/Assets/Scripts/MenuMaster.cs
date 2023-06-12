using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMaster : MonoBehaviour
{

    public GameObject menuForMaster;
    public OVRInput.RawButton menu;

    // Start is called before the first frame update
    void Start()
    {
        menuForMaster.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(menu))
        {
            menuForMaster.SetActive(true);
        }
        else
        {
            menuForMaster.SetActive(false);
        }
    }
}
