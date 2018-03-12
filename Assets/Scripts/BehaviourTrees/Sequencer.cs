using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.BehaviourTrees
{
    public class Sequencer : BehaviourNode
    {
        public override NodeStatus Status()
        {
            for (int i = lockIndex; i < children.Count; i++)
            {
                if (children[i].Status() == NodeStatus.False)
                {
                    lockIndex = 0;
                    return NodeStatus.False;  // El secuenciAND se detiene cuando obtiene un False
                }
                else if (children[i].Status() == NodeStatus.Processing)
                {
                    lockIndex = i;
                    HaltTree(children[i]);
                    return NodeStatus.Processing;  //Si obtiene un procesing detiene el arbol y devuelve procesing
                }
            }
            lockIndex = 0;
            return NodeStatus.True;
        }
    }
}