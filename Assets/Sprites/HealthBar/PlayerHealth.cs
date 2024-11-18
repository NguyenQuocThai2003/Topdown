using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;

    public HealthBar healthBar;

    public UnityEvent OnDeath;

    public float safeTime = 1f;
    float safeTimeCoolDown;

    //public bool isDead = false;

    private void OnEnable()
    {
        OnDeath.AddListener(Death);
    }

    private void OnDisable()
    {
        OnDeath.RemoveListener(Death);
    }

    private void Start()
    {
        currentHealth = maxHealth;

        //healthBar.UpdateBar(currentHealth, maxHealth);
        if (healthBar != null)
        {
            healthBar.UpdateBar(currentHealth, maxHealth);
        }
        else
        {
            Debug.LogWarning("HealthBar chưa được gán trong Inspector.");
        }
    }
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // Đảm bảo máu không vượt quá giới hạn tối đa
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Cập nhật thanh máu
        if (healthBar != null)
        {
            healthBar.UpdateBar(currentHealth, maxHealth);
        }
        else
        {
            Debug.LogWarning("healthBar chưa được gán giá trị!");
        }

        Debug.Log("Player healed for: " + healAmount + " HP. Current health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (safeTimeCoolDown <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //if (this.gameObject.tag == "Enemy")
                //{
                //    FindObjectOfType<WeaponManager>().RemoveEnemyToFireRange(this.transform);
                //    FindObjectOfType<Killed>().UpdateKilled();
                //    FindObjectOfType<PlayerExp>().UpdateExperience(UnityEngine.Random.Range(1, 4));
                //    Destroy(this.gameObject, 0.125f);
                //}
                //isDead = true;
                OnDeath.Invoke();
            }

            safeTimeCoolDown = safeTime;
            //healthBar.UpdateBar(currentHealth, maxHealth);
            if (healthBar != null)
            {
                healthBar.UpdateBar(currentHealth, maxHealth);
            }
            else
            {
                Debug.LogWarning("healthBar chưa được gán giá trị!");
            }
        }
        //if (safeTimeCoolDown <= 0)
        //{
        //    currentHealth -= damage;

        //    if (currentHealth <= 0)
        //    {
        //        currentHealth = 0;
        //        if (OnDeath != null)
        //        {
        //            OnDeath.Invoke();
        //        }
        //        else
        //        {
        //            Debug.LogWarning("OnDeath chưa được gán giá trị!");
        //        }
        //    }

        //    safeTimeCoolDown = safeTime;

        //    if (healthBar != null)
        //    {
        //        healthBar.UpdateBar(currentHealth, maxHealth);
        //    }
        //    else
        //    {
        //        Debug.LogWarning("healthBar chưa được gán giá trị!");
        //    }
        //}
    }
    public GameObject gameOverCanvas; // Kéo GameOverCanvas vào đây trong Inspector.

    public void Death()
    {
        if (gameOverCanvas != null)
        {
            Time.timeScale = 0f; // Dừng thời gian trong game
            gameOverCanvas.SetActive(true); // Hiển thị màn hình Game Over
        }
        else
        {
            Debug.LogWarning("GameOverCanvas chưa được gán trong Inspector.");
        }

        // Xóa đối tượng người chơi
        Destroy(gameObject); // Điều này sẽ xóa GameObject của người chơi khỏi scene

        // Nếu muốn thêm một chút thời gian delay trước khi xóa, có thể làm như sau
         Destroy(gameObject, 0.125f); // Bỏ comment nếu muốn delay một chút trước khi xóa
    }
    private void Update()
    {
        //safeTimeCoolDown -= Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(5);
        //}
        if (safeTimeCoolDown > 0)
        {
            safeTimeCoolDown -= Time.deltaTime;
        }
    }
}
