using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int health = 10;

    public int Id => id;
    public int CurrentHealth => health;

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
        currentHealth = health;

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
        currentHealth -= damage;

        print("Player hit: " + damage.ToString());

        if (currentHealth <= 0)
        {
            currentPlayerState = PlayerState.Dead;
            movement.canMove = false;
            GameManager.Instance.AddScore(id);
            GameManager.Instance.RestartGame();
            print("Player dead");
            currentHealth = health;
        }
    }

    void ItemPickUp()
    {
        if (currentItem != null) return;

        Item closestItem = null;

        foreach (Item item in availableItems)
        {
            if (item.hidden) continue;

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

        if (closestItem == null) return;

        closestItem.HideItem();

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

    public void ResetPlayer()
    {
        currentHealth = health;
        currentItem = null;
        canShoot = false;
    }

    private void OnEnable()
    {
        GameEvents.OnGameRestart += ResetPlayer;
    }

    private void OnDisable()
    {
        GameEvents.OnGameRestart -= ResetPlayer;
    }
}
