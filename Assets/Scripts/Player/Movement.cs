using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float stoppingFroceOrigin;
    [SerializeField] float stoppingForce;

    Rigidbody rb;

    Player player;
    
    public Direction currentDirection;

    public bool canMove = true;
    public float stepSoundDelay;

    public enum Direction
    {
        Up, Down, Left, Right,
        UpRight, DownRight, UpLeft, DownLeft
    }

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    bool moving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!moving)
        {
            rb.linearDamping = 100;
        }
        else
        {
            rb.linearDamping = stoppingForce;
        }

        if (player.currentItem != null)
            stoppingForce = stoppingFroceOrigin * player.currentItem.GetItem().stoppingForceMultiplier * Time.deltaTime * 30;

        else
            stoppingForce = stoppingFroceOrigin;

        MovementInput();
        //WallColission();
    }

    public Vector3 GetDirection()
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
        else
        {
            moving = false;
        }
    }

    void Move(Vector3 direction)
    {
        moving = true;
        if (!canMove) return;
        //transform.position += direction * playerSpeed * Time.deltaTime;

        rb.AddForce(GetDirection() * Time.deltaTime * 30, ForceMode.VelocityChange);
    }

    IEnumerator SteppingSoundRoutine()
    {
        while (true)
        {
            AudioManager.Instance.PlayUI("Step1");
        }
    }
}
