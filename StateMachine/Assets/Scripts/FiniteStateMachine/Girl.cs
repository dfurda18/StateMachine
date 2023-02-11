using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FSM;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * Daydreaming girl Entity.
    * @see Civilian
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class Girl : Civilian
    {
        /**
         * Next time to pray.
         */
        public int nextTimeToPray = 20;
        /**
         * The church location.
         */
        public Transform churchLocation;
        /**
         *The list of prayers.
         */
        public List<string> prayers;
        /**
         * Girl's toughts.
         */
        public List<string> thoughts;
        /**
         * The remaining time to pray.
         */
        [HideInInspector]
        public int remainingTimeToPray = 0;
        /**
         * Girl constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public Girl() : base()
        {
            this.ID = "GirlInLove";

            this.RegisterState(new GirlPrayingState());
            this.RegisterState(new DayDreamState());
        }

    }
}
