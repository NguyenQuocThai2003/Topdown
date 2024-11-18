using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button continueButton; // Tham chiếu đến nút Continue

    void Start()
    {
        //// Kiểm tra nếu có game đã lưu, nếu không thì vô hiệu hóa nút Continue
        //if (PlayerPrefs.HasKey("HasSavedGame") && PlayerPrefs.GetInt("HasSavedGame") == 1)
        //{
        //    continueButton.interactable = true; // Kích hoạt nút Continue nếu có dữ liệu đã lưu
        //}
        //else
        //{
        //    continueButton.interactable = false; // Vô hiệu hóa nút Continue nếu chưa có dữ liệu đã lưu
        //}
    }

    public void LoadGame()
    {
        // Bắt đầu game mới
        SceneManager.LoadScene("Game");
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("HasSavedGame"))
        {
            // Tải scene game (hoặc vị trí lưu cuối cùng)
            SceneManager.LoadScene("Game");

            // Khi vào game, có thể cần logic để thiết lập vị trí và trạng thái từ dữ liệu lưu.
            // Ví dụ: đọc PlayerPrefs để thiết lập vị trí, máu, điểm của người chơi.
        }
        else
        {
            Debug.Log("Không có dữ liệu game để tiếp tục.");
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
