using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.BehaviourTrees
{
    public class Selector : BehaviourNode
    {
        public override NodeStatus Status()
        {
            for (int i = lockIndex; i < children.Count; i++)
            {
                if (children[i].Status() == NodeStatus.True)
                {
                    lockIndex = 0;
                    return NodeStatus.True;  // EL selectOR se detiene cuando obtiene un True
                }
                else if (children[i].Status() == NodeStatus.Processing)
                {
                    lockIndex = i;
                    HaltTree(children[i]);
                    return NodeStatus.Processing;  // Cuando obtiene un processing detiene el arbol y devuelve procesing
                }
            }
            lockIndex = 0;
            return NodeStatus.False;
        }
    }
}