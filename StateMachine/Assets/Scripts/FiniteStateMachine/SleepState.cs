using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /**
    * State class for a plant to stay sleeping.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class SleepState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * SleepState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public SleepState()
        {
            this.ID = "Sleep";
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
            this.parent.Say("I'ma tired.");
            this.animator.SetBool("Sleeping", true);
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();

            // maybe say ZZZZ
            if(Random.Range(0, 2) == 1)
            {
                this.parent.Say("Zzzzzzz");
            }
            
            // Update counter and change state
            ((Flower)this.parent).tired++;
            if (((Flower)this.parent).tired > 5)
            {
                this.parent.ChangeState("Angry");
            }
        }
        /**
         * MEthod called when exiting the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void OnExit()
        {
            this.animator.SetBool("Sleeping", false);
            this.parent.HideDialogue();
            this.parent.Say("Whooooaaah!!.");
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
