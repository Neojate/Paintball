using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Proyecto
{
    public enum TeamType
    {
        PLAYER, ENEMY, ALLIED
    }

    public class HumanElement : AnimatedElement
    {
        //Color del traje
        protected Color suitColor;

        //Equipo del personaje
        protected Marker marker;                //pistola
        protected List<Pod> pods;               //lista de pods

        #region CONSTRUCTORES

        public HumanElement(Camera camera) : base(camera)
        {
            pods = new List<Pod>();
        }

        #endregion



        #region METODOS VIRTUALES

        /** Metodo que calcula la trigonometria entre un elemento y su objetivo (x = a, y = b, z = h) */
        protected virtual Vector3 calculateTrigonometry()
        {
            return Vector3.Zero;
        }

        /** Método que calcula la rotación entre un elemento y su objetivo */
        protected virtual float calculateRotation()
        {
            return 0f;
        }

        #endregion



        #region METODOS PROTECTED



        #endregion



        #region GETTERS Y SETTERS

        public Color getSuitColor() { return suitColor; }
        public void setSuitColor(Color suitColor) { this.suitColor = suitColor; } 

        public Marker getMarker() { return marker; }
        public void setMarker(Marker marker) { this.marker = marker; }

        public List<Pod> getPods() { return pods; }
        public void setPods(List<Pod> pods) { this.pods = pods; }

        #endregion

    }
}
