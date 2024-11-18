using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Tham chiếu đến Panel của Pause Menu
    private bool isPaused = false;    // Trạng thái của game (tạm dừng hoặc không)

    void Start()
    {
        // Ẩn Pause Menu khi bắt đầu game
        pauseMenuPanel.SetActive(false);
    }

    // Hàm bật/tắt chế độ Pause
    public void TogglePause()
    {
        Debug.Log("Pause button clicked");
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0; // Dừng thời gian khi tạm dừng
        }
        else
        {
            Time.timeScale = 1; // Tiếp tục thời gian khi quay lại
        }
    }

    // Hàm tiếp tục trò chơi
    public void ContinueGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Hàm thoát về Menu chính
    public void QuitToMainMenu()
    {
        Time.timeScale = 1; // Đặt lại thời gian trước khi thoát
        SceneManager.LoadScene("MainMenu"); // Thay bằng tên scene của Menu chính
    }

    // Hàm lưu trò chơi và thoát về Menu chính
    public void SaveAndExitToMainMenu()
    {
        SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    // Hàm lưu trò chơi
    public void SaveGame()
    {
        PlayerPrefs.SetInt("HasSavedGame", 1);
        // Lưu thêm các dữ liệu khác, ví dụ: vị trí của người chơi, máu, điểm
        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }

}
