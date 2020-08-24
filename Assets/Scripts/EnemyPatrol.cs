using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] wayPoints;
    public SpriteRenderer spriteRenderer;
    public int damageOnCollision = 20;

    private Transform target;
    private int destPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = wayPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Si l'ennemi arrive à sa destination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            // Changement de target
            destPoint = (destPoint + 1) % wayPoints.Length;
            target = wayPoints[destPoint];

            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
