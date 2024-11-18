using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.TextCore.Text;

public class EnemyAI2 : MonoBehaviour
{
    public bool roaming = true;
    public float moveSpeed;
    public float nextWPDistance;

    public Seeker seeker;
    public bool updateContinuesPath;
    Path path;
    Coroutine moveCorotine;
    bool reachDestination = false;
    public SpriteRenderer characterSP;

    public float freezeDurationTime;
    float freezeDuration;

    // shoot
    public bool isShootable = false;
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    private float fireCooldown;

    private void Awake()
    {
        if (seeker == null) seeker = GetComponent<Seeker>();
        if (characterSP == null) characterSP = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
        freezeDuration = 0;
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown < 0 && isShootable)
        {
            fireCooldown = timeBtwFire;
            EnemyFireBullet();
        }
    }

    void EnemyFireBullet()
    {
        var player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogWarning("Player not found! Bullet not fired.");
            return;
        }

        var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        Vector3 direction = player.transform.position - transform.position;
        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();
        if (seeker.IsDone() && (reachDestination || updateContinuesPath))
            seeker.StartPath(transform.position, target, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCorotine != null) StopCoroutine(moveCorotine);
        moveCorotine = StartCoroutine(MoveToTargetCorotine());
    }

    IEnumerator MoveToTargetCorotine()
    {
        if (path == null) yield break;

        int currentWP = 0;
        reachDestination = false;
        while (currentWP < path.vectorPath.Count)
        {
            while (freezeDuration > 0)
            {
                freezeDuration -= Time.deltaTime;
                yield return null;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
            {
                currentWP++;
            }

            if (force.x != 0)
                characterSP.transform.localScale = new Vector3(force.x < 0 ? -1 : 1, 1, 1);

            yield return null;
        }
        reachDestination = true;
    }

    Vector2 FindTarget()
    {
        var player = FindObjectOfType<Player>();
        if (player == null)
        {
            //Debug.LogWarning("Player not found! Returning default target.");
            return transform.position;
        }

        Vector3 playerPos = player.transform.position;
        if (roaming)
        {
            return (Vector2)playerPos + Random.Range(10f, 50f) * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        else
        {
            return playerPos;
        }
    }

    public void FreezeEnemy()
    {
        freezeDuration = freezeDurationTime;
    }
}
