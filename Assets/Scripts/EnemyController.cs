using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Player playerS;
    public int minDamage;
    public int maxDamage;

    public GameObject damPopUp;

    ColoredFlash flash;
    PlayerHealth health;

    EnemyAI2 enemyAI;

    public void Start()
    {
        health = GetComponent<PlayerHealth>();
        flash = GetComponent<ColoredFlash>();
        enemyAI = GetComponent<EnemyAI2>();
    }

    public void TakeDamageP(int damage)
    {
        health.TakeDamage(damage);
        if (damPopUp != null)
        {
            GameObject instance = Instantiate(damPopUp, transform.position
                    + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
            instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
            Destroy(instance, 1f); // Xóa sau 1 giây
            Animator animator = instance.GetComponentInChildren<Animator>();
            if (damage <= 10) animator.Play("normal");
            else animator.Play("critical");
        }
        // Flash
        if (flash != null)
        {
            flash.Flash(Color.white);
        }
        // Freeze
        if (enemyAI != null)
        {
            enemyAI.FreezeEnemy();
        }

        health.TakeDamage(damage);

        if (health == null)
        {
            Debug.LogWarning($"Health is not assigned in {gameObject.name}. Ensure PlayerHealth is attached.");
            return;
        }

        //health.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerS = collision.GetComponent<Player>();
            if (playerS != null)
            {
                InvokeRepeating("DamagePlayer", 0, 0.1f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerS = null;
            CancelInvoke("DamagePlayer");
        }
    }

    void DamagePlayer()
    {
        if (playerS != null)
        {
            int damage = UnityEngine.Random.Range(minDamage, maxDamage);
            //Debug.Log("Player take damage" + damage);
            playerS.TakeDamageP(damage);
        }
    }
}
