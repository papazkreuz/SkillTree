using System.Collections.Generic;

public class SkillTree
{
    public SkillNode StartNode { get; private set; }
    public List<SkillNode> SkillNodes { get; private set; }

    public SkillTree(SkillNode startNode)
    {
        StartNode = startNode;
        SkillNodes = new List<SkillNode>();
        
        StartNode.Learn();
    }
}
