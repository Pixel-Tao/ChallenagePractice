using UnityEngine;

public enum PanelPositionType
{
    None,
    Top,
    Bottom,
    Left,
    Right,
}

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MovementController))]
public class MovePanel : MonoBehaviour
{
    [SerializeField] private PanelPositionType positionType;

    private SpriteRenderer spriteRenderer;
    private MovementController movementController;
    private BoxCollider2D boxCollider;
    private Camera cam;

    public MovementType MovementType => movementController?.movementType ?? MovementType.None;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        boxCollider.size = Vector2.one;
    }

    public void SetData(PanelPositionType positionType)
    {
        Sprite square = Resources.Load<Sprite>("Sprites/Square");
        if (square == null) return;

        spriteRenderer.sprite = square;
        this.positionType = positionType;
        switch (positionType)
        {
            case PanelPositionType.Top:
                movementController.SetMovementType(MovementType.Vertical);
                transform.localScale = new Vector3(1, 0.25f, 1);
                transform.position = new Vector3(0, GameManagerWeek4.Instance.maxVector.y, 0);
                break;
            case PanelPositionType.Bottom:
                movementController.SetMovementType(MovementType.Vertical);
                transform.localScale = new Vector3(1, 0.25f, 1);
                transform.position = new Vector3(0, GameManagerWeek4.Instance.minVector.y, 0);
                break;
            case PanelPositionType.Left:
                movementController.SetMovementType(MovementType.Horizontal);
                transform.localScale = new Vector3(0.25f, 1, 1);
                transform.position = new Vector3(GameManagerWeek4.Instance.minVector.x, 0, 0);
                break;
            case PanelPositionType.Right:
                movementController.SetMovementType(MovementType.Horizontal);
                transform.localScale = new Vector3(0.25f, 1, 1);
                transform.position = new Vector3(GameManagerWeek4.Instance.maxVector.x, 0, 0);
                break;
        }
    }

}
