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
    float speed = 3.4f;
    [SerializeField]
    float jumpHeight = 50.0f;
    [SerializeField]
    public Camera mainCamera;
    [SerializeField]
    SpriteRenderer image;
    [SerializeField]
    private Vector3 groundCheckerPosition = Vector3.zero;
    [SerializeField]
    private Vector2 groundCheckerSize = new(.9f, .1f);

    int colliding = 0;
    int airJumpCount = 0;
    Vector3 movement;
    [SerializeField]
    LayerMask groundLayer;
    Transform t;
    Transform StartPosition;
    bool facingRight;
    Vector3 cameraPos;
    [SerializeField]
    bool isGrounded = false;
    void Start()
    {
        t = transform;
        StartPosition = t;
        rigidbodyPlayer.freezeRotation = true;
        rigidbodyPlayer.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        facingRight = t.localScale.x > 0;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            facingRight = true;
            movement = new Vector3(speed, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            facingRight = false;
            movement = new Vector3(-speed, 0f, 0f);
        }
       
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        }

        if (isGrounded || airJumpCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbodyPlayer.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
                airJumpCount--;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = StartPosition.position;
        }
        ChangeFacing();
        GroundCheck();
    }

    private void ChangeFacing()
    {
        if (!facingRight)
        {
            image.gameObject.transform.localScale = new Vector3(-1,1,0);
        } else
        {
            image.gameObject.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    void GroundCheck()
    {
        bool overlap = Physics2D.OverlapBox(transform.position + groundCheckerPosition, groundCheckerSize, 0, groundLayer);

        isGrounded = overlap;
        if (isGrounded)
        {
            airJumpCount = 2;
        }
    }



    void FixedUpdate()
    {
        transform.position += movement * Time.deltaTime;
        movement = Vector3.zero;        
    }
}
