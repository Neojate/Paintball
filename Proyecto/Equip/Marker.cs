using Microsoft.Xna.Framework;
using System;

namespace Proyecto
{
    //Tipo de disparo
    public enum ShootMode
    {
        SEMI, AUTO, DOUBLE_TRIGGER
    }

    //Marcadoras
    public enum MarkerModel
    {
        SPYDER_VICTOR, TIPPMANN_98, TIPPMANN_A5, EMPIRE_AXE, ECLIPSE_CSR, RAP4_T68
    }

    public class Marker : BaseEquip
    {
        //constantes
        private const int ACCU_RANGE = 500;

        //atributos
        private MarkerModel model;
        private float accuracy;             //precisión del arma a 1000 metros
        private float fireRate;             //frecuencia de disparo: segundo / bolas
        private ShootMode shootType;        //tipo de disparo
        private Vector2 loader;             //cargador del arma

        //fisicas
        private double shootTime;
        private bool canShoot;

        //destino
        Vector2 destiny;



        #region CONSTRUCTORES

        public Marker(MarkerModel model)
        {
            this.model = model;
            language = new Language();

            //carga del modelo
            loadModel(model);

            //atributos
            shootTime = 0;
            canShoot = false;
        }

        #endregion



        #region METODOS PUBLICOS

        public void handleInput(Camera camera, Vector2 elementPos, Vector2 elementOffset, Vector2 elementSize, Vector2 destiny)
        {
            switch(shootType)
            {
                case ShootMode.SEMI:
                    if (Input.mouseClickPressed(TypeButton.LEFT_BUTTON) && canShoot) shoot(camera, elementPos, elementOffset, elementSize, accuracy, destiny);
                    break;
                case ShootMode.DOUBLE_TRIGGER:
                    if (Input.mouseClickDown(TypeButton.LEFT_BUTTON) && canShoot) shoot(camera, elementPos, elementOffset, elementSize, accuracy, destiny); //cambiar
                    break;
                case ShootMode.AUTO:
                    if (Input.mouseClickDown(TypeButton.LEFT_BUTTON) && canShoot) shoot(camera, elementPos, elementOffset, elementSize, accuracy, destiny);
                    break;
            }
        }

        public void update()
        {
            shootTime += Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            if (shootTime > 1000 / fireRate)
            {
                canShoot = true;
                shootTime = 0;
            }
        }

        /** Método que gasta una bala */
        public void waistAmmo()
        {
            loader.X -= 1;
            if (loader.X <= 0) loader.X = 0;
        }

        /** Método que recarga el loader */
        public void loadMarker(int charge)
        {
            loader.X += charge;
            if (loader.X > loader.Y) loader.X = loader.Y;
        }

        #endregion



        #region METODOS PRIVADOS

        /** Metodo para disparar */
        private void shoot(Camera camera, Vector2 elementPos, Vector2 elementOffset, Vector2 elementSize, float accuracy, Vector2 destiny)
        {
            if (getLoader().X > 0)
            {
                canShoot = false;
                GameScreen.map.auxElements.Add(new Shoot(camera, elementPos, elementOffset, elementSize, accuracy, ShootFrom.ALLIED, destiny));
                waistAmmo();
            }
        }

        /** Método que calcula la dispersión del arma */
        private Vector2 calculateTrinometryReason()
        {
            float h = (float)Math.Sqrt(ACCU_RANGE * ACCU_RANGE + accuracy * accuracy);
            return new Vector2(accuracy / h, ACCU_RANGE / h);
        }

        /** Método que carga las características de cada marcadora */
        private void loadModel(MarkerModel model)
        {            
            switch(model)
            {
                case MarkerModel.SPYDER_VICTOR:
                    name = "Spyder Victor";
                    description = language.getMessage("spyderVictor_description");
                    price = "65";
                    texture = Textures.markerSpyderVictor;
                    shootType = ShootMode.SEMI;
                    accuracy = 180f;
                    fireRate = 5f;
                    loader = new Vector2(150, 150);
                    break;

                case MarkerModel.TIPPMANN_98:
                    name = "Tippmann 98";
                    description = language.getMessage("tippmann98_description");
                    price = "140";
                    texture = Textures.markerTippmann98;
                    shootType = ShootMode.SEMI;
                    accuracy = 140f;
                    fireRate = 8f;
                    loader = new Vector2(200, 200);
                    break;

                case MarkerModel.TIPPMANN_A5:
                    name = "Tippmann A5 AK Model";
                    description = language.getMessage("tippmannA5_description");
                    price = "289";
                    texture = Textures.markerTippmannA5;
                    shootType = ShootMode.AUTO;
                    accuracy = 110f;
                    fireRate = 14f;
                    loader = new Vector2(50, 50);
                    break;

                case MarkerModel.EMPIRE_AXE:
                    name = "Empire Axe 2.0";
                    description = language.getMessage("empireAxe_description");
                    price = "499";
                    texture = Textures.markerEmpireAxe;
                    shootType = ShootMode.DOUBLE_TRIGGER;
                    accuracy = 75f;
                    fireRate = 20f;
                    loader = new Vector2(200, 200);
                    break;

                case MarkerModel.ECLIPSE_CSR:
                    name = "Eclipse Geo CSR";
                    description = language.getMessage("eclipseGeo_description");
                    price = "2095";
                    texture = Textures.markerEclipseCSR;
                    shootType = ShootMode.DOUBLE_TRIGGER;
                    accuracy = 40f;
                    fireRate = 24f;
                    loader = new Vector2(200, 200);
                    break;

                case MarkerModel.RAP4_T68:
                    name = "Rap4 T68";
                    description = language.getMessage("rap4_description");
                    price = "699";
                    texture = Textures.markerT68;
                    shootType = ShootMode.SEMI;
                    accuracy = 10f;
                    fireRate = 3f;
                    loader = new Vector2(20, 20);
                    break;
            }
        }

        #endregion



        #region GETTERS Y SETTERS

        public MarkerModel getModel() { return model; }

        public float getAccuracy() { return accuracy; }
        public void setAccuracy(float accuracy) { this.accuracy = accuracy; }

        public float getFireRate() { return fireRate; }
        public void setFireTate(float fireRate) { this.fireRate = fireRate; }

        public ShootMode getShootType() { return shootType; }
        public void setShootType(ShootMode shootType) { this.shootType = shootType; }

        public Vector2 getLoader() { return loader; }
        public void setLoader(Vector2 loader) { this.loader = loader; }

        #endregion

    }
}
