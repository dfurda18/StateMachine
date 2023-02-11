using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FSM;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * Flower Entity.
    * @see Entity
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class Flower : Entity
    {
        /**
         * The list of shouts.
         */
        public List<string> shouts;
        /**
         * The hated element.
         */
        public GameObject hated;
        /**
         * The distance needed for the hated element to be shouted at.
         */
        public float hatedDistance;
        /**
         * How tired the flower is.
         */
        [HideInInspector]
        public int tired = 0;
        /**
         * Flower constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public Flower() : base()
        {
            this.ID = "Flower";

            SleepState initialState = new SleepState();
            this.currentState = initialState;
            this.RegisterState(initialState);
            this.RegisterState(new AngryState());
        }
    }

}

