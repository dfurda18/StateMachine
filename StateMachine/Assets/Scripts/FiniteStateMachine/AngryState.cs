using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /**
    * State class for the plant when it is awake.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class AngryState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * AngryState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public AngryState()
        {
            this.ID = "Angry";
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
            this.parent.Say("Wha!? Sit mownin awready?");
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            // Calculate the distance from the hated element
            float distance = Vector3.Distance(((Flower)this.parent).hated.transform.position, this.parent.transform.position);
            this.parent.HideDialogue();
            if (distance < ((Flower)this.parent).hatedDistance)
            {
                // Face at the hated element and shout a random text
                this.parent.transform.LookAt(((Flower)this.parent).hated.transform);
                int shout = Random.Range(0, ((Flower)this.parent).shouts.Count);
                this.parent.Say(((Flower)this.parent).shouts[shout]);
            }

            // Update the tired counter and move to sleep if it's too tired
            ((Flower)this.parent).tired--;
            if (((Flower)this.parent).tired <= 0)
            {
                this.parent.ChangeState("Sleep");
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
