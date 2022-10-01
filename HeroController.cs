using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float jump;
    public float buttonTime = 0.3f;
    private float jumpTime;
    private Animator anima;
    private bool jumping = false;
    private bool falling = false;
    private bool colliding;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(falling);
        horizontal = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Jump");
        if (horizontal < 0)
        {
            renderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            renderer.flipX = false;
        }
        if (jump > 0)
        {
            // anima.Play("Jump");
            jumpTime += Time.deltaTime;
            jumping = true;
        }
        else if (jumping && jump == 0)
        {
            jumpTime = 0;
            jumping = false;
            falling = true;
        }
    }

    void FixedUpdate()
    {
        // if (colliding)
        // {
        //     if (horizontal != 0 && !running)
        //     {
        //     }
        //     else if (horizontal == 0)
        //     {
        //         running = false;
        //     }
        // }
        Vector2 position = rb.position;
        position.x = position.x + 6.0f * horizontal * Time.deltaTime;
        if (jumpTime <= buttonTime && jumping && !falling)
        {
            anima.Play("Jump");
            position.y = position.y + 10f * jump * Time.deltaTime;
        }
        else if ((jumpTime > buttonTime || !jumping) && !colliding)
        {
            anima.Play("Fall");
        }

        rb.MovePosition(position);
    }
    
    void OnCollisionEnter2D()
    {
        colliding = true;
        anima.Play("Idle"); 
        falling = false;
    }
    
    void OnCollisionStay2D()
    {
        falling = false;
    }
    
    void OnCollisionExit2D()
    {
        Debug.Log(colliding);
        colliding = false;
    }
}
