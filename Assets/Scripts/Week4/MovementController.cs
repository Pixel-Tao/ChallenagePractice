using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    None,
    Horizontal,
    Vertical,
}

public interface IMoveTarget
{
    void MoveCommand(Vector2 delta);
    void SetMoveHandler(InputController inputController);
}

public class MovementController : MonoBehaviour, IMoveTarget
{
    public MovementType movementType;
    [SerializeField] private float speed = 5f;

    private Vector3 moveVector;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ApplyMove();
    }

    private void ApplyMove()
    {
        Vector2 min = GameManagerWeek4.Instance.minVector;
        Vector2 max = GameManagerWeek4.Instance.maxVector;

        Vector3 nextPosition = transform.position + moveVector * Time.deltaTime;

        if (movementType == MovementType.Vertical)
        {
            if (nextPosition.x < min.x)
            {
                nextPosition.x = min.x;
            }
            else if (nextPosition.x > max.x)
            {
                nextPosition.x = max.x;
            }
        }
        else if (movementType == MovementType.Horizontal)
        {
            if (nextPosition.y < min.y)
            {
                nextPosition.y = min.y;
            }
            else if (nextPosition.y > max.y)
            {
                nextPosition.y = max.y;
            }
        }
        transform.position = nextPosition;
    }

    public void MoveHandler(Vector2 delta)
    {
        Debug.Log("Called via delegate");
        SetMoveVector(delta);
    }

    public void MoveCommand(Vector2 delta)
    {
        Debug.Log("Called via interface");
        SetMoveVector(delta);
    }

    private void SetMoveVector(Vector2 delta)
    {
        switch (movementType)
        {
            case MovementType.Vertical:
                moveVector = new Vector3(delta.x, 0, 0);
                break;
            case MovementType.Horizontal:
                moveVector = new Vector3(0, delta.y, 0);
                break;
        }
    }

    public void SetMovementType(MovementType movementType)
    {
        this.movementType = movementType;
    }

    public void SetMoveHandler(InputController inputController)
    {
        inputController.OnMoveEvent += MoveHandler;
    }
}
