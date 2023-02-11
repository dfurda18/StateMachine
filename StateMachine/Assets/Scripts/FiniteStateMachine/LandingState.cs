using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * State class for a Dragon to land into the town.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class LandingState : State
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
         * LandingState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public LandingState()
        {
            this.ID = "Landing";
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

            // Get a random landing point and fly there
            int location = Random.Range(0, ((Dragon)this.parent).landingLocations.Count);
            this.destination = ((Dragon)this.parent).landingLocations[location].position;
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

            // Maybe roar
            if (Random.Range(0, 2) == 1)
            {
                this.parent.Say("Rooooaaarrrr!!!!");
            }

            // Change state if it arrived there
            if (Vector3.Distance(this.destination, this.parent.transform.position) <= 0.1)
            {
                this.parent.messageList.Clear();
                this.parent.ChangeState("Attacking");
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
            this.parent.SendMessage(new Message(this.parent.ID, Subscriptions.TOWN, MessageList.MESSAGE_DRAGON_ARRIVED));
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
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        private void FlyTo(Vector3 destination)
        {
            // Claculate the distance to set the correct speed, to move and look at the destination
            float distance = Vector3.Distance(this.parent.transform.position, destination);
            this.parent.transform.DOMove(destination, distance / ((Dragon)this.parent).speed);
            this.parent.transform.LookAt(new Vector3(destination.x, this.parent.transform.position.y, destination.z));
        }
    }
}
