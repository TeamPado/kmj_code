using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public int damage;
    public int cooldown;
    public int currentCooldown;

    public void Use(Unit user, Unit target)
    {
        target.TakeDamage(damage);
        currentCooldown = cooldown;
        Debug.Log($"{user.unitName} uses {skillName}! ({damage} damage)");
    }

    public void TickCooldown()
    {
        if (currentCooldown > 0)
            currentCooldown--;
    }
}
