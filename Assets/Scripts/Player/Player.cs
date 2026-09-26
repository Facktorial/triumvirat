using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] int health;

    Movement movement;
    [HideInInspector] public Item currentItem;
    Item sellectedItem;

    public bool canShoot = true;
    [SerializeField] GameObject projectile;
    [SerializeField] KeyCode shoot;

    public PlayerState currentPlayerState;

    [SerializeField] List<Item> availableItems = new List<Item>();

    public enum PlayerState
    {
        Alive,
        Dead
    }

    private void Start()
    {
        movement = GetComponent<Movement>();
        currentPlayerState = PlayerState.Alive;
    }

    private void Update()
    {
        if (Input.GetKeyDown(shoot))
        {
            Shoot();
        }

        if (currentItem == null && Input.GetKeyDown(shoot))
        {
            ItemPickUp();
        }
    }

    public void Hit(int damage)
    {
        health -= damage;

        print("Player hit: " + damage.ToString());

        if (health <= 0)
        {
            currentPlayerState = PlayerState.Dead;
            movement.canMove = false;
            GameManager.Instance.AddScore(id);
            print("Player dead");
        }
    }

    void ItemPickUp()
    {
        if (currentItem != null) return;

        Item closestItem = null;

        foreach (Item item in availableItems)
        {
            if (closestItem == null)
            {
                closestItem = item;
                continue;
            }

            if (Vector3.Distance(transform.position, closestItem.transform.position) >
                Vector3.Distance(transform.position, item.transform.position))
            {
                closestItem = item;
            }
        }

        currentItem.gameObject.SetActive(false);
        availableItems.Remove(closestItem);
        currentItem = closestItem;
        canShoot = true;
        print("Got item");
    }

    void Shoot()
    {
        if (!canShoot || currentItem == null) return;

        print("Shot");

        var bullet = Instantiate(projectile.transform);
        bullet.GetComponent<Projectile>().Init(currentItem.GetItem().itemSpeed, movement.GetDirection(), this, currentItem.GetItem());
        bullet.transform.position = this.transform.position + movement.GetDirection();

        currentItem = null;
        canShoot = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentItem != null) return;

        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();

            if (!availableItems.Contains(item))
            {
                availableItems.Add(item);
                item.Sellect();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            availableItems.Remove(item);
            item.Desellect();
        }
    }
}
