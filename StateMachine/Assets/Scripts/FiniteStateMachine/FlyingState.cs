using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;

namespace FSM
{
    /**
    * State class for the Dragon to fly around.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class FlyingState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * The destination.
         */
        private Vector3 destination;
        /**
         * FlyingState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public FlyingState()
        {
            this.ID = "Flying";
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

            // Claculate a random amount of destinations before attacking
            ((Dragon)this.parent).timeToAttack = Random.Range(2, 5);

            // Set the first destination and fly there.
            int location = Random.Range(0, this.parent.wanderLocations.Count);
            this.destination = this.parent.wanderLocations[location].position;
            this.FlyTo(this.destination);

        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();

            // Check if it arrived to the destination
            if (Vector3.Distance(this.destination, this.parent.transform.position) <= 0.1)
            {
                // Update the counter and either change state or fly to a different destination
                ((Dragon)this.parent).timeToAttack--;
                if (((Dragon)this.parent).timeToAttack <= 0)
                {
                    this.parent.ChangeState("Landing");
                } else
                {
                    int location = Random.Range(0, this.parent.wanderLocations.Count);
                    this.destination = this.parent.wanderLocations[location].position;
                    this.FlyTo(this.destination);
                }
            }
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
        /**
         * Fly to a destination.
         * @param destination The Vector3 with the final destination.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        private void FlyTo(Vector3 destination)
        {
            // Look and move torwards the destination
            float distance = Vector3.Distance(this.parent.transform.position, destination);
            this.parent.transform.DOMove(destination, distance / ((Dragon)this.parent).speed);
            this.parent.transform.LookAt(new Vector3(destination.x, this.parent.transform.position.y, destination.z));
        }
    }
}
