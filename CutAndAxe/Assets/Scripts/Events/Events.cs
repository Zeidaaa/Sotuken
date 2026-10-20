using UnityEngine;

public struct TreeDestroyed : IEvent
{
    public Tree Tree;
    public float OverDamage;

    public TreeDestroyed(Tree tree, float overDamage)
    {
        Tree = tree;
        OverDamage = overDamage;
    }
}