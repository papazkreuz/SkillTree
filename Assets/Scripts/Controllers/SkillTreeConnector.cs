using System.Collections.Generic;
using System.Linq;

public class SkillTreeConnector
{
    private readonly List<SkillNode> _skillNodes;

    public SkillTreeConnector(List<SkillNode> skillNodes)
    {
        _skillNodes = skillNodes;
    }

    public SkillTree Connect()
    {
        SkillTree skillTree = new SkillTree(_skillNodes[0]);
        
        _skillNodes.ForEach((currentNode) =>
        {
            skillTree.SkillNodes.Add(currentNode);
            
            foreach (SkillNode nextNode in _skillNodes.Where((node) => currentNode.NextNodesIds.Contains(node.Id)))
            {
                currentNode.AddNextNode(nextNode);
            }

            currentNode.NextNodes?.ForEach((node) => node.AddPreviousNode(currentNode));
        });

        return skillTree;
    }
}