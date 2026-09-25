using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int health;

    Movement movement;
    Item currentItem;
    Item sellectedItem;

    [HideInInspector] public bool canShoot = true;


    public PlayerState currentPlayerState;

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
        if (currentItem == null)
            ItemCollision();

        if (currentItem == null && Input.GetKeyDown(movement.shoot))
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
            print("Player dead");
        }
    }

    void ItemPickUp()
    {
        if (sellectedItem != null)
        {
            currentItem = sellectedItem;
            print("Got item");
        }
    }

    void ItemCollision()
    {
        Ray ray = new Ray(transform.position, movement.GetDirection());

        if (Physics.Raycast(ray, out RaycastHit hit, 1))
        {
            if (hit.collider.CompareTag("Item"))
            {
                if (sellectedItem != null)
                {
                    print("Item spotted");
                    sellectedItem = hit.collider.gameObject.GetComponent<Item>();
                    sellectedItem.Sellect();
                    canShoot = false;
                }
            }
            else
            {
                if (sellectedItem != null)
                {
                    sellectedItem.Desellect();
                    sellectedItem = null;
                    canShoot = false;
                }
            }
        }
    }
}
