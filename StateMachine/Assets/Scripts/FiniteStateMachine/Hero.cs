using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FSM;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * Hero Entity.
    * @see Fighter
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class Hero : Fighter
    {
        /**
         * The list of public speeches.
         */
        public List<string> speeches;
        /**
         * Hero constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public Hero() : base()
        {
            this.ID = "Hero";
            this.RegisterState(new MotivationalScpeechState());
        }
    }

}

