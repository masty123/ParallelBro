using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    public GameObject Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float JumpZoomOutSize = 6.5f;
    private float defaultSize;

    // Start is called before the first frame update
    void Start()
    {
        defaultSize = GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        Vector3 playerTransform = player.transform.position;

        Vector3 desiredPosition = playerTransform + offset;
        // VDebug.Instance.Log(desiredPosition.)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        // transform.LookAt(playerTransform);

        // if() not ground
        // Debug.Log(player.GetComponent<Character2DController>().m_Grounded);
        if (!player.GetComponent<Character2DController>().m_Grounded)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, JumpZoomOutSize, smoothSpeed);
        }
        else
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, defaultSize, smoothSpeed);
        }
        // if ground
    }
}
