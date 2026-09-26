using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    [SerializeField] int damage;

    Player shooter;

    private void Update()
    {
        Shoot();        
    }

    public void Init(float speed, Vector3 direction, Player shooter)
    {
        this.speed = speed;
        this.direction = direction;
        this.shooter = shooter;
    }

    void Shoot()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(direction * speed);
    }

    void Hit(Player player)
    {
        player.Hit(damage);
    }

    void Break()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player target = other.GetComponent<Player>();

            if (target == shooter) return;

            Hit(target);
            Break();
        }

        else if (other.CompareTag("Wall") || other.CompareTag("Floor"))
        {
            Break();
        }
    }
}
