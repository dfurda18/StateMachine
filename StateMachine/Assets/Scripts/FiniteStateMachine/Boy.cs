using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FSM;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * Playing boy entity that runs arround and playes.
    * @see Civilian
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class Boy : Civilian
    {
        /**
         * How tired the boy is.
         */
        [HideInInspector]
        public int tired = 0;
        /**
         * Boy constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public Boy() : base()
        {
            this.ID = "PlayingBoy";

            this.RegisterState(new BoyPlayingState());
        }

    }
}
