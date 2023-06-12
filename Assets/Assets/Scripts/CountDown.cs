using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    

    static public string nextScene;
        


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CdDuration());

    }

    private IEnumerator CdDuration()
    {
        yield return new WaitForSeconds(10);

        //Debug.Log(nextScene);
        Loadlevel();
    }
    private void Loadlevel()
    {
        SceneManager.LoadScene(nextScene);
    }
}
