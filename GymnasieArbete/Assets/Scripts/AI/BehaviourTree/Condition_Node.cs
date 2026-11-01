using BehaviourTree;
using UnityEngine;

public class Condition_Node : Node
{
    System.Func<bool> condition;

    // Using a function bool to update in realtime a variable in the constructor
    public Condition_Node(System.Func<bool> condition)
    {
        this.condition = condition;
    }

    public override NodeState Evaluate()
    {
        return condition() ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
