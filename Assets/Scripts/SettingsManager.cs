using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Button settingButton;
    public GameObject settingPanel;
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
       
        settingPanel.SetActive(false);

        
        settingButton.onClick.AddListener(ToggleSettingsPanel);

      
        volumeSlider.value = audioSource.volume;

        
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    
    void ToggleSettingsPanel()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

   
    void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
