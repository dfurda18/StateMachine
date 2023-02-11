using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /**
    * The abstract class for states.
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public abstract class State
    {
        /**
         * The state's parent.
         */
        public Entity parent = null;
        /**
         * The state's ID.
         */
        public string ID = "";
        /**
         * Entering the state
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public abstract void OnEnter();
        /**
         * Updateing the states.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public abstract void Update();
        /**
         * The exits logic.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public abstract void OnExit();
        /**
         * Destroy the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public abstract void Destroy();
    }
}
