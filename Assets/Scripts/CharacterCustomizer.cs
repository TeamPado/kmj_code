using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterCustomizer : MonoBehaviour
{
    public Image previewImage;
    public Sprite[] hairOptions;
    public Sprite[] bodyOptions;

    public Dropdown hairDropdown;
    public Dropdown bodyDropdown;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Text redValueText;
    public Text greenValueText;
    public Text blueValueText;

    private Color currentColor = Color.white;

    void Start()
    {
        InitDropdown(hairDropdown, hairOptions.Length, OnHairChange);
        InitDropdown(bodyDropdown, bodyOptions.Length, OnBodyChange);

        redSlider.onValueChanged.AddListener(UpdateColor);
        greenSlider.onValueChanged.AddListener(UpdateColor);
        blueSlider.onValueChanged.AddListener(UpdateColor);

        UpdateColor(0); // 초기 색상 적용 및 텍스트 갱신
    }

    private void InitDropdown(Dropdown dropdown, int count, UnityEngine.Events.UnityAction<int> callback)
    {
        dropdown.ClearOptions();
        var options = new List<string>();
        for (int i = 0; i < count; i++) options.Add($"Option {i + 1}");
        dropdown.AddOptions(options);
        dropdown.onValueChanged.AddListener(callback);
    }

    private void OnHairChange(int index)
    {
        previewImage.sprite = hairOptions[index];
        ApplyColor();
    }

    private void OnBodyChange(int index)
    {
        previewImage.sprite = bodyOptions[index];
        ApplyColor();
    }

    private void UpdateColor(float _)
    {
        currentColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        redValueText.text = Mathf.RoundToInt(redSlider.value * 255).ToString();
        greenValueText.text = Mathf.RoundToInt(greenSlider.value * 255).ToString();
        blueValueText.text = Mathf.RoundToInt(blueSlider.value * 255).ToString();
        ApplyColor();
    }

    private void ApplyColor()
    {
        if (previewImage != null)
            previewImage.color = currentColor;
    }
}