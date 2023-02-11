using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FSM;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * Fighter Entity. It launches to fight the Dragon
    * @see Entity
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class Fighter : Entity
    {
        /**
         * The comments to say while running to battle.
         */
        public List<string> toBattle;
        /**
         * The comments to say while fighting.
         */
        public List<string> whileFighting;
        /**
         * The Celebration comment.
         */
        public string celebration;
        /**
         * The dragon to fight.
         */
        public GameObject dragon;
        /**
         * Fighter constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public Fighter() : base()
        {
            this.ID = "Fighter";

            WanderingState initialState = new WanderingState();
            this.currentState = initialState;
            this.RegisterState(initialState);
            this.RegisterState(new FindDragonState());
            this.RegisterState(new FightDragonState());
            this.RegisterState(new CelebtationState());
        }
        /**
         * Reacts to the dragon arriving.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void ReactToMessages()
        {
            if (this.GetMessage(MessageList.MESSAGE_DRAGON_ARRIVED))
            {
                this.ChangeState("FindDragon");
            }
        }
    }

}

