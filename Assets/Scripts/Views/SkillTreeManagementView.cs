using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManagementView : MonoBehaviour
{
    [SerializeField] private SkillPointsView _skillPointsView;
    [SerializeField] private SkillInfoView _skillInfoView;
    
    [SerializeField] private Button _addSkillPointButton;
    [SerializeField] private Button _forgetAllButton;
    [SerializeField] private Button _learnSkillButton;
    [SerializeField] private Button _forgetSkillButton;

    public SkillPointsView SkillPointsView => _skillPointsView;
    public SkillInfoView SkillInfoView => _skillInfoView;
    
    public void Init(Action addSkillPoint, Action forgetAll, Action learnSkill, Action forgetSkill)
    {
        _addSkillPointButton.onClick.AddListener(() => addSkillPoint());
        _forgetAllButton.onClick.AddListener(() => forgetAll());
        _learnSkillButton.onClick.AddListener(() => learnSkill());
        _forgetSkillButton.onClick.AddListener(() => forgetSkill());
    }

    public void OnSkillStatusUpdated(bool isLearnSkillInteractable, bool isForgetSkillInteractable)
    {
        UpdateButtonsState(isLearnSkillInteractable, isForgetSkillInteractable);
    }
    
    private void UpdateButtonsState(bool isLearnSkillInteractable, bool isForgetSkillInteractable)
    {
        _learnSkillButton.interactable = isLearnSkillInteractable;
        _forgetSkillButton.interactable = isForgetSkillInteractable;
    }
}