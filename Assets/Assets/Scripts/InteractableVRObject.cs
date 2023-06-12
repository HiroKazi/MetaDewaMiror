using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableVRObject : MonoBehaviour
{
    /*public GameObject characterButton;
    public GameObject sceneButton;
    public GameObject demoButton;*/

    public Collider RightIndexFinger;
    public Collider LeftIndexFinger;
    public Collider ButtonEnviroment;
    public Collider ButtonCharacter;
    public Collider ButtonProperties;
    public Collider StartGame;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool isRHandPointing = OVRInput.Get(OVRInput.RawButton.RHandTrigger, OVRInput.Controller.RTouch);
        bool isLHandPointing = OVRInput.Get(OVRInput.RawButton.LHandTrigger, OVRInput.Controller.LTouch);

        if (isRHandPointing || isLHandPointing)
        {
            if (RightIndexFinger.bounds.Intersects(ButtonEnviroment.bounds) || LeftIndexFinger.bounds.Intersects(ButtonEnviroment.bounds))
            {
                SceneManager.LoadScene("EnviromentDemo");
            }

            if (RightIndexFinger.bounds.Intersects(ButtonCharacter.bounds) || LeftIndexFinger.bounds.Intersects(ButtonCharacter.bounds))
            {
                SceneManager.LoadScene("CharacterDemo");
            }

            if (RightIndexFinger.bounds.Intersects(ButtonProperties.bounds) || LeftIndexFinger.bounds.Intersects(ButtonProperties.bounds))
            {
                SceneManager.LoadScene("PropertyDemo");
            }

            if (RightIndexFinger.bounds.Intersects(StartGame.bounds) || LeftIndexFinger.bounds.Intersects(StartGame.bounds))
            {
                SceneManager.LoadScene("RealControlRoom_baru");
            }
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Success");
        if (characterButton)
        {
            Debug.Log("Call Character Scene");
            //SceneManager.LoadScene("NPC", LoadSceneMode.Additive);
        }

        if (sceneButton)
        {
            SceneManager.LoadScene("Room for 1", LoadSceneMode.Additive);
        }
    }*/
}
