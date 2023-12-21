using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speed = 3.4f;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Transform groundChecker;
    [SerializeField]
    GameObject text;

    private Rigidbody2D rb;
    private bool facingRight;
    private bool canMove = true;
    Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(facingRight ? speed : -speed, 0, 0);

        Vector3 groundDirection = groundChecker.position - transform.position;

        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, groundDirection, groundDirection.magnitude, groundLayer);

        if (groundHit.collider == null)
        {
            if (facingRight)
            {
                Flip();
                facingRight = false;
            } else
            {
                Flip();
                facingRight = true;
            }
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate()
    { if(canMove)
        transform.position += movement * Time.deltaTime;
    }

    public void EnemyWin()
    {
        if (facingRight)
        {
            text.transform.localScale = new Vector3(text.transform.localScale.x * -1, text.transform.localScale.y, text.transform.localScale.z);
        }
        canMove = false;
        text.SetActive(true);
    }
}
