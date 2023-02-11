using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /**
    * State class that makes a boy play for a period of time.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class BoyPlayingState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * BoyPlayingState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public BoyPlayingState()
        {
            this.ID = "Playing";
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
            this.parent.Say("Nice! Perfect spot to play.");
            // Set a random tired value
            ((Boy)this.parent).tired = Random.Range(1, 5);
            this.animator.SetBool("Playing", true);
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();
            this.parent.Say("I'm playing!");

            // Update the tired counter and change states if 0
            ((Boy)this.parent).tired--;
            if(((Boy)this.parent).tired <= 0)
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
            this.animator.SetBool("Playing", false);
            this.parent.HideDialogue();
            this.parent.Say("I'm done playing.");
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
