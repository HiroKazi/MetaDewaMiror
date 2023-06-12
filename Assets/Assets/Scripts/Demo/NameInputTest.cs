using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class NameInputTest : MonoBehaviour
{
    public TMP_Text inputText;
    
    TouchScreenKeyboard keyboard;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (OVRInput.Get(OVRInput.Button.One))
        //{
        //    keyboard = TouchScreenKeyboard.Open(inputText.text, TouchScreenKeyboardType.Default);
        //}

        if (keyboard != null)
        {
            inputText.text = keyboard.text;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("asdasdasdas"); ;
        keyboard = TouchScreenKeyboard.Open(inputText.text, TouchScreenKeyboardType.Default);   
    }

}
