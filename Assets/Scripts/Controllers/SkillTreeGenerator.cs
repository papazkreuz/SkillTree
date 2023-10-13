using System.Linq;
using UnityEngine;

public class SkillTreeGenerator
{
    private readonly int _minNodesCount;
    private readonly int _maxEdgesInNodeCount;
    private readonly int _maxNodeCost;

    public SkillTreeGenerator(int minNodesCount, int maxEdgesInNodeCount, int maxNodeCost)
    {
        _minNodesCount = minNodesCount;
        _maxEdgesInNodeCount = maxEdgesInNodeCount;
        _maxNodeCost = maxNodeCost;
    }

    public SkillTree Generate()
    {
        int generatedNodesCount = 0;
        
        SkillNode startNode = new SkillNode(generatedNodesCount, 0);
        SkillTree skillTree = new SkillTree(startNode);

        skillTree.SkillNodes.Add(startNode);
        generatedNodesCount++;

        SkillNode currentNode = startNode;
        
        while (generatedNodesCount < _minNodesCount)
        {
            int nextNodesCount = Random.Range(1, _maxEdgesInNodeCount + 1);

            for (int i = 0; i < nextNodesCount; i++)
            {
                int randomCost = Random.Range(1, _maxNodeCost + 1);
                SkillNode nextNode = new SkillNode(generatedNodesCount, randomCost);
                
                skillTree.SkillNodes.Add(nextNode);
                currentNode.AddNextNode(nextNode);
                nextNode.AddPreviousNode(currentNode);
                
                generatedNodesCount++;
            }

            currentNode = skillTree.SkillNodes.FirstOrDefault((node) => node.NextNodes == null);
        }

        return skillTree;
    }
}