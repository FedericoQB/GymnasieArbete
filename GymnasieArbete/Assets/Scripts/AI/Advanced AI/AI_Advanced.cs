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
                /* For the hearing node, it can instead of instantly initiating chase or stalk, it can add a lastknownposition
                 * Which will lead to the investigation node to be used.
                */

                // Decides between chasing or stalking depending on the player's vision
                new Selector(new List<Node>
                {
                    // First sequence runs if the player can see the Agent - Chase
                    new Sequence(new List<Node>
                    {
                        new Condition_Node(() => aI_Memory.playerSeesAI),
                        new Chase_Node(this, npcSettings)
                    }),

                    // Second sequence runs if the player can NOT see the Agent - Stalk
                    new Sequence(new List<Node>
                    {
                        new Stalk_Node(this, npcSettings)
                    }),
                }),
            }),

            new Investigate_Node(this, npcSettings),

            // The fallback
            new Patrol_Node(this, npcSettings, waypoints),
        });

        // Testing Root - Replace Node to the testing Node and change return value to root2
        Node root3 = new Perception_Node(this, npcSettings, eyesTransform);

        // Can use multiple roots and change active through the return value
        return root2;
    }
}
