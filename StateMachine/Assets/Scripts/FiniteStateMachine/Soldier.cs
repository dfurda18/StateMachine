using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FSM;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * Soldier Entity.
    * @see Fighter
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class Soldier : Fighter
    {
        /**
         * The Soldier's shouts
         */
        public List<string> shouts;
        /**
         * The current patroll destination
         */
        [HideInInspector]
        public int currentPatrollingDestination;
        /**
         * Soldier constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public Soldier() : base()
        {
            this.ID = "Soldier";
            this.RegisterState(new PatrollingStopState());
            this.RegisterState(new PatrollingState());
        }
    }

}