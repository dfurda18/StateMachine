using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /**
    * State class a girl to pray at the church.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class GirlPrayingState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * The amount of times to pray.
         */
        private int prayCounter;
        /**
         * GirlPrayingState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public GirlPrayingState()
        {
            this.ID = "Praying";
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
            this.parent.Say("Enthuzimuzzy. I made it on time.");
            this.prayCounter = 0;
            this.animator.SetBool("Praying", true);
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();

            // Say the next dialogue, update the coutner and change states
            this.parent.Say(((Girl)this.parent).prayers[this.prayCounter]);
            this.prayCounter++;
            if (this.prayCounter >= ((Girl)this.parent).prayers.Count)
            {
                ((Girl)this.parent).remainingTimeToPray = ((Girl)this.parent).nextTimeToPray;
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
            this.animator.SetBool("Praying", false);
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
