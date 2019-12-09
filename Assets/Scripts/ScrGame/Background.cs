using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int index = UserData.GetInstance().GetCharacterIndex();
        if (index == 2)
        {
            transform.localRotation = Quaternion.Euler(180, 0, 0);
        }
    }
}
