using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private Vector3 direction;

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
}
