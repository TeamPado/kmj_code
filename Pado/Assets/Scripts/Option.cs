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
        // ���� ���� �ҷ�����
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        AudioListener.volume = volumeSlider.value;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // �ػ� �ߺ� ���� �� ����
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

        // ��ü ȭ�� ����
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullScreen);

        // ��� ���� �ҷ����� (�⺻: 0�� �ε���)
        languageDropdown.value = PlayerPrefs.GetInt("Language", 0);
        languageDropdown.onValueChanged.AddListener(SetLanguage);

        settingsPanel.SetActive(false); // �ɼ�â ó���� ��Ȱ��ȭ
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
        Debug.Log("��� ����: " + index);
    }
}
