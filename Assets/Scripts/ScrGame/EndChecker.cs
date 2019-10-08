using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChecker : MonoBehaviour
{

    public Transferable[] needToBeAtPlayer1;
    public Transferable[] needToBeAtPlayer2;

    //For debug
    /*
    private void Update()
    {
        Debug.Log("Is end: " + isEnd());
    }
    */

    public bool isEnd()
    {
        if (needToBeAtPlayer1 != null)
        {
            foreach (Transferable player1Object in needToBeAtPlayer1)
            {
                if (player1Object.GetVisiblePlayer().visiblePlayer != VisiblePlayer.PLAYER_1)
                {
                    return false;
                }
            }
        }
        if (needToBeAtPlayer2 != null)
        {
            foreach (Transferable player2Object in needToBeAtPlayer2)
            {
                //Debug.Log(player2Object.GetComponent<InteractableVisible>().visiblePlayer);
                if (player2Object.GetVisiblePlayer().visiblePlayer != VisiblePlayer.PLAYER_2)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
