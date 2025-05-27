using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int maxHP;
    public int currentHP;
    public int ammo;
    public int morale;
    public int speed;
    public bool isPlayerControlled;
    public int attackDamage;

    public Skill activeSkill;  // 단일 스킬 예시

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log($"{unitName} has been defeated.");
            gameObject.SetActive(false);
        }
    }
}
