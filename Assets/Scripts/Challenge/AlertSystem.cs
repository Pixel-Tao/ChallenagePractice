using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    // fov가 45라면 45도 각도안에 있는 aesteriod를 인식할 수 있음.
    [SerializeField] private float fov = 45f;
    // radius가 10이라면 반지름 10 범위에서 aesteriod들을 인식할 수 있음.
    [SerializeField] private float radius = 10f;
    private float alertThreshold;

    private Animator animator;
    private static readonly int blinking = Animator.StringToHash("isBlinking");

    private void Start()
    {
        animator = GetComponent<Animator>();
        // FOV를 라디안으로 변환하고 코사인 값을 계산
    }

    private void Update()
    {
        CheckAlert();
    }

    private void CheckAlert()
    {
        // 주변 반경의 소행성들을 확인하고 이를 감지하여 Alert를 발생시킴(isBlinking -> true)
        Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var obj in objectsInRadius)
        {
            if (obj.name != "Aesteriod") continue;

            Vector3 direction = (obj.transform.position - transform.position).normalized;
            // 2. 내적(Dot Product) 계산
            float dot = Vector2.Dot(transform.up, direction);
            // FOV의 절반을 계산해서 cos 값으로 변환
            float cosFovHalf = Mathf.Cos((fov / 2) * Mathf.Deg2Rad);
            Debug.Log($"dot: {dot}, cosFovHalf: {cosFovHalf}");
            // 내적 값이 FOV 범위 내에 있는지 확인
            if (dot >= cosFovHalf)
            {
                animator.SetBool(blinking, true);
                return;
            }
        }

        animator.SetBool(blinking, false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius); // 반경 표시

        Vector3 fovLine1 = Quaternion.Euler(0, 0, fov / 2) * transform.up * radius;
        Vector3 fovLine2 = Quaternion.Euler(0, 0, -fov / 2) * transform.up * radius;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1); // FOV 라인 1
        Gizmos.DrawLine(transform.position, transform.position + fovLine2); // FOV 라인 2
    }
}