using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { Normal, Fire }

public class Player : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float collisionCheckRange = 0.1f;
    [SerializeField] LayerMask jumpableLayers = new LayerMask();

    [SerializeField, Space, Header("Type effects")]
    ParticleSystem fire = null;

    float normalMovementSpeed = 0;
    float normalLinearDrag = 0;
    float normalGravity = 0;

    bool isGrounded = false;

    Transform cam = null;
    Collider2D col = null;
    Renderer rend = null;

    public Element Element { get; private set; } = Element.Normal;
    public Rigidbody2D RB { get; private set; }

    public delegate void Jump(Player player);
    public event Jump OnJump;

    void Awake()
    {
        cam = Camera.main.transform;
        col = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
        RB = GetComponent<Rigidbody2D>();

        normalMovementSpeed = movementSpeed;
        normalLinearDrag = RB.drag;
        normalGravity = RB.gravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeType(Element.Fire);

        isGrounded = IsGrounded();
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Vector2 vel = RB.velocity;
            vel.y = jumpForce;
            RB.velocity = vel;
            OnJump?.Invoke(this);
        }

        bool IsGrounded()
        {
            Vector2 center = (Vector2)transform.position + col.offset;
            Vector2 size = col.bounds.size;
            size.x *= 0.8f;
            return Physics2D.BoxCast(center, size, 0, -cam.up, collisionCheckRange, jumpableLayers);
        }  
    }

    void FixedUpdate()
    {
        Vector2 vel = RB.velocity;
        vel.x = cam.right.x * Input.GetAxis("Horizontal") * movementSpeed;
        RB.velocity = vel;
    }

    public void ChangeType(Element element)
    {
        Element = element;
        switch (element)
        {
            case Element.Normal:
                break;
            case Element.Fire:
                rend.enabled = false;
                fire.gameObject.SetActive(true);
                break;
        }
    }

    public void Kill()
    {
        switch (Element)
        {
            case Element.Normal:
                break;
            case Element.Fire:
                fire.Stop();
                col.enabled = false;
                enabled = false;
                break;
            default:
                break;
        }
    }

    public void ResetValues()
    {
        movementSpeed = normalMovementSpeed;
        RB.drag = normalLinearDrag;
        RB.gravityScale = normalGravity;
        OnJump = null;
    }

    void OnDrawGizmosSelected()
    {
        if (cam && col)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Vector2 center = (Vector2)transform.position + col.offset;
            Vector2 size = col.bounds.size;
            size.x *= 0.8f;
            Gizmos.DrawCube(center + -(Vector2)cam.up * collisionCheckRange, size);
        }
    }
}