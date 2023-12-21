using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 3.4f;
    [SerializeField]
    float damage = 5.0f;

    private Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(bool isFacingRight)
    {
        Vector2 direction2D = new Vector2(isFacingRight ? 1 : -1,0.25f);
        rigidbody2d.AddForce(direction2D * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        }

        Destroy(this.gameObject);
    }
}
