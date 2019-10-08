using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomReady : MonoBehaviour
{

    bool isReady = false;

    public void readyPressed()
    {
        if (!isReady)
        {
            isReady = true;
            //TODO: Add ready player
            //TODO: If ready player == 2 : start the scene?
        }
        else {
            isReady = false;
            //TODO: Remove from ready players
        }
    }

}
