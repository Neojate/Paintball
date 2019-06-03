using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{

    public enum IAType
    {
        SCOUT_LEFT, SCOUT_RIGHT, COVER, MARAUDER
    }

    public class IAMoral
    {

        //node padre
        public static Node fatherNode;

        //tipo de moral
        private IAType iaType;

        //posicion inicial
        private Vector2 initialPos;

        //velocidad del personaje
        private float elementSpeed;
        
        //ruta de la IA
        private List<Node> nodes;
        private Node destinyNode;
        private Node originalNode;

        //conducta
        private int percentToCover;

        private int indexNode;
        private bool repeatNodes;
        private bool firstStep;

        //disparos de la IA
        private int shootRange;

        

        public IAMoral(IAType iaType, Vector2 initialPos)
        {
            this.iaType = iaType;
            this.initialPos = initialPos;

            createNodes();

            indexNode = 0;

            elementSpeed = 3f;

            firstStep = false;

            percentToCover = 90;

        }

        public void incrementNodeIndex()
        {
            indexNode++;
            if (indexNode >= nodes.Count)
            {
                indexNode = nodes.Count - 1;
                if (repeatNodes) indexNode = 0;
            }
        }

        public bool isFinalNode()
        {
            return indexNode == nodes.Count - 1;
        }

        public void calculateNextNode(Player player)
        {
            Random rnd = new Random();

            if (!firstStep)
            {
                destinyNode = nodes[rnd.Next(0, 2)];
                fatherNode = destinyNode;
                firstStep = true;
                return;
            }

            rnd = new Random();
            if (rnd.Next(0, 101) < percentToCover)
            {
                calculateHedge(player);
                moveOnHedge(player);
                return;
            }

            destinyNode.findAvaliableNodes();
            destinyNode = nodes[destinyNode.getAvaliableNodes()[rnd.Next(0, destinyNode.getAvaliableNodes().Length)]];
            fatherNode = destinyNode;

            calculateHedge(player);

            //moveOnHedge(player);
            //destinyNode = destinyNode.getAvaliableNodes()[rnd.Next(0, destinyNode.getAvaliableNodes().Count)];
            


            //destinyNode = destinyNode.getAvaliableNodes()[rnd.Next(0, destinyNode.getAvaliableNodes().Count)];
        }

        public bool checkSite(Vector2 currentPosition)
        {
            return currentPosition == destinyNode.getPosition();
        }



        #region MÉTODOS PRIVADOS
        
        private void createNodes()
        {
            nodes = new List<Node>();
            nodes.Add(new Node(new Vector2(7, 4)));     //0
            nodes.Add(new Node(new Vector2(15, 4)));    //1
            nodes.Add(new Node(new Vector2(3, 6)));     //2
            nodes.Add(new Node(new Vector2(11, 6)));    //3
            nodes.Add(new Node(new Vector2(19, 6)));    //4
            nodes.Add(new Node(new Vector2(7, 9)));     //5
            nodes.Add(new Node(new Vector2(15, 9)));    //6
            nodes.Add(new Node(new Vector2(3, 12)));    //7
            nodes.Add(new Node(new Vector2(11, 12)));   //8
            nodes.Add(new Node(new Vector2(18, 12)));   //9
        }

        private void calculateHedge(Player player)
        {

            if (destinyNode.getPosition().Y <= player.getElementPos().Y)
            {
                if (destinyNode.getPosition().X < player.getElementPos().X)
                {
                    //destinyNode = destinyNode.getNeighbours()[0];
                    destinyNode = new Node(new Vector2(destinyNode.getPosition().X - 1, destinyNode.getPosition().Y - 1));
                }
                else destinyNode = new Node(new Vector2(destinyNode.getPosition().X + 1, destinyNode.getPosition().Y - 1));
            }
            else
            {
                if (destinyNode.getPosition().X < player.getElementPos().X)
                {
                    destinyNode = new Node(new Vector2(destinyNode.getPosition().X - 1, destinyNode.getPosition().Y + 1));
                }
                else destinyNode = new Node(new Vector2(destinyNode.getPosition().X + 1, destinyNode.getPosition().Y + 1));
            }

        }

        private void moveOnHedge(Player player)
        {
            if (destinyNode.getPosition().Y <= player.getElementPos().Y)
            {
                if (destinyNode.getPosition().X < fatherNode.getPosition().X)
                {
                    destinyNode = new Node(new Vector2(fatherNode.getPosition().X + 1, fatherNode.getPosition().Y - 1));
                }
                else destinyNode = new Node(new Vector2(fatherNode.getPosition().X - 1, fatherNode.getPosition().Y - 1));
            }
            else
            {
                if (destinyNode.getPosition().X < fatherNode.getPosition().X)
                {
                    destinyNode = new Node(new Vector2(fatherNode.getPosition().X + 1, fatherNode.getPosition().Y + 1));
                }
                else destinyNode = new Node(new Vector2(fatherNode.getPosition().X - 1, fatherNode.getPosition().Y + 1));
            }
        }

        #endregion



        #region GETTERS Y SETTERS

        public float getElementSpeed() { return elementSpeed; }

        public Vector2 getNextPosition() { return destinyNode.getPosition(); }

        public int getIndexNode() { return indexNode; }

        public Node getDestinyNode() { return destinyNode; }
        public void setDestinyNode(Node destinyNode) { this.destinyNode = destinyNode; }

        #endregion



    }
}
