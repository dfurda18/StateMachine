using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FSM;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * Civilian Entity.
    * @see Entity
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class Civilian : Entity
    {
        /**
         * The Civilian's hiding locations.
         */
        public List<Transform> hideLocations;
        /**
         * The Civilian's scared messages.
         */
        public List<string> scaredMessages;
        /**
         * The Civilian's relief message.
         */
        public string reliefMessage;
        /**
         * Civilian constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public Civilian() : base()
        {
            this.ID = "GirlInLove";

            WanderingState initialState = new WanderingState();
            this.currentState = initialState;
            this.RegisterState(initialState);
            this.RegisterState(new GoToHideState());
            this.RegisterState(new HidingState());
        }
        /**
         * What the civilian does to react to instant messages.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void ReactToMessages()
        {
            if (this.GetMessage(MessageList.MESSAGE_DRAGON_ARRIVED))
            {
                this.ChangeState("GoToHide");
            }
        }

    }
}
