using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 20.0f;
    [SerializeField]
    float damage = 5.0f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetupBullet(bool isFacingRight)
    {
        Vector2 direction2D = new Vector2(isFacingRight ? 1 : -1, 0.05f);
        rb.AddForce(direction2D * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().getDamage(damage);
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject, 1f);
    }
}
