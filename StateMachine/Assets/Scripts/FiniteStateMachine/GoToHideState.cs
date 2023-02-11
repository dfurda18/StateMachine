using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * State class For a Civilian to go to a hiding spot when the Dragon arrives.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class GoToHideState : State
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
         * GoToHideState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public GoToHideState()
        {
            this.ID = "GoToHide";
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
            this.parent.HideDialogue();
            // Say a random dialogue
            this.parent.Say(((Civilian)this.parent).scaredMessages[Random.Range(0, ((Civilian)this.parent).scaredMessages.Count)]);
            this.animator.SetBool("Moving", false);
            this.firstLoop = true;

            // Get the hiding locaation.
            this.destination = ((Civilian)this.parent).hideLocations[Random.Range(0, ((Civilian)this.parent).hideLocations.Count)].position;
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();

            // Move to the destination if it is the first loop
            if (this.firstLoop)
            {
                this.animator.SetBool("Moving", true);
                this.navAgent.SetDestination(this.destination);
            }

            // Claculate the distance and change state if arrived.
            float dist = this.navAgent.remainingDistance;
            if (!this.firstLoop &&
                dist != Mathf.Infinity &&
                this.navAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                this.navAgent.remainingDistance == 0)
            {
                this.animator.SetBool("Moving", false);
                this.parent.ChangeState("Hiding");
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
