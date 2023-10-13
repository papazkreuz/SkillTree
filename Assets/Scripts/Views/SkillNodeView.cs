using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _chosenMark;

    private readonly Color _forgottenBackgroundColor = Color.white;
    private readonly Color _learnedBackgroundColor = Color.cyan;

    private SkillNode _skillNode;

    public SkillNode SkillNode => _skillNode;

    public void Init(SkillNode skillNode)
    {
        _skillNode = skillNode;

        gameObject.name = $"SkillNode_{_skillNode.Id}";
        _text.text = $"{_skillNode.Id}";

        SetUnselected();

        if (_skillNode.IsLearned)
        {
            SetLearned();
        }
        else
        {
            SetForgotten();
        }
        
        _skillNode.SkillSelected += SetSelected;
        _skillNode.SkillUnselected += SetUnselected;

        _skillNode.SkillLearned += SetLearned;
        _skillNode.SkillForgotten += SetForgotten;
    }

    public void SetButtonAction(Action<SkillNode> selectSkillNode)
    {
        _button.onClick.AddListener(() => selectSkillNode(_skillNode));
    }

    private void SetSelected()
    {
        _chosenMark.SetActive(true);
    }

    private void SetUnselected()
    {
        _chosenMark.SetActive(false);
    }

    private void SetLearned()
    {
        _background.color = _learnedBackgroundColor;
    }

    private void SetForgotten()
    {
        _background.color = _forgottenBackgroundColor;
    }
}
