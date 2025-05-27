using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider slider;
    public Unit unit;

    void Start()
    {
        if (unit == null)
        {
            unit = GetComponentInParent<Unit>();
            if (unit == null)
            {
                Debug.LogError("[HPBar] Unit이 부모에서 발견되지 않았습니다.");
            }
        }

        if (slider == null)
        {
            slider = GetComponent<Slider>();
            if (slider == null)
            {
                Debug.LogError("[HPBar] Slider가 연결되지 않았습니다.");
            }
        }

        slider.maxValue = unit.maxHP;
        slider.value = unit.currentHP;
    }

    void Update()
    {
        if (unit != null && slider != null)
        {
            slider.value = unit.currentHP;
        }
    }
}
