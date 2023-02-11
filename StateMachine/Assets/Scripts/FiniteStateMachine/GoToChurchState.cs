using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    /**
    * State class a Girl to go to church.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class GoToChurchState : State
    {
        /**
         * The navigation agent.
         */
        public NavMeshAgent navAgent;
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * The destination.
         */
        private Vector3 destination;
        /**
         * Is this the first loop?.
         */
        private bool firstLoop;
        /**
         * GoToChurchState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public GoToChurchState()
        {
            this.ID = "GoToChurch";
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
            this.navAgent = this.parent.GetComponent<NavMeshAgent>();
            this.animator.SetBool("Moving", false);

            // Get the next destination
            this.destination = ((Girl)this.parent).churchLocation.position;
            this.parent.HideDialogue();
            this.parent.Say("Oh my! Mass will start soon!");
            this.firstLoop = true;
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();
            // Move if it is the first loop
            if (this.firstLoop)
            {
                this.animator.SetBool("Moving", true);
                this.navAgent.SetDestination(this.destination);
            }

            // Check the distance and change states
            float dist = this.navAgent.remainingDistance;
            if (!this.firstLoop && 
                dist != Mathf.Infinity &&
                this.navAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                this.navAgent.remainingDistance == 0)
            {
                this.animator.SetBool("Moving", false);
                this.parent.ChangeState("Praying");
            }
            this.firstLoop = false;
        }
        /**
         * MEthod called when exiting the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void OnExit()
        {
            this.parent.HideDialogue();
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
