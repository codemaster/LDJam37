using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentNode : MonoBehaviour {

    /// <summary>
    /// The size of the sphere.
    /// </summary>
    private static float gizmoSphereSize = .5f;

    /// <summary>
    /// The max player scent.
    /// </summary>
    public float MaxPlayerScent;

    /// <summary>
    /// The current player scent.
    /// </summary>
    public float PlayerScent;

    /// <summary>
    /// The scent decay frequency in %(0-1) of MaxPlayerScent per second.
    /// </summary>
    public float ScentDecayFrequency;

    /// <summary>
    /// The linked nodes.
    /// </summary>
    public List<ScentNode> LinkedNodes;

    void Update()
    {
        FadeScent();
    }

    /// <summary>
    /// Fades the scent.
    /// </summary>
    private void FadeScent()
    {
        if(PlayerScent > 0)
            PlayerScent -= MaxPlayerScent * ScentDecayFrequency * Time.deltaTime;
        if (PlayerScent < 0)
            PlayerScent = 0;
    }

    /// <summary>
    /// Gets the liked node with a higher scent than this one. If none has higher scent than returns null;
    /// </summary>
    /// <returns>The smellier neighbour.</returns>
    public ScentNode GetSmellierNeighbour()
    {
        foreach (var node in LinkedNodes)
        {
            if (node.PlayerScent > PlayerScent)
                return node;
        }
        return null;
    }

    /// <summary>
    /// Adds a linked node.
    /// </summary>
    /// <param name="node">Node.</param>
    public void AddLinkedNode(ScentNode node)
    {
        // Not doing any validations for now as I don't know how this will be integrated in the procedural generation system
        LinkedNodes.Add(node);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            PlayerScent = MaxPlayerScent;
    }

    #region Gizmo code
    /// <summary>
    /// Raises the draw gizmos event.
    /// </summary>
    void OnDrawGizmos()
    {
        DrawGizmos(Color.yellow);
    }

    /// <summary>
    /// Raises the draw selected gizmos event.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        DrawGizmos(Color.cyan);
    }

    private void DrawGizmos(Color gizmosColor)
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSphereSize);
        if (LinkedNodes != null)
        {
            foreach (var node in LinkedNodes)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
    #endregion
}
