using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{

    public float delaySeconds = 2f;

    public Text loadingText;

    // Start is called before the first frame update
    void Start()
    {
        loadText();
        StartCoroutine(LoadSceneWithDelaySeconds());
    }

    IEnumerator LoadSceneWithDelaySeconds()
    {
        yield return new WaitForSeconds(delaySeconds);
        PhotonNetwork.LoadLevel("level" + RoomData.GetInstance().level);
    }

    void loadText()
    {
        if(loadingText != null)
        {
            loadingText.text += "Level" + RoomData.GetInstance().level;
        }
    }

}
