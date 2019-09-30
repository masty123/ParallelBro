using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    
    private float horizontalRaw = 0;
    private int leftPress = 0;
    private int rightPress = 0;

    private void Update()
    {
        horizontalRaw = rightPress - leftPress;
    }

    public void DownLeft()
    {
        leftPress = 1;
    }

    public void UpLeft()
    {
        leftPress = 0;
    }

    public void DownRight()
    {
        rightPress = 1;
    }

    public void UpRight()
    {
        rightPress = 0;
    }

    public float GetHorizontalRaw()
    {
        return horizontalRaw;
    }
    
}
