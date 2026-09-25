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
        if (Input.GetKey(shoot))
        {
            Shoot();
        }

        MovementInput();
    }

    void MovementInput()
    {
        if (Input.GetKey(right) && Input.GetKey(up))
        {
            Move(new Vector3(1, 0, 1));
            currentDirection = Direction.UpRight;
        }

        else if (Input.GetKey(right) && Input.GetKey(down))
        {
            Move(new Vector3(1, 0, -1));
            currentDirection = Direction.DownRight;
        }

        else if (Input.GetKey(left) && Input.GetKey(up))
        {
            Move(new Vector3(-1, 0, 1));
            currentDirection = Direction.UpLeft;
        }

        else if (Input.GetKey(left) && Input.GetKey(down))
        {
            Move(new Vector3(-1, 0, -1));
            currentDirection = Direction.DownLeft;
        }

        else if (Input.GetKey(up))
        {
            Move(new Vector3(0, 0, 1));
            currentDirection = Direction.Up;
        }

        else if (Input.GetKey(down))
        {
            Move(new Vector3(0, 0, -1));
            currentDirection = Direction.Down;
        }

        else if (Input.GetKey(left))
        {
            Move(new Vector3(-1, 0, 0));
            currentDirection = Direction.Left;
        }

        else if (Input.GetKey(right))
        {
            Move(new Vector3(1, 0, 0));
            currentDirection = Direction.Right;
        }
    }

    void Move(Vector3 direction)
    {
        transform.position += direction * playerSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        var bullet = Instantiate(projectile.transform, transform);

    }
}
