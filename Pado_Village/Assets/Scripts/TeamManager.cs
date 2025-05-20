using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public TeamSlot[] slots; // 슬롯 4개 연결
    public Sprite[] availableCharacters; // 사용할 병사 프리셋 이미지

    public void AddRandomCharacterToTeam()
    {
        foreach (TeamSlot slot in slots)
        {
            if (!slot.isOccupied)
            {
                // 예시로 랜덤 병사 배치
                int index = Random.Range(0, availableCharacters.Length);
                slot.SetCharacter(availableCharacters[index]);
                break;
            }
        }
    }
}
