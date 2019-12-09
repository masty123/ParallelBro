using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterAnimationController : MonoBehaviour
{
    public Character2DController character;
    public PickingThings characterPicking;
    public Animator animator;

    private GameManager gameManager;
    private int actualTime;
    private int timeBeforeGlitch;

    bool isFloating;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = character.GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();
        timeBeforeGlitch = RandomGlitchTime();
    }

    // Update is called once per frame
    void Update()
    {
        actualTime = Mathf.RoundToInt((float)(PhotonNetwork.Time - gameManager.startTime));
        //if(timeBeforeGlitch)


        if (character.m_Grounded && Input.GetAxis("Jump") > 0)
            animator.SetBool("isGrounded", true);

        if (characterPicking.holdingItem)
        {
            animator.SetBool("IsCarrying", true);
        }
        else
        {
            animator.SetBool("IsCarrying", false);
        }
    }

    private void FixedUpdate()
    {
        //Horizonal moving
        // float move = Input.GetAxis("Horizontal");
        float move = rb.velocity.x;
        animator.SetFloat("Speed", Mathf.Abs(move));
        //Vertical moving
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        animator.SetBool("isGrounded", character.m_Grounded);
    }

    private int RandomGlitchTime()
    {
        return Random.Range(5, 10);
    }
}
