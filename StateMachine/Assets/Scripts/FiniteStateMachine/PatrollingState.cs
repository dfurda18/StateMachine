using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    /**
    * State class a Soldier while in a patrolling spot.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class PatrollingState : State
    {
        /**
         * The navigator agent.
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
         * PatrollingState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public PatrollingState()
        {
            this.ID = "Patrolling";
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

            // Go to the next patrolling destination
            ((Soldier)this.parent).currentPatrollingDestination++;
            int location = ((Soldier)this.parent).currentPatrollingDestination;
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
            // MOve to the desitnation if it is the first loop
            if (this.firstLoop)
            {
                this.animator.SetBool("Moving", true);
                this.navAgent.SetDestination(this.destination);
            }

            // Check the distance and change state if it arrived there
            float dist = this.navAgent.remainingDistance;
            if (!this.firstLoop &&
                dist != Mathf.Infinity &&
                this.navAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                this.navAgent.remainingDistance == 0)
            {
                this.parent.HideDialogue();
                this.animator.SetBool("Moving", false);
                this.parent.ChangeState(this.parent.stateAfterWandering);
            }
            this.firstLoop = false;

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
