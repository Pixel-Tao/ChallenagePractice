using UnityEngine;

public class CurveBoomerangProjectile : Projectile
{
    // 곡선 높이를 외부에서 조절
    public float curveHeightOffset = 2f; // 곡선의 높이
    // 시작 지점 출발한 시간(돌아오는 경우에는 목표지점에서 출발한 시간)
    private float startTime;
    // 돌아오는 중인지
    private bool returning;
    // 시작지점과 목표지점 거리
    private float distance;

    // 목표 지점까지 도달하면 시작, 목표 위치는 바뀌어야 함.
    private Vector3 startPoint; // 시작 위치
    private Vector3 endPoint; // 목표 위치
    private float curveHeight; // 곡선 높이 (목표지점까지 도달하면 반대 쪽 곡선을 그리면서 돌아옴)

    protected override void Update()
    {
        Move();
    }

    protected override void Move()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / distance;

        if (fractionOfJourney >= 1f && !returning)
        {
            returning = true;
            startTime = Time.time;
            SetDestPoint(endPoint, startPoint);
            curveHeight = -curveHeightOffset;
        }
        else if (fractionOfJourney >= 1f && returning)
        {
            Destroy();
            IsPlaying = false;
        }

        Vector3 currentPos = Vector3.Lerp(startPoint, endPoint, fractionOfJourney);
        currentPos.y += Mathf.Sin(fractionOfJourney * Mathf.PI) * curveHeight;
        transform.position = currentPos;
    }

    public override void Play(Transform startTransform, Transform targetTransform)
    {
        if (IsPlaying) return;
        IsPlaying = true;
        startTime = Time.time;
        transform.position = this.startPosition;
        SetDestPoint(startTransform.position, targetTransform.position);
        distance = Vector3.Distance(startPosition, targetTransform.position);

        returning = false;
        curveHeight = curveHeightOffset;
    }

    private void SetDestPoint(Vector3 start, Vector3 end)
    {
        this.startPoint = start;
        this.endPoint = end;
    }
}
