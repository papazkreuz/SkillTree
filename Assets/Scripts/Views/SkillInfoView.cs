using TMPro;
using UnityEngine;

public class SkillInfoView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void OnSkillSelected(SkillNode skillNode)
    {
        if (skillNode != null)
        {
            _text.text = $"Skill Id: {skillNode.Id}<br>Skill Cost: {skillNode.Cost}";
        }
        else
        {
            _text.text = string.Empty;
        }
    }
}