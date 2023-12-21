using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Basics")]
    [SerializeField] Rigidbody2D rigidbodyPlayer;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] float speed = 3.4f;
    [SerializeField] float jumpHeight = 50.0f;
    [SerializeField] public Camera mainCamera;
    [SerializeField] SpriteRenderer image;
    [SerializeField] bool isGrounded = false;

    int airJumpCount = 0;
    Vector3 movement;
    [SerializeField]
    LayerMask groundLayer;
    Transform t;
    List<Collider2D> groundColliders;

    DoorOpener doorOpener;
    EnemyController enemy;
    [Header("Shooting")]
    [SerializeField] Transform firingpoint;
    [SerializeField] Bullet bullet;
    [SerializeField] Bullet BFGbullet;
    [SerializeField] Sprite BFGImage;
    [SerializeField] SpriteRenderer gunSprite;
    [SerializeField] float fireDelay = 0.5f;
    [SerializeField] float BFGfireDelay = 3f;
    private float fireTimer;
    bool hasBFG;

    bool facingRight;

    Vector3 cameraPos;


    private bool canMove;
    void Start()
    {
        t = transform;
        groundColliders = new List<Collider2D>();
        canMove = true;
        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rigidbodyPlayer.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
                    airJumpCount--;
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {

                if (hasBFG)
                {
                    if (fireTimer >= BFGfireDelay)
                    {

                        Instantiate(BFGbullet, firingpoint.position, Quaternion.identity).Setup(facingRight);
                        fireTimer = 0;
                    }
                }
                else
                {
                    if (fireTimer >= fireDelay)
                    {
                        Instantiate(bullet, firingpoint.position, Quaternion.identity).Setup(facingRight);
                        fireTimer = 0;
                    }
                }


            }

            if (Input.GetKeyDown(KeyCode.F) && doorOpener)
                doorOpener.OpenDoor();
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
        fireTimer += Time.deltaTime;
        ChangeFacing();
    }
    void FixedUpdate()
    {
        transform.position += movement * Time.deltaTime;
        movement = Vector3.zero;
    }
    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator Death()
    {
        canMove = false;
        rigidbodyPlayer.AddForce(new Vector3(0, 5, 0), ForceMode2D.Impulse);
        rigidbodyPlayer.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(3f);
        ResetLevel();
    }


    private void ChangeFacing()
    {
        if (!facingRight)
        {
            image.gameObject.transform.localScale = new Vector3(-1, 1, 0);
        }
        else
        {
            image.gameObject.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((groundLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            if (!groundColliders.Contains(collision))
            {
                groundColliders.Add(collision);
            }

            airJumpCount = 2;
            isGrounded = true;
        }

        if (collision.CompareTag("Platform"))
        {
            transform.parent = collision.transform;
            isGrounded = true;
        }


        if (collision.CompareTag("DoorOpener"))
        {
            doorOpener = collision.gameObject.GetComponent<DoorOpener>();
            doorOpener.ShowText();
        }

        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.EnemyWin();
            StartCoroutine(Death());
        }

        if (collision.CompareTag("BFG9000"))
        {
            hasBFG = true;
            gunSprite.sprite = BFGImage;
        }
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

        if (collision.CompareTag("DoorOpener"))
        {
            if (doorOpener != null)
                doorOpener.HideText();
            doorOpener = null;
        }

    }




}
