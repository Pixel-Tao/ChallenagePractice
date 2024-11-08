using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;

    public bool useDelegate = false;
    [SerializeField] private List<IMoveTarget> moveTargets;

    private void Start()
    {
        GameManagerWeek4.Instance.inputController = this;
        moveTargets = new List<IMoveTarget>();
    }

    public void AddMoveTarget(IMoveTarget moveTarget)
    {
        moveTarget.SetMoveHandler(this);
        moveTargets.Add(moveTarget);
    }

    public void OnMove(InputValue input)
    {
        Vector2 delta = input.Get<Vector2>();
        if (useDelegate)
        {
            OnMoveEvent?.Invoke(delta);
        }
        else
        {
            foreach (IMoveTarget moveTarget in moveTargets)
            {
                moveTarget.MoveCommand(delta);
            }
        }
    }
}
