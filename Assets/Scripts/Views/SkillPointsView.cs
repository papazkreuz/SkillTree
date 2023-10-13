using TMPro;
using UnityEngine;

public class SkillPointsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void OnSkillPointsChange(int skillPoints)
    {
        _text.text = $"Skill Points: {skillPoints}";
    }
}
