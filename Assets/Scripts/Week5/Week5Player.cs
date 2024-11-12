using System;
using UnityEngine;

public class Week5Player : MonoBehaviour
{
    public SkillBook skillBook;
    public Transform target;
    private PlayerInputController inputController;
    private Camera cam;
    
    private Vector3 movePosition;
    
    private void Awake()
    {
        cam = Camera.main;
        GameManagerWeek5.Instance.player = this;
        skillBook = GetComponent<SkillBook>();
        inputController = GetComponent<PlayerInputController>();
    }

    private void Start()
    {
        inputController.MoveEvent += Move;
        QuickSlotManagerWeek5.Instance.QuickSlotButtonPressed -= UseSkill;
        QuickSlotManagerWeek5.Instance.QuickSlotButtonPressed += UseSkill;
    }

    private void Update()
    {
        transform.position = movePosition;
    }

    private void Move(Vector2 position)
    {
        movePosition = cam.ScreenToWorldPoint(position);
        movePosition.z = 0f;
    }

    private void UseSkill(int skillId)
    {
        skillBook.InvokeExecute(skillId, target);
    }
}