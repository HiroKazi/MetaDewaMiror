using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;


public class db : MonoBehaviour {
    public GameObject hideButton;
    public TMP_Text output;

    // Create
    public Button createOpener;
    public GameObject create;
    public TMP_InputField createItem;

    // Read
    public Button readOpener;
    public GameObject read;
    public TMP_InputField readId;

    // Update
    public Button updateOpener;
    public GameObject update;
    public TMP_InputField updateId;
    public TMP_InputField updateItem;

    // Delete
    public Button deleteOpener;
    public GameObject delete;
    public TMP_InputField deleteId;

    public void Start() {
        HideAll();
    }

    public void ChangeAllOpenerEnabled(bool enable) {
        createOpener.enabled = enable;
        readOpener.enabled = enable;
        deleteOpener.enabled = enable;
        updateOpener.enabled = enable;
    }

    public void HideAll() {
        create.SetActive(false);
        read.SetActive(false);
        update.SetActive(false);
        delete.SetActive(false);
        hideButton.SetActive(false);
    }

    public void Create() {
        output.text = "Requesting...";
        ChangeAllOpenerEnabled(false);
        StartCoroutine(postRequest(
            "https://70cjvwat7l.execute-api.ap-southeast-1.amazonaws.com/crud/create?item="+
            UnityWebRequest.EscapeURL(createItem.text),
            (JSONNode r) => { 
                Debug.Log(r);
                output.text = r.ToString();
                ChangeAllOpenerEnabled(true);
            }
        ));
        HideAll();
    }

    public void Read() {
        output.text = "Requesting...";
        ChangeAllOpenerEnabled(false);
        StartCoroutine(postRequest(
            "https://70cjvwat7l.execute-api.ap-southeast-1.amazonaws.com/crud/read?id=" +
            UnityWebRequest.EscapeURL(readId.text),
            (JSONNode r) => {
                Debug.Log(r);
                output.text = r.ToString();
                ChangeAllOpenerEnabled(true);
            }
        ));
        HideAll();
    }

    public void UpdateR() {
        output.text = "Requesting...";
        ChangeAllOpenerEnabled(false);
        StartCoroutine(postRequest(
            "https://70cjvwat7l.execute-api.ap-southeast-1.amazonaws.com/crud/update?id=" +
            UnityWebRequest.EscapeURL(updateId.text)+"&item="+UnityWebRequest.EscapeURL(updateItem.text),
            (JSONNode r) => {
                Debug.Log(r);
                output.text = r.ToString();
                ChangeAllOpenerEnabled(true);
            }
        ));
        HideAll();
    }

    public void Delete() {
        output.text = "Requesting...";
        ChangeAllOpenerEnabled(false);
        StartCoroutine(postRequest(
            "https://70cjvwat7l.execute-api.ap-southeast-1.amazonaws.com/crud/delete?id=" +
            UnityWebRequest.EscapeURL(deleteId.text),
            (JSONNode r) => {
                Debug.Log(r);
                output.text = r.ToString();
                ChangeAllOpenerEnabled(true);
            }
        ));
        HideAll();
    }

    public void ShowUI(GameObject popup) {
        popup.SetActive(true);
        hideButton.SetActive(true);
    }

    IEnumerator postRequest(string url, System.Action<JSONNode> callback) {
        var uwr = new UnityWebRequest(url, "POST");
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError) {
            JSONNode data = JSON.Parse(uwr.downloadHandler.text);
            callback(data);
        }
    }
}
