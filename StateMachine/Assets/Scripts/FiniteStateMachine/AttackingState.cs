using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;

namespace FSM
{
    /**
    * State class that makes a dragon attack until the health is gone.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class AttackingState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * AttackingState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public AttackingState()
        {
            this.ID = "Attacking";
            this.parent = null;
        }
        /**
         * Method called when entering the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void OnEnter()
        {
            this.parent.Say("Grrrrraaaaaggghhhh!!!!");
            this.parent.transform.LookAt(((Dragon)this.parent).enemy.transform.position);
            ((Dragon)this.parent).health = 20;
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            // Look at the enemy
            this.parent.HideDialogue();
            this.parent.transform.LookAt(((Dragon)this.parent).enemy.transform.position);

            // Maybe Growl
            if (Random.Range(0, 2) == 1)
            {
                this.parent.Say(((Dragon)this.parent).roars[Random.Range(0, ((Dragon)this.parent).roars.Count)]);
            }
            // Receive Damage
            if(this.parent.GetMessage(MessageList.HERO_HIT_DRAGON))
            {
                ((Dragon)this.parent).health -= 3;
            }
            if (this.parent.GetMessage(MessageList.SQUIRE_HIT_DRAGON))
            {
                ((Dragon)this.parent).health -= 1;
            }
            // Check if dead and let everyone know
            if (((Dragon)this.parent).health <= 0)
            {
                this.parent.SendMessage(new Message(this.parent.ID, Subscriptions.TOWN, MessageList.MESSAGE_DRAGON_DEAD));
                ((Dragon)this.parent).health = 20;
            }
            // If the rest is celebrating, ruun away
            if (this.parent.GetMessage(MessageList.CELEBRATION))
            {
                this.parent.ChangeState("Flying");
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
    }
}
