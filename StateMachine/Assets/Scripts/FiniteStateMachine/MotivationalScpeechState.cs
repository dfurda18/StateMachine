using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FSM
{
    /**
    * State class For a Hero to speak and motivate.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class MotivationalScpeechState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * The current counter.
         */
        private int speechCounter;
        /**
         * The amount of dialogues said in this state.
         */
        private int speechLength;
        /**
         * The current speech.
         */
        private int currentSpeech;
        /**
         * MotivationalScpeechState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public MotivationalScpeechState()
        {
            this.ID = "MotivationalSpeech";
            this.parent = null;
            this.currentSpeech = 0;
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
            this.parent.Say("EVERYONE! GATHER! I HAVE IMPORTANT NEWS!");
            this.speechCounter = 0;

            // Get a random length
            this.speechLength = Random.Range(2, 6);
            this.animator.SetBool("Speaking", true);
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();

            // Say the current speech and update the coutners
            this.parent.Say(((Hero)this.parent).speeches[this.currentSpeech]);
            this.currentSpeech++;
            this.speechCounter++;

            // Rotate 90 degrees in half a second
            this.parent.Rotate(90, 0.5f);

            // Go back to the frist speech if it went over
            if (this.currentSpeech >= ((Hero)this.parent).speeches.Count)
            {
                this.currentSpeech = 0;
            }

            // Check the amount of times and change state
            if(this.speechCounter >= this.speechLength)
            {
                this.parent.ChangeState("Wandering");
            }

            // Maybe the dragon arrived
            this.parent.ReactToMessages();
        }
        /**
         * MEthod called when exiting the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void OnExit()
        {
            this.animator.SetBool("Speaking", false);
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
