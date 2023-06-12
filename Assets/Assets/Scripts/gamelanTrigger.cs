using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gamelanTrigger : MonoBehaviour 
{
    
    public AudioSource source;
    public Action <int>triggered;
    public int gamelanID;
    
    // Update is called once per frame
    void Update()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "stick")
        {
            Debug.Log("Kena");
            if (!source.isPlaying)
            {
                source.Play();
            }
            triggered?.Invoke(gamelanID);
        }
    }

}

