using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Character2DController character;
    public Animator animator;

    bool isFloating;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (character.m_Grounded && Input.GetAxis("Jump") > 0)
            animator.SetBool("isGrounded", true);
    }

    private void FixedUpdate()
    {
        //Horizonal moving
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));
        //Vertical moving
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        animator.SetBool("isGrounded", character.m_Grounded);
    }
}
