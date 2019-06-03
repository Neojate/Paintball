using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class Node
    {
        //Posición
        private Vector2 position;

        //Nodos disponibles
        private int[] avaliableNodes;

        public Node(Vector2 position)
        {
            this.position = position;
        }



        #region MÉTODOS PÚBLICOS

        /** Determina si es el mismo nodo */
        public bool isSameNode(Vector2 position)
        {
            return this.position == position;
        }

        #endregion



        #region MÉTODOS PRIVADOS

        /** Busca los nodos disponibles en función del nodo actual */
        public void findAvaliableNodes()
        {
            Vector2 position = IAMoral.fatherNode.getPosition();

            //primera generacion
            if (position == new Vector2(7, 4)) avaliableNodes = new int[] { 2, 3 };                                     //0     hecho
            else if (position == new Vector2(15, 4)) avaliableNodes = new int[] { 3, 4 };                               //1     hecho
            else if (position == new Vector2(3, 6)) avaliableNodes = new int[] { 0, 3, 5, 7 };                          //2     hecho 
            else if (position == new Vector2(11, 6)) avaliableNodes = new int[] { 0, 1, 2, 4, 5, 6 };                   //3     hecho
            else if (position == new Vector2(19, 6)) avaliableNodes = new int[] { 1, 3, 6, 9 };                         //4     hecho
            else if (position == new Vector2(7, 9)) avaliableNodes = new int[] { 2, 3, 7, 8 };                          //5     hecho
            else if (position == new Vector2(15, 9)) avaliableNodes = new int[] { 3, 4, 8, 9 };                         //6     hecho
            else if (position == new Vector2(3, 12)) avaliableNodes = new int[] { 2, 5 };                               //7     medias
            else if (position == new Vector2(11, 12)) avaliableNodes = new int[] { 5, 6 };                              //8     medias
            else if (position == new Vector2(18, 12)) avaliableNodes = new int[] { 4, 6 };                              //9     medias

        }

        #endregion



        #region GETTERS Y SETTERS

        public Vector2 getPosition() { return position; }

        public int[] getAvaliableNodes() { return avaliableNodes; }

        #endregion

    }
}
