using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    [SerializeField] int damage;

    Player shooter;
    ItemScriptable item;

    [SerializeField] GameObject itemBreakParticles;


    private void Update()
    {
              
    }

    public void Init(float speed, Vector3 direction, Player shooter, ItemScriptable item)
    {
        this.speed = speed;
        this.direction = direction;
        this.shooter = shooter;
        this.item = item;
        damage = item.damage;

        Shoot();
    }

    void Shoot()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(direction * speed, ForceMode.VelocityChange);

        Instantiate(item.visuals, transform);

        rb.angularVelocity = Random.insideUnitSphere * 20;
    }

    void Hit(Player player)
    {
        player.Hit(damage, direction);
    }

    void Break()
    {
        print("Bereaking");
        AudioManager.Instance.PlaySFX(item.audioClipName, transform.position);
        var particles = Instantiate(itemBreakParticles);
        particles.transform.position = transform.position;

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

        else if (other.CompareTag("Wall"))
        {
            Break();
        }
    }
}
