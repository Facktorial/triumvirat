using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    [SerializeField] int damage;

    private void Update()
    {
        Shoot();        
    }

    public void Init(float speed, Vector3 direction)
    {
        this.speed = speed;
        this.direction = direction;
    }

    void Shoot()
    {
        transform.position += direction * speed * Time.deltaTime;
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
            Hit(other.GetComponent<Player>());
            Break();
        }

        else if (other.CompareTag("Wall"))
        {
            Break();
        }
    }
}
