using UnityEngine;

public class SkillTreeDrawer
{
    private readonly SkillTreeView _skillTreeViewPrefab;
    private readonly SkillNodeView _skillNodeViewPrefab;
    private readonly EdgeView _edgePrefab;
    private readonly SkillTreeManagementView _skillTreeManagementViewPrefab;
    private readonly Vector3 _treeStartPosition;
    private readonly Vector3 _treeVerticalOffset;
    private readonly float _treeStartHorizontalSpace;

    public SkillTreeDrawer(
        SkillTreeView skillTreeViewPrefab, 
        SkillNodeView skillNodeViewPrefab, 
        EdgeView edgePrefab, 
        SkillTreeManagementView skillTreeManagementViewPrefab,
        Vector3 treeStartPosition,
        Vector3 treeVerticalOffset,
        float treeStartHorizontalSpace)
    {
        _skillTreeViewPrefab = skillTreeViewPrefab;
        _skillNodeViewPrefab = skillNodeViewPrefab;
        _edgePrefab = edgePrefab;
        _skillTreeManagementViewPrefab = skillTreeManagementViewPrefab;
        _treeStartPosition = treeStartPosition;
        _treeVerticalOffset = treeVerticalOffset;
        _treeStartHorizontalSpace = treeStartHorizontalSpace;
    }

    public SkillTreeView DrawTreeView(SkillTree skillTree, Transform container)
    {
        SkillTreeView skillTreeView = Object.Instantiate(_skillTreeViewPrefab, container);
        skillTreeView.Draw(
            skillTree, 
            _skillNodeViewPrefab, 
            _edgePrefab, 
            _treeStartPosition, 
            _treeVerticalOffset, 
            _treeStartHorizontalSpace);

        return skillTreeView;
    }

    public SkillTreeManagementView DrawManagementView(Transform container)
    {
        SkillTreeManagementView skillTreeManagementView = Object.Instantiate(_skillTreeManagementViewPrefab, container);

        return skillTreeManagementView;
    }
}
