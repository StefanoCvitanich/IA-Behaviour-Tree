using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.BehaviourTrees
{
    public class BhTree
    {
        private Dictionary<string, BehaviourNode> nodes = new Dictionary<string, BehaviourNode>();
        private List<BehaviourNode> firstLine = new List<BehaviourNode>();
        private BehaviourNode actionInProgress = null;
        private int lockIndex = 0;

        public void UpdateTree()
        {
            if (actionInProgress)
            {
                NodeStatus status = actionInProgress.Status();
                if(status == NodeStatus.Processing)  
                {
                    return;
                }
                else
                {
                    actionInProgress = null;
                }
            }
            
            for(int i = lockIndex; i < firstLine.Count; i++)
            {
				NodeStatus status = firstLine[i].Status();  //Si hay algun nodo de la primera linea que esta procesando, dejo de recorrer el alrbol
                if (status == NodeStatus.Processing)
                {
                    lockIndex = i;
                    return;
                }
            }
            lockIndex = 0;
        }

        public void AddNode(BehaviourNode node, string name, BehaviourNode parent = null)
        {
            node.SetTree(this);
            if (parent != null)
            {
				parent.AddChild(node);  //Si el nodo que agrego tiene padre, lo agrego a su lista de hijos
                nodes.Add(name, node);
            }
            else
            {
				nodes.Add(name, node);   //Si no tiene padre solo lo agrego al diccionario de nodos
                firstLine.Add(node);
            }
        }

		public void AddNode(string name, BehaviourNode node, string parentName = null)  //Sobrecarga con el nombre (string) del padre
        {
            node.SetTree(this);
            if (parentName != null && nodes.ContainsKey(parentName)) //Si el nodo que agrego tiene padre, lo agrego a su lista de hijos
            {
                nodes[parentName].AddChild(node);
                nodes.Add(name, node);
            }
            else
            {
                nodes.Add(name, node);  //Si no tiene padre solo lo agrego al diccionario de nodos
                firstLine.Add(node);
            }

        }

        public bool RemoveNode(string name)
        {
            if (nodes.ContainsKey(name))  //Busco por nombre si el nodo existe
            {
                nodes.Remove(name);
                return true;
            }
            return false;
        }

        public bool RemoveNode(BehaviourNode node) //sobrecarga para cuando mando un nodo como parametro
        {
            string key = null;
            foreach(string k in nodes.Keys) //Busco en el dicionario si se encuentra el nodo que mande a borrar
            {
                if (node == nodes[key])
                {
                    key = k;
                    break;
                }
            }

            if (key != null)  // Si el nodo existe lo borro
            {
                RemoveNode(key);
                return true;
            }
            return false;
        }

        public void HaltTree(BehaviourNode action)
        {
            actionInProgress = action;
        }
    }
}