using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class AI_Advanced : BTree
{
    [SerializeField] private NPCSettings npcSettings;

    public Transform[] waypoints;


    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            // Senses Sequence
            new Sequence(new List<Node>
            {
                // Each unique Senses, if one picks something up, it will return succesful
                new Selector(new List<Node>
                {
                    // Different Perceptions
                }),

                // Investigate
                new Sequence(new List<Node>
                {
                    // Search the Area

                    // Uses "echolocation" to further investigate the area
                    new Echolocation_Node(this, npcSettings),
                }),
                new Chase_Node(this, npcSettings),
            }),

            // The fallback
            new TaskPatrol(this, npcSettings, waypoints),
        });

        //Node root2 = new Echolocation_Node(this, npcSettings);

        // Can use multiple Nodes and change through the return value
        return root;
    }
}
