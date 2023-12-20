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

    int airJumpCount = 0;
    Vector3 movement;
    [SerializeField]
    LayerMask groundLayer;
    Transform t;
    List<Collider2D> groundColliders;

    Transform StartPosition;
    bool facingRight;
    Vector3 cameraPos;
    [SerializeField]
    bool isGrounded = false;
    void Start()
    {
        t = transform;
        StartPosition = t;
        groundColliders = new List<Collider2D>();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((groundLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            if (!groundColliders.Contains(collision))
            {
                groundColliders.Add(collision);
            }
            
            airJumpCount = 2;
        }

        if (collision.CompareTag("Platform"))
            transform.parent = collision.transform;
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((groundLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            if (groundColliders.Contains(collision))
            {
                groundColliders.Remove(collision);
            }
            if (groundColliders.Count == 0)
                isGrounded = false;
        }

        if (collision.CompareTag("Platform"))
            transform.parent = null;
    }



    void FixedUpdate()
    {
        transform.position += movement * Time.deltaTime;
        movement = Vector3.zero;        
    }
}
