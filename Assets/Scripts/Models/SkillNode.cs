using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SkillNode
{
    [SerializeField] private int _id;
    [SerializeField] private int _cost;
    [SerializeField] private List<int> _nextNodesIds;

    [NonSerialized] private bool _isLearned;
    [NonSerialized] private bool _isSelected;
    [NonSerialized] private List<SkillNode> _nextNodes;
    [NonSerialized] private List<SkillNode> _previousNodes;

    public int Id => _id;
    public int Cost => _cost;
    public List<int> NextNodesIds => _nextNodesIds;
    public bool IsSelected => _isSelected;
    public bool IsLearned => _isLearned;
    public List<SkillNode> NextNodes => _nextNodes;
    public List<SkillNode> PreviousNodes => _previousNodes;

    public Action SkillSelected { get; set; }
    public Action SkillUnselected { get; set; }
    public Action SkillLearned { get; set; }
    public Action SkillForgotten { get; set; }

    public SkillNode(int id, int cost)
    {
        _id = id;
        _cost = cost;
    }

    public override bool Equals(object obj)
    {
        SkillNode skillNode = obj as SkillNode;

        if (skillNode == null)
        {
            return false;
        }
        
        return Id == skillNode.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void Learn()
    {
        _isLearned = true;
        SkillLearned?.Invoke();
    }

    public void Forget()
    {
        _isLearned = false;
        SkillForgotten?.Invoke();
    }

    public void Select()
    {
        _isSelected = true;
        SkillSelected?.Invoke();
    }

    public void Unselect()
    {
        _isSelected = false;
        SkillUnselected?.Invoke();
    }
    
    public void AddNextNode(SkillNode nextNode)
    {
        if (_nextNodes == null)
        {
            _nextNodes = new List<SkillNode>();
        }
        
        _nextNodes.Add(nextNode);
    }

    public void AddPreviousNode(SkillNode previousNode)
    {
        if (_previousNodes == null)
        {
            _previousNodes = new List<SkillNode>();
        }
        
        _previousNodes.Add(previousNode);
    }
    
    public bool CanBeLearned()
    {
        bool hasPreviousNodes = PreviousNodes != null;
        bool previousNodeIsLearned = PreviousNodes?.FirstOrDefault((nextNode) => nextNode.IsLearned) != null;

        return IsLearned == false && hasPreviousNodes && previousNodeIsLearned;

    }

    public bool CanBeForgotten()
    {
        bool hasPreviousNodes = PreviousNodes != null;
        bool hasNextNodes = NextNodes != null;
        bool nextNodeIsLearned = NextNodes?.FirstOrDefault((nextNode) => nextNode.IsLearned) != null;
        bool nextNodeHasAnotherPreviousLearnedNode = false;

        if (hasNextNodes && nextNodeIsLearned)
        {
            foreach (SkillNode nextNode in NextNodes)
            {
                if (nextNode.PreviousNodes?.FirstOrDefault((node) => node.Equals(this) == false && node.IsLearned) != null)
                {
                    nextNodeHasAnotherPreviousLearnedNode = true;
                    break;
                }
            }
        }
        
        return IsLearned && (hasNextNodes == false || nextNodeIsLearned == false || nextNodeHasAnotherPreviousLearnedNode) && hasPreviousNodes;
    }
}
