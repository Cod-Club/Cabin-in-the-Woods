using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [Range(0, .3f)]
    [SerializeField]
    private float m_MovementSmoothing = .05f; // How much to smooth out the movement

    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float move)
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move, m_Rigidbody2D.velocity.y);

        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(
            m_Rigidbody2D.velocity,
            targetVelocity,
            ref m_Velocity,
            m_MovementSmoothing
        );

        // Flip the player based on movement input
        transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
    }
}
