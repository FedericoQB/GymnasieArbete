using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class AI_Advanced : BTree
{
    [SerializeField] private NPCSettings npcSettings;

    public Transform[] waypoints;

    [SerializeField] private Transform eyesTransform;

    protected override Node SetupTree()
    {
        // ACTUAL TREE THAT IS TO BE USED IN FINAL PRODUCT
        Node root = new Selector(new List<Node>
        {
            // Senses Sequence
            new Sequence(new List<Node>
            {
                new Perception_Node(this, npcSettings, eyesTransform),
            }),

            // Investigation Sequence
            new Sequence(new List<Node>
            {
                new Echolocation_Node(this, npcSettings),
                new Stalk_Node(this, npcSettings),

                // Investigate
                    // Search the Area

                    // Uses "echolocation" to further investigate the area
            }),

            // The fallback
            new Patrol_Node(this, npcSettings, waypoints),
        });

        // Testing tree
        Node root2 = new Selector(new List<Node>
        {
            // Senses Sequence
            new Sequence(new List<Node>
            {
                new Perception_Node(this, npcSettings, eyesTransform),
                new Chase_Node(this, npcSettings),
            }),

            // The fallback
            new Patrol_Node(this, npcSettings, waypoints),
        });

        // Testing Root - Replace Node to the testing Node and change return value to root2
        Node root3 = new Perception_Node(this, npcSettings, eyesTransform);

        // Can use multiple roots and change active through the return value
        return root2;
    }
}
