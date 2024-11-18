using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkillTopDown : MonoBehaviour
{
    public float teleportDistance = 5f; // Khoảng cách dịch chuyển
    public GameObject teleportEffectPrefab; // Prefab hiệu ứng dịch chuyển
    public float effectDuration = 1f; // Thời gian tồn tại của hiệu ứng
    public LayerMask obstacleLayer; // Lớp vật cản (các đối tượng có BoxCollider2D)
    public float cooldownTime = 3f; // Thời gian hồi chiêu (seconds)

    private float lastTeleportTime = -Mathf.Infinity; // Thời gian của lần dịch chuyển cuối cùng

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Time.time >= lastTeleportTime + cooldownTime) // Kiểm tra hồi chiêu
        {
            Teleport();
            lastTeleportTime = Time.time; // Cập nhật thời gian lần dịch chuyển cuối
        }
    }

    void Teleport()
    {
        // Kiểm tra xem đã gắn Prefab hiệu ứng chưa
        if (teleportEffectPrefab != null)
        {
            // Tạo hiệu ứng tại vị trí hiện tại trước khi dịch chuyển
            GameObject effect = Instantiate(teleportEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectDuration); // Xóa hiệu ứng sau một khoảng thời gian
        }

        // Lấy hướng từ đối tượng đến vị trí chuột
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Đảm bảo vị trí Z không thay đổi

        // Tính toán vector hướng dịch chuyển
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Tính toán vị trí mới
        Vector3 targetPosition = transform.position + direction * teleportDistance;

        // Kiểm tra xem có vật cản trong hướng dịch chuyển hay không
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, teleportDistance, obstacleLayer);

        // Nếu có vật cản, dừng dịch chuyển tại điểm va chạm
        if (hit.collider != null)
        {
            targetPosition = hit.point;
        }

        // Dịch chuyển đến vị trí mới
        transform.position = targetPosition;

        // Tạo hiệu ứng tại vị trí mới sau khi dịch chuyển
        if (teleportEffectPrefab != null)
        {
            GameObject effect = Instantiate(teleportEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectDuration); // Xóa hiệu ứng sau một khoảng thời gian
        }
    }
}
