using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /**
    * State class For a Civilian while it is hiding.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class HidingState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * HidingState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public HidingState()
        {
            this.ID = "Hiding";
            this.parent = null;
        }
        /**
         * Method called when entering the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void OnEnter()
        {
            this.animator = this.parent.GetComponent<Animator>();
            this.parent.HideDialogue();
            this.animator.SetBool("Moving", false);
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();
            this.parent.Say(((Civilian)this.parent).scaredMessages[Random.Range(0, ((Civilian)this.parent).scaredMessages.Count)]);

            // Change until the dragon is dead
            if (this.parent.GetMessage(MessageList.MESSAGE_DRAGON_DEAD))
            {
                this.parent.ChangeState("Wandering");
            }
        }
        /**
         * MEthod called when exiting the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void OnExit()
        {
        }
        /**
         * Method called when destroying the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Destroy()
        {

        }
    }
}
