using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /**
    * State class for a Soldier to patrol through a path.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class PatrollingStopState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * The cuurent speech.
         */
        private int speechCounter;
        /**
         * The amount of times the state will update.
         */
        private int speechLength;
        /**
         * PatrollingStopState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public PatrollingStopState()
        {
            this.ID = "PatrollingStop";
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
            this.speechCounter = 0;
            this.speechLength = 5;
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();

            // Say something on the third iteration
            int currentSpeech = Random.Range(0, ((Soldier)this.parent).shouts.Count);
            if (this.speechCounter == 2)
            {
                this.parent.Say(((Soldier)this.parent).shouts[currentSpeech]);

            }
            this.speechCounter++;

            // Rotate 90 degrees in on 10th of a second
            this.parent.Rotate(90, 0.1f);
            
            // Check if it patrolled enough and change state
            if (this.speechCounter >= this.speechLength)
            {
                this.parent.ChangeState("Patrolling");
            }

            // Maybe the Dragon arrived
            this.parent.ReactToMessages();
        }
        /**
         * MEthod called when exiting the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void OnExit()
        {
            this.parent.HideDialogue();
            this.parent.Say("ADVANCING!");
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
