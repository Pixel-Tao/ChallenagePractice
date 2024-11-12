using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Projectile : MonoBehaviour
{
    public float speed = 2;
    public float playTime = 2f;
    public bool shouldDestroyOnTime;
    public Vector3 direction;

    private float timer = 0;

    public bool IsPlaying { get; protected set; }

    protected Vector3 startPosition;
    protected Transform targetTransform;
    protected SpriteRenderer spriteRenderer;

    protected abstract void Move();

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Circle");
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

    }

    protected virtual void Update()
    {
        if (IsPlaying)
        {
            Move();
            timer += Time.deltaTime;
            if (timer >= playTime)
            {
                IsPlaying = false;
                timer = 0;
                Destroy();
            }
        }
    }

    public virtual void Play(Transform startTransform, Transform targetTransform)
    {
        if (IsPlaying) return;
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
        
        IsPlaying = true;
        this.startPosition = startTransform.position;
        this.targetTransform = targetTransform;
        transform.position = this.startPosition;
        direction = (targetTransform.position - startPosition).normalized;
    }

    public virtual void Play(Transform startTransform)
    {
        if (IsPlaying) return;
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        IsPlaying = true;
        this.startPosition = startTransform.position;
        transform.position = this.startPosition;
    }

    protected void Destroy()
    {
        if (shouldDestroyOnTime)
            Destroy(gameObject);
        else
            PoolManager.Instance.Despawn(gameObject);
    }
}
