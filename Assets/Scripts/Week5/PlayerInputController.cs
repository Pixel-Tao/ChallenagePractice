using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public event Action<Vector2> MoveEvent;
    
    public void OnQuickSlot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // name = 1 or 2 or 3 or 4
            if (int.TryParse(context.control.name, out int key))
                QuickSlotManagerWeek5.Instance.PressQuickSlotButton((QuickSlotKey)key);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }
}