using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Assertions;

namespace FSM
{
    /**
    * State class for Fighters to find the Dragon.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class FindDragonState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * The nav agent.
         */
        public NavMeshAgent navAgent;
        /**
         * The destination.
         */
        private Vector3 destination;
        /**
         * First state's loop.
         */
        private bool firstLoop;
        /**
         * FindDragonState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public FindDragonState()
        {
            this.ID = "FindDragon";
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

            // Get the destination and get a random offset so they all sorround the dragon
            this.destination = new Vector3(((Fighter)this.parent).dragon.transform.position.x, 0.0f, ((Fighter)this.parent).dragon.transform.position.z);
            Vector3 sorroundingOffset = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            this.destination += (Vector3.Normalize(sorroundingOffset) * 2.5f);
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

            // Move in the first loop only
            if (this.firstLoop)
            {
                this.animator.SetBool("Moving", true);
                this.navAgent.SetDestination(this.destination);
            }

            // Maybe say something
            if (Random.Range(0, 2) == 1)
            {
                int shout = Random.Range(0, ((Fighter)this.parent).toBattle.Count);
                this.parent.Say(((Fighter)this.parent).toBattle[shout]);

            }

            // If it arrived to the destination, change states
            float dist = this.navAgent.remainingDistance;
            if (!this.firstLoop &&
                dist != Mathf.Infinity &&
                this.navAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                this.navAgent.remainingDistance <= 1)
            {
                this.animator.SetBool("Moving", false);
                this.parent.ChangeState("FightDragon");
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
