using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillTreeView : MonoBehaviour
{
    [SerializeField] private Transform _edgesContainer;
    [SerializeField] private Transform _nodesContainer;

    private SkillTree _skillTree;
    private List<SkillNodeView> _skillNodeViews;
    private SkillNodeView _selectedSkillNodeView;

    public List<SkillNodeView> SkillNodeViews => _skillNodeViews;
    
    public void Draw(
        SkillTree skillTree, 
        SkillNodeView skillNodeViewPrefab, 
        EdgeView edgePrefab,
        Vector3 treeStartPosition,
        Vector3 treeVerticalOffset,
        float treeStartHorizontalSpace
        )
    {
        _skillTree = skillTree;
        _skillNodeViews = new List<SkillNodeView>();

        DrawSkillNodeViews(skillNodeViewPrefab);
        
        PlaceNodeAndNextNodesViews(
            skillTree.StartNode, 
            treeStartPosition, 
            treeVerticalOffset, 
            treeStartHorizontalSpace);
        
        DrawEdges(edgePrefab);
    }

    public void Init(Action<SkillNode> selectSkillNode)
    {
        _skillNodeViews.ForEach((skillNodeView) => skillNodeView.SetButtonAction(selectSkillNode));
    }
    
    private void DrawSkillNodeViews(SkillNodeView skillNodeViewPrefab)
    {
        foreach (SkillNode skillNode in _skillTree.SkillNodes)
        {
            SkillNodeView skillNodeView = Instantiate(skillNodeViewPrefab, _nodesContainer);
            skillNodeView.Init(skillNode);

            _skillNodeViews.Add(skillNodeView);
        }
    }

    private void DrawEdges(EdgeView edgePrefab)
    {
        foreach (SkillNodeView skillNodeView in _skillNodeViews)
        {
            if (skillNodeView.SkillNode.NextNodes == null)
            {
                continue;
            }

            foreach (SkillNode nextNode in skillNodeView.SkillNode.NextNodes)
            {
                EdgeView edgeView = Instantiate(edgePrefab, _edgesContainer);
                SkillNodeView nextNodeView = GetSkillNodeView(nextNode);

                edgeView.Init(skillNodeView, nextNodeView);
            }
        }
    }
    
    private void PlaceNodeAndNextNodesViews(SkillNode skillNode, Vector3 position, Vector3 verticalOffset, float horizontalSpace)
    {
        SkillNodeView nodeView = GetSkillNodeView(skillNode);
        
        nodeView.transform.localPosition = position;

        if (skillNode.NextNodes == null)
        {
            return;
        }

        int nextNodesCount = skillNode.NextNodes.Count;
        float newHorizontalSpace = horizontalSpace / nextNodesCount;

        for (int i = 0; i < nextNodesCount; i++)
        {
            int positionMultiplier = nextNodesCount / 2 - i;

            if (nextNodesCount % 2 == 0 && i == nextNodesCount / 2)
            {
                positionMultiplier--;
            }
            
            Vector3 horizontalOffset = Vector3.left * newHorizontalSpace * positionMultiplier;

            PlaceNodeAndNextNodesViews(
                skillNode.NextNodes[i], 
                position + verticalOffset + horizontalOffset, 
                verticalOffset, 
                newHorizontalSpace);
        }
    }
    
    private SkillNodeView GetSkillNodeView(SkillNode skillNode)
    {
        return _skillNodeViews.FirstOrDefault((skillNodeView) => skillNodeView.SkillNode.Equals(skillNode));
    }
}