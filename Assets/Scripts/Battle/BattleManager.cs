using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public Transform playerSpawn;
    public Transform enemySpawn;
    public TurnUI turnUI;
    public Button attackButton;
    public Button skillButton;
    public TextMeshProUGUI skillCooldownText;

    public List<Unit> allUnits = new List<Unit>();
    private Queue<Unit> turnQueue = new Queue<Unit>();
    public Unit currentUnit;

    void Start()
    {
    SpawnPlayerUnit();
    SpawnEnemyUnit();
    SetupTurnQueue();
    UpdateButtonInteractability();
    NextTurn();
    }

    void SpawnPlayerUnit()
    {
        GameObject go = Instantiate(unitPrefab, playerSpawn.position, Quaternion.identity);
        Unit unit = go.GetComponent<Unit>();

        unit.unitName = "플레이어";
        unit.maxHP = unit.currentHP = 20;
        unit.attackDamage = 5;
        unit.ammo = 999;
        unit.morale = 100;
        unit.speed = 10;
        unit.isPlayerControlled = true;

        // 스킬 설정
        unit.activeSkill = new Skill
        {
            skillName = "강타",
            damage = 10,
            cooldown = 2,
            currentCooldown = 0
        };

        allUnits.Add(unit);
    }

    void SpawnEnemyUnit()
    {
        GameObject go = Instantiate(unitPrefab, enemySpawn.position, Quaternion.identity);
        Unit unit = go.GetComponent<Unit>();

        unit.unitName = "적 병사";
        unit.maxHP = unit.currentHP = 30;
        unit.attackDamage = 3;
        unit.ammo = 999;
        unit.morale = 100;
        unit.speed = 8;
        unit.isPlayerControlled = false;

        allUnits.Add(unit);
    }

    void SetupTurnQueue()
    {
        turnQueue = new Queue<Unit>(allUnits.Where(u => u.currentHP > 0).OrderByDescending(u => u.speed));
        turnUI.UpdateTurnOrderUI(turnQueue.ToList());
    }

    public void NextTurn()
    {
        if (turnQueue.Count == 0) SetupTurnQueue();

        currentUnit = turnQueue.Dequeue();

        currentUnit?.activeSkill?.TickCooldown();

        Debug.Log($"[턴] {currentUnit.unitName}의 턴입니다.");

        UpdateButtonInteractability();

        if (!currentUnit.isPlayerControlled)
        {
            StartCoroutine(AIAction());
        }
    }


    IEnumerator AIAction()
    {
        yield return new WaitForSeconds(1f);

        Unit target = allUnits.Find(u => u.isPlayerControlled && u.currentHP > 0);
        if (target != null)
        {
            target.TakeDamage(currentUnit.attackDamage);
            Debug.Log($"{currentUnit.unitName}이(가) {target.unitName}에게 공격! ({currentUnit.attackDamage} 데미지)");
        }

        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void UseAttack()
    {
        if (currentUnit == null || !currentUnit.isPlayerControlled) return;

        Unit target = allUnits.Find(u => !u.isPlayerControlled && u.currentHP > 0);
        if (target != null)
        {
            target.TakeDamage(currentUnit.attackDamage);
            Debug.Log($"{currentUnit.unitName}이(가) {target.unitName}에게 기본 공격! ({currentUnit.attackDamage} 데미지)");
        }

        NextTurn();
    }


    public void UseSkill()
    {
        if (currentUnit == null || !currentUnit.isPlayerControlled) return;

        Skill skill = currentUnit.activeSkill;
        if (skill == null || skill.currentCooldown > 0)
        {
            Debug.Log("스킬을 사용할 수 없습니다.");
            return;
        }

        Unit target = allUnits.Find(u => !u.isPlayerControlled && u.currentHP > 0);
        if (target != null)
        {
            skill.Use(currentUnit, target);
        }

        NextTurn();
    }
    void UpdateButtonInteractability()
    {
        if (currentUnit != null && currentUnit.isPlayerControlled)
        {
            attackButton.interactable = true;

            Skill skill = currentUnit.activeSkill;
            skillButton.interactable = skill != null && skill.currentCooldown <= 0;

            if (skillCooldownText != null)
            {
                if (skill != null && skill.currentCooldown > 0)
                    skillCooldownText.text = $"스킬 쿨다운: {skill.currentCooldown}턴";
                else
                    skillCooldownText.text = "";
            }

        }
        else
        {
            attackButton.interactable = false;
            skillButton.interactable = false;
            if (skillCooldownText != null)
                skillCooldownText.text = "";
        }
    }

}