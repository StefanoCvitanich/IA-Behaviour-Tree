using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.BehaviourTrees
{
    public class Logic : BehaviourNode
    {
        private LogicType type;

        public Logic(LogicType logicType)
        {
            type = logicType;
        }

        public override NodeStatus Status()
        {
            switch (type)
            {
                case LogicType.And:

                    for (int i = 0; i < children.Count; i++)
                    {
                        if (children[i].Status() == NodeStatus.False)
                        {
                            return NodeStatus.False;  // Si obtengo un solo false detengo la busqueda porque el "and" busca "trues"
                        }
                        else if (children[i].Status() == NodeStatus.Processing)
                        {
                            return NodeStatus.Processing;
                        }
                    }
                    return NodeStatus.True;

                case LogicType.Or:

                    for (int i = 0; i < children.Count; i++)
                    {
                        if (children[i].Status() == NodeStatus.True)
                        {
                            return NodeStatus.True;  //Si encuentro un true se detiene porque or busca por lo menos que se cumpla una sola condicion
                        }
                        else if (children[i].Status() == NodeStatus.Processing)
                        {
                            return NodeStatus.Processing;
                        }
                    }
                    return NodeStatus.False;

                default:
                    return NodeStatus.False;
            }
        }
    }
}