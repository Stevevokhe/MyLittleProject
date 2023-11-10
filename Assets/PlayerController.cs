using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D rigidbodyPlayer;
    [SerializeField]
    CapsuleCollider2D capsuleCollider;
    [SerializeField]
    BoxCollider2D groundDetector;
    [SerializeField]
    float speed = 3.4f;
    [SerializeField]
    float jumpHeight = 6.5f;
    [SerializeField]
    float gravityScale = 1.5f;
    [SerializeField]
    public Camera mainCamera;

    Transform t;
    bool facingRight;
    Vector3 cameraPos;
    bool isGrounded = false;
    void Start()
    {
        t = transform;
        rigidbodyPlayer.freezeRotation = true;
        rigidbodyPlayer.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidbodyPlayer.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if(Input.GetKey(KeyCode.A))
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
            else {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
        }
              

        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        }
    }

    void FixedUpdate()
    {
        if (groundDetector.gameObject.layer == 3)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {

            rigidbodyPlayer.velocity = new Vector2(rigidbodyPlayer.velocity.x, jumpHeight);
        }

        if (facingRight)
        {
            this.GameObject.
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            rigidbodyPlayer.velocity = new Vector2(speed, rigidbodyPlayer.velocity.y);
        }
    }
}
