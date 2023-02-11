using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FSM;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * Dragon Entity.
    * @see Entity
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class Dragon : Entity
    {
        /**
         * The Dragon's speed.
         */
        public float speed;
        /**
         * The Dragon's health.
         */
        public int health = 20;
        /**
         * The remaiing time to attack.
         */
        public int timeToAttack = 10;
        /**
         * The Dragon's Roars.
         */
        public List<string> roars;
        /**
         * The Dragon's landing locations.
         */
        public List<Transform> landingLocations;
        /**
         * The Dragon's main enemy.
         */
        public GameObject enemy;
        /**
        * Dragon Entity.
        * @see Entity
        * @author DarioUrdapilleta
        * @since 02/10/2023
        * @version 1.0
        */
        public Dragon() : base()
        {
            this.ID = "Dragon";
            FlyingState initialState = new FlyingState();
            this.currentState = initialState;
            this.RegisterState(initialState); 
            this.RegisterState(new LandingState());
            this.RegisterState(new AttackingState());
        }
    }

}

