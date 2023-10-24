using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // public CharacterController2D controller;
    public float runSpeed = 40f;
    public Rigidbody2D rb;

    float horizontalMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (horizontalMove != 0)
            transform.localScale = new Vector3(Mathf.Sign(horizontalMove), 1, 1);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);
    }
}
