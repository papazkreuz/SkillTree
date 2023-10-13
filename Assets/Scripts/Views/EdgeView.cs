using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EdgeView : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    
    public void Init(SkillNodeView startNodeView, SkillNodeView endNodeView)
    {
        _lineRenderer = GetComponent<LineRenderer>();

        Vector3 startNodePosition = startNodeView.transform.localPosition;
        Vector3 endNodePosition = endNodeView.transform.localPosition;
        
        _lineRenderer.SetPosition(0, startNodePosition);
        _lineRenderer.SetPosition(1, endNodePosition);
        
        gameObject.name = $"Edge_{startNodeView.SkillNode.Id}-{endNodeView.SkillNode.Id}";
    } 
}