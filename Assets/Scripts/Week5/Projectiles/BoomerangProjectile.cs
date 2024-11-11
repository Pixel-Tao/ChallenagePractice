using UnityEngine;

public class CurveBoomerangProjectile : Projectile
{
    // � ���̸� �ܺο��� ����
    public float curveHeightOffset = 2f; // ��� ����
    // ���� ���� ����� �ð�(���ƿ��� ��쿡�� ��ǥ�������� ����� �ð�)
    private float startTime;
    // ���ƿ��� ������
    private bool returning;
    // ���������� ��ǥ���� �Ÿ�
    private float distance;

    // ��ǥ �������� �����ϸ� ����, ��ǥ ��ġ�� �ٲ��� ��.
    private Vector3 startPoint; // ���� ��ġ
    private Vector3 endPoint; // ��ǥ ��ġ
    private float curveHeight; // � ���� (��ǥ�������� �����ϸ� �ݴ� �� ��� �׸��鼭 ���ƿ�)

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
