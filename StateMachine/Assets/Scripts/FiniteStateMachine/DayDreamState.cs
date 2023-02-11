using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /**
    * State class For a Girl to day dream on.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class DayDreamState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * The transition counter.
         */
        private int dayDreamCounter;
        /**
         * The length of the state.
         */
        private int dayDreamLength;
        /**
         * DayDreamState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public DayDreamState()
        {
            this.ID = "DayDream";
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
            this.parent.Say("Alas! What a beautiful spot.");
            this.dayDreamCounter = 0;

            // Set the length of the state
            this.dayDreamLength = Random.Range(2, 5);
            this.animator.SetBool("DayDream", true);
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();
            this.parent.Say(((Girl)this.parent).thoughts[this.dayDreamCounter]);
            
            // Update the counter and change state if done
            this.dayDreamCounter++;
            if (this.dayDreamCounter >= this.dayDreamLength)
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
            this.animator.SetBool("DayDream", false);
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
