using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalitySkill2D : MonoBehaviour
{
    public GameObject shieldPrefab; // Prefab vòng bảo vệ (nếu muốn có hiệu ứng khi hồi máu)
    public int healAmount = 20;     // Lượng máu hồi mỗi lần kích hoạt
    public float skillDuration = 1f; // Thời gian tồn tại của vòng bảo vệ (hiệu ứng)
    public float cooldown = 20f;    // Thời gian hồi chiêu

    private GameObject activeShield;
    private float nextUseTime = 0f;
    private bool isHealing = false;

    void Update()
    {
        // Nhấn phím E để kích hoạt kỹ năng hồi máu
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextUseTime)
        {
            ActivateHealing();
            nextUseTime = Time.time + cooldown; // Cập nhật thời gian hồi chiêu
        }
    }

    void ActivateHealing()
    {
        // Kích hoạt hồi máu
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount); // Gọi phương thức hồi máu từ PlayerHealth
            Debug.Log("Player healed for: " + healAmount + " HP");
        }

        // Hiệu ứng vòng bảo vệ (nếu có)
        if (shieldPrefab != null)
        {
            activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            activeShield.transform.SetParent(transform); // Gắn vòng bảo vệ vào nhân vật
            activeShield.transform.localPosition = new Vector3(0, -4.5f, 0); // Đặt vị trí của vòng bảo vệ
            Destroy(activeShield, skillDuration); // Hủy vòng bảo vệ sau khi hết thời gian
        }
    }
}
