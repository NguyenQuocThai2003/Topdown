using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Seeker seeker;
    public Transform target;
    Path path;
    public float moveSpeed;
    public float nextWPDistance;
    public SpriteRenderer characterSP;

    Coroutine moveCoroutine;

    private void Start()
    {
        target = FindObjectOfType<Player>().gameObject.transform;
        InvokeRepeating(nameof(CaculatatePath), 0f, 0.5f); // Cập nhật đường dẫn mỗi 0.5 giây
    }

    void CaculatatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathCallBack);
        }
    }

    void OnPathCallBack(Path p)
    {
        if (p.error) return;

        path = p;
        MoveToTarget(); // Gọi di chuyển ngay khi có đường dẫn mới
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargerCoroutine());
    }

    IEnumerator MoveToTargerCoroutine()
    {
        if (path == null) yield break;

        int currentWP = 0;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force; // Di chuyển đúng vị trí

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
            {
                currentWP++;
            }

            // Đảo ngược sprite theo hướng di chuyển
            if (force.x != 0)
                if (force.x < 0)
                    characterSP.transform.localScale = new Vector3(-1, 1, 1);
                else
                    characterSP.transform.localScale = new Vector3(1, 1, 1);

            yield return null;
        }
    }
}
