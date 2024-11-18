using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int minDamage = 6;
    public int maxDamage = 16;
    public bool goodSizeBullet;
    public string shooterTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Enemy") && goodSizeBullet)
        //{
        //    int damage = Random.Range(minDamage, maxDamage);
        //    collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        //    collision.GetComponent<EnemyController>().TakeDamageP(damage);
        //    Destroy(gameObject);
        //}

        //if (collision.CompareTag("Player") && goodSizeBullet)
        //{
        //    int damage = Random.Range(minDamage, maxDamage);
        //    collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        //    Destroy(gameObject);
        //}

        //if (collision.CompareTag("Enemy") && goodSizeBullet)
        //{
        //    int damage = Random.Range(minDamage, maxDamage);
        //    collision.GetComponent<EnemyController>().TakeDamageP(damage);
        //    Destroy(gameObject);
        //}

        if (collision.CompareTag("Player") && goodSizeBullet)
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (collision.gameObject.tag == shooterTag)
            {
                Debug.Log($"Bullet ignored because shooter ({shooterTag}) hit itself.");
                return;
            }
            if (playerHealth != null)
            {
                int damage = Random.Range(minDamage, maxDamage);
                playerHealth.TakeDamage(damage);
                //Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on Player object!");
            }
            Destroy(gameObject);
        }

        //if (collision.CompareTag("Enemy") && goodSizeBullet)
        //{
        //    EnemyController enemyController = collision.GetComponent<EnemyController>();
        //    if (enemyController == null)
        //    {
        //        Debug.LogWarning($"EnemyController not found on {collision.gameObject.name}.");
        //        return;
        //    }

        //    int damage = Random.Range(minDamage, maxDamage);
        //    enemyController.TakeDamageP(damage);
        //    Destroy(gameObject);

        //}

        if (collision.CompareTag("Enemy") && goodSizeBullet)
        {
            // Kiểm tra xem Enemy trúng đạn có phải là shooter không
            if (collision.gameObject.tag == shooterTag)
            {
                Debug.Log($"Bullet ignored because shooter ({shooterTag}) hit itself.");
                return;
            }

            EnemyController enemyController = collision.GetComponent<EnemyController>();
            if (enemyController == null)
            {
                Debug.LogWarning($"EnemyController not found on {collision.gameObject.name}.");
                return;
            }

            int damage = Random.Range(minDamage, maxDamage);
            enemyController.TakeDamageP(damage);
            Destroy(gameObject);
        }
    }
}