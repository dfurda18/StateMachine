using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    /**
    * State class for an entity to wander.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class WanderingState : State
    {
        /**
         * The navigarot agent.
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
         * Is this the first loop?
         */
        private bool firstLoop;
        /**
         * WanderingState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public WanderingState()
        {
            this.ID = "Wandering";
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

            // Get a ranodm location
            int location = Random.Range(0, this.parent.wanderLocations.Count);
            this.animator.SetBool("Moving", false);
            this.destination = this.parent.wanderLocations[location].position;
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

            // Move to the location is it is the first loop
            if (this.firstLoop)
            {
                this.animator.SetBool("Moving", true);
                this.navAgent.SetDestination(this.destination);
            }

            // Calculate the distance and change state
            float dist = this.navAgent.remainingDistance;
            if (!this.firstLoop &&
                dist != Mathf.Infinity &&
                this.navAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                this.navAgent.remainingDistance == 0)
            {
                this.animator.SetBool("Moving", false);
                this.parent.ChangeState(this.parent.stateAfterWandering);
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
