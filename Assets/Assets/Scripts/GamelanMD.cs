using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamelanMD : NetworkBehaviour
{
    public AudioSource source;

    [SyncVar]
    bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [Command(requiresAuthority = false)]
    void cmdTriggerGamelan()
    {
        if (!isPlaying)
        {
            source.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        cmdTriggerGamelan();
    }
}
