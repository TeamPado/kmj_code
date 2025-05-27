using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Option : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public Dropdown languageDropdown;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private void Start()
    {
        // 볼륨 설정 불러오기
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        AudioListener.volume = volumeSlider.value;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 해상도 중복 제거 및 설정
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        List<string> resolutionOptions = new List<string>();
        HashSet<string> resolutionSet = new HashSet<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            var res = resolutions[i];
            string resKey = res.width + "x" + res.height;

            if (!resolutionSet.Contains(resKey))
            {
                resolutionSet.Add(resKey);
                filteredResolutions.Add(res);
                resolutionOptions.Add(resKey);

                if (res.width == Screen.currentResolution.width &&
                    res.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = filteredResolutions.Count - 1;
                }
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener((index) =>
        {
            Resolution selectedRes = filteredResolutions[index];
            Screen.SetResolution(selectedRes.width, selectedRes.height, fullscreenToggle.isOn);
        });

        // 전체 화면 설정
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullScreen);

        // 언어 설정 불러오기 (기본: 0번 인덱스)
        languageDropdown.value = PlayerPrefs.GetInt("Language", 0);
        languageDropdown.onValueChanged.AddListener(SetLanguage);

        settingsPanel.SetActive(false); // 옵션창 처음엔 비활성화
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    private void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void SetLanguage(int index)
    {
        PlayerPrefs.SetInt("Language", index);
        Debug.Log("언어 변경: " + index);
    }
}
