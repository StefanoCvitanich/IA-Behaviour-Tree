using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.BehaviourTrees
{
    public class Conditional : BehaviourNode
    {
        ConditionParam param;

        public Conditional(ConditionParam watch)
        {
            param = watch;
        }

        public override NodeStatus Status()  //Getter del status
        {
            if (param.Invoke())
                return NodeStatus.True;
            return NodeStatus.False;
        }
    }
}