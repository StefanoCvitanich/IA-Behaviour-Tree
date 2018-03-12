using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.BehaviourTrees
{
    public class Action : BehaviourNode
    {
        ActionParam method;

        public Action(ActionParam action)
        {
            method = action;
        }

        public override NodeStatus Status()  //Getter del Status
        {
            return method.Invoke();
        }
    }
}