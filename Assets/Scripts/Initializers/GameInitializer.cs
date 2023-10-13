using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("Tree generation parameters")]
    [SerializeField] private bool _useGenerator;
    [SerializeField] private int _minNodesCount;
    [SerializeField] private int _maxEdgesInNodeCount;
    [SerializeField] private int _maxNodeCost;
    
    [Header("Tree (if no generation) parameters")]
    [SerializeField] private List<SkillNode> _skillNodes;

    [Header("Tree drawing parameters")]
    [SerializeField] private SkillTreeView _skillTreeViewPrefab;
    [SerializeField] private SkillNodeView _skillNodeViewPrefab;
    [SerializeField] private EdgeView _edgePrefab;
    [SerializeField] private SkillTreeManagementView _skillTreeManagementView;
    [SerializeField] private Vector3 _treeStartPosition;
    [SerializeField] private Vector3 _treeVerticalOffset;
    [SerializeField] private float _treeStartHorizontalSpace;

    [Header("Misc")]
    [SerializeField] private Canvas _canvas;
    
    private void Start()
    {
        SkillTree skillTree;
        
        if (_useGenerator)
        {
            SkillTreeGenerator skillTreeGenerator = new SkillTreeGenerator(_minNodesCount, _maxEdgesInNodeCount, _maxNodeCost);
            skillTree = skillTreeGenerator.Generate();
        }
        else
        {
            SkillTreeConnector skillTreeConnector = new SkillTreeConnector(_skillNodes);
            skillTree = skillTreeConnector.Connect();
        }
        
        SkillTreeDrawer skillTreeDrawer = new SkillTreeDrawer(
            _skillTreeViewPrefab, 
            _skillNodeViewPrefab, 
            _edgePrefab, 
            _skillTreeManagementView,
            _treeStartPosition,
            _treeVerticalOffset,
            _treeStartHorizontalSpace);

        SkillTreeView skillTreeView = skillTreeDrawer.DrawTreeView(skillTree, _canvas.transform);
        SkillTreeManagementView skillTreeManagementView = skillTreeDrawer.DrawManagementView(_canvas.transform);

        SkillTreeController skillTreeController = new SkillTreeController(skillTreeView, skillTreeManagementView);
        skillTreeController.Init();
    }
}
