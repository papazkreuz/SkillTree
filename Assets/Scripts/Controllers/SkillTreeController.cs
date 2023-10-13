using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillTreeController
{
    private SkillTreeView _skillTreeView;
    private SkillTreeManagementView _skillTreeManagementView;

    private int _skillPoints;
    private SkillNode _selectedSkillNode;

    private Action<int> _skillPointsChanged;
    private Action<SkillNode> _skillNodeSelected;
    private Action<bool, bool> _skillStatusUpdated;

    public SkillTreeController(SkillTreeView skillTreeView, SkillTreeManagementView skillTreeManagementView)
    {
        _skillTreeView = skillTreeView;
        _skillTreeManagementView = skillTreeManagementView;
    }

    public void Init()
    {
        _skillPoints = 0;

        _skillTreeView.Init(SelectSkillNode);
        _skillTreeManagementView.Init(AddSkillPoint, ForgetAllSkills, LearnSelectedSkill, ForgetSelectedSkill);

        _skillPointsChanged += _skillTreeManagementView.SkillPointsView.OnSkillPointsChange;
        _skillNodeSelected += _skillTreeManagementView.SkillInfoView.OnSkillSelected;
        _skillStatusUpdated += _skillTreeManagementView.OnSkillStatusUpdated;
    }

    private void SelectSkillNode(SkillNode skillNode)
    {
        if (_selectedSkillNode != null)
        {
            UnselectSkillNode(_selectedSkillNode);
        }

        skillNode.Select();

        _skillNodeSelected.Invoke(skillNode);
        _skillStatusUpdated.Invoke(skillNode.CanBeLearned() && IsEnoughPointsToBuySkill(skillNode),
            skillNode.CanBeForgotten());

        _selectedSkillNode = skillNode;
    }

    private void UnselectSkillNode(SkillNode skillNode)
    {
        skillNode.Unselect();

        _skillNodeSelected.Invoke(null);
        _selectedSkillNode = null;
    }

    private void AddSkillPoint()
    {
        AddSkillPoints(1);
    }

    private void AddSkillPoints(int value)
    {
        if (value <= 0)
        {
            Debug.LogError("Can't add negative or zero number of skill points");
            return;
        }

        _skillPoints += value;

        _skillPointsChanged.Invoke(_skillPoints);
        _skillStatusUpdated.Invoke(_selectedSkillNode.CanBeLearned() && IsEnoughPointsToBuySkill(_selectedSkillNode),
            _selectedSkillNode.CanBeForgotten());
    }

    private void SpendSkillPoints(int value)
    {
        if (value <= 0)
        {
            Debug.LogError("Can't spend negative or zero number of skill points");
            return;
        }

        _skillPoints -= value;

        _skillPointsChanged.Invoke(_skillPoints);
        _skillStatusUpdated.Invoke(_selectedSkillNode.CanBeLearned() && IsEnoughPointsToBuySkill(_selectedSkillNode),
            _selectedSkillNode.CanBeForgotten());
    }

    private void ForgetAllSkills()
    {
        foreach (SkillNodeView nodeView in _skillTreeView.SkillNodeViews.OrderByDescending((view) => view.SkillNode.Id))
        {
            ForgetSkill(nodeView.SkillNode);
        }
    }

    private void LearnSkill(SkillNode skillNode)
    {
        if (skillNode.CanBeLearned() && IsEnoughPointsToBuySkill(skillNode))
        {
            skillNode.Learn();
            SpendSkillPoints(skillNode.Cost);
        }
        else
        {
            Debug.Log("Can't learn this skill.");
        }
    }

    private void LearnSelectedSkill()
    {
        LearnSkill(_selectedSkillNode);
    }

    private void ForgetSkill(SkillNode skillNode)
    {
        if (skillNode.CanBeForgotten())
        {
            skillNode.Forget();
            AddSkillPoints(skillNode.Cost);
        }
        else
        {
            Debug.Log("Can't forget this skill.");
        }
    }

    private void ForgetSelectedSkill()
    {
        ForgetSkill(_selectedSkillNode);
    }

    private bool IsEnoughPointsToBuySkill(SkillNode skillNode)
    {
        return _skillPoints >= skillNode.Cost;
    }
}