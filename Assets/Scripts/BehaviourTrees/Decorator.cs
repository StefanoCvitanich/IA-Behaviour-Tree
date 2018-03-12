using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.BehaviourTrees
{
    public class Decorator : BehaviourNode
    {
        public override NodeStatus Status()  //Getter del Status
        {
            if (children[0].Status() == NodeStatus.False)  //Como es el decorador niego el status que paso el hijo
            {
                return NodeStatus.True;
            }
            else if (children[0].Status() == NodeStatus.Processing)  //El status de procesando NO se modifica
            {
                return NodeStatus.Processing;
            }
            else  //Este ultimo caso es si el hijo devuelve True y se niega a False
            {
                return NodeStatus.False;
            }
        }
    }
}