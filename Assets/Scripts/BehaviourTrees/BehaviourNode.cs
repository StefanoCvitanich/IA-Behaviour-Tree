using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.BehaviourTrees
{
    public delegate bool ConditionParam();
    public delegate NodeStatus ActionParam();
    public delegate void MethodToExecute();

    public enum NodeStatus
    {
        True,
        False,
        Processing
    }

    public enum LogicType
    {
        And,
        Or
    }

    public enum ConditionType
    {
        Equals,
        NotEquals,
        IsGreater,
        IsLower,
        IsGreaterOrEqual,
        IsLowerOrEqual,
    }

    public abstract class BehaviourNode : Object
    {
        protected List<BehaviourNode> children;
        protected BehaviourNode parent = null;
        protected int lockIndex = 0;
        private BhTree tree = null;

        public BehaviourNode()
        {
            children = new List<BehaviourNode>();
        }

        public void AddChild(BehaviourNode child)
        {
            child.parent = this;
            children.Add(child);
        }

        public bool RemoveChild(BehaviourNode child)
        {
            if (children.Contains(child))  // Busco si la lista de hijos contiene al nodo que mande a borrar
            {
                children.Remove(child);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HaltTree(BehaviourNode action)
        {
            if (parent)   //voy avisando a los nodos padre que no hay que seguir recorriendo el arbol. Hay algo ejecutando.
            {
                parent.HaltTree(action);
            }
            else
            {
                tree.HaltTree(action);
            }
        }

        public void SetTree(BhTree tgtTree)
        {
            tree = tgtTree;
        }

        public abstract NodeStatus Status();
    }
}