using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * State class that For the Fighters to celebrate.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class CelebtationState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * The final message.
         */
        public MessageList hit;
        /**
         * CelebtationState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public CelebtationState()
        {
            this.ID = "Celebration";
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
            this.parent.Say(((Fighter)this.parent).celebration);
            this.parent.transform.LookAt(((Fighter)this.parent).dragon.transform.position);
            // Stop attacking
            this.animator.SetBool("Attacking", false);
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.parent.HideDialogue();
            this.parent.transform.LookAt(((Fighter)this.parent).dragon.transform.position);

            // Say something and move on
            this.parent.SendMessage(new Message(this.parent.ID, Subscriptions.DRAGON_FIGHT, MessageList.CELEBRATION));

            this.parent.ChangeState("Wandering");
        }
        /**
         * MEthod called when exiting the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void OnExit()
        {
            this.parent.HideDialogue();
            this.animator.SetBool("Attacking", false);
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
