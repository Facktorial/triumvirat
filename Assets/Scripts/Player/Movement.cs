using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject projectile;
    
    public Direction currentDirection;

    public enum Direction
    {
        Up, Down, Left, Right,
        UpRight, DownRight, UpLeft, DownLeft
    }

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode shoot;

    private void Update()
    {
        if (Input.GetKeyDown(shoot))
        {
            Shoot();
        }

        MovementInput();
    }

    Vector3 GetDirection()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                return new Vector3(0, 0, 1);

            case Direction.Down:
                return new Vector3(0, 0, -1);

            case Direction.Left:
                return new Vector3(-1, 0, 0);

            case Direction.Right:
                return new Vector3(1, 0, 0);

            case Direction.UpRight:
                return new Vector3(1, 0, 1);

            case Direction.DownRight:
                return new Vector3(1, 0, -1);

            case Direction.UpLeft:
                return new Vector3(-1, 0, 1);

            case Direction.DownLeft:
                return new Vector3(-1, 0, -1);

            default:
                return Vector3.zero;
        }
    }

    void MovementInput()
    {
        if (Input.GetKey(right) && Input.GetKey(up))
        {
            currentDirection = Direction.UpRight;
            Move(GetDirection());
        }

        else if (Input.GetKey(right) && Input.GetKey(down))
        {
            currentDirection = Direction.DownRight;
            Move(GetDirection());
        }

        else if (Input.GetKey(left) && Input.GetKey(up))
        {
            currentDirection = Direction.UpLeft;
            Move(GetDirection());
        }

        else if (Input.GetKey(left) && Input.GetKey(down))
        {
            currentDirection = Direction.DownLeft;
            Move(GetDirection());
        }

        else if (Input.GetKey(up))
        {
            currentDirection = Direction.Up;
            Move(GetDirection());
        }

        else if (Input.GetKey(down))
        {
            currentDirection = Direction.Down;
            Move(GetDirection());
        }

        else if (Input.GetKey(left))
        {
            currentDirection = Direction.Left;
            Move(GetDirection());
        }

        else if (Input.GetKey(right))
        {
            currentDirection = Direction.Right;
            Move(GetDirection());
        }
    }

    void Move(Vector3 direction)
    {
        transform.position += direction * playerSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        var bullet = Instantiate(projectile.transform);
        bullet.GetComponent<Projectile>().Init(projectileSpeed, GetDirection());
        bullet.transform.position = this.transform.position;
    }
}
