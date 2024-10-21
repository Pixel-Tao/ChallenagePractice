using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class AesteriodSpawner : MonoBehaviour
{
    [SerializeField] private Transform rocket;
    [SerializeField] private GameObject aesteriodPrefab;

    [SerializeField] private float spreadAngle = 45f;
    [SerializeField] private float range = 10f;

    private WaitForSeconds waitTime = new WaitForSeconds(5f);

    private void Start()
    {
        StartCoroutine(CoSpawn());
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = rocket.position;
        pos.y += range;
        transform.position = pos;
    }

    IEnumerator CoSpawn()
    {
        while (true)
        {
            yield return waitTime;

            float angle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Vector3 dir = Quaternion.Euler(0, 0, angle) * -transform.up;

            Debug.Log(dir);

            GameObject go = Instantiate(aesteriodPrefab, transform);

            go.transform.position = transform.position;
            go.GetComponent<Aesteriod>()?
                .SetInfo(
                    GetRandomType<AesteriodSizeType>(),
                    GetRandomType<AesteriodMoveType>(),
                    dir
                );
        }
    }

    private T GetRandomType<T>() where T : System.Enum
    {
        System.Array values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range(0, values.Length));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 pos = transform.position;
        pos.y = rocket.position.y + range;
        Gizmos.DrawWireSphere(pos, range); // 반경 표시

        Vector3 fovLine1 = Quaternion.Euler(0, 0, -spreadAngle / 2) * -transform.up * range;
        Vector3 fovLine2 = Quaternion.Euler(0, 0, spreadAngle / 2) * -transform.up * range;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, pos + fovLine1); // FOV 라인 1
        Gizmos.DrawLine(pos, pos + fovLine2); // FOV 라인 2
    }
}
