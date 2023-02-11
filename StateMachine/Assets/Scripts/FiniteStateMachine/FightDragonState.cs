using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * State class for Fighters to fight the Dragon.
    * @see State
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class FightDragonState : State
    {
        /**
         * The parent's animator.
         */
        public Animator animator;
        /**
         * This Fighter's damage message.
         */
        public MessageList hit;
        /**
         * FightDragonState constructor.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public FightDragonState()
        {
            this.ID = "FightDragon";
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
            this.parent.Say("For the King!!!!");

            // Look at the dragon
            this.parent.transform.LookAt(((Fighter)this.parent).dragon.transform.position);

            // Set the hit message depending on the type
            if(this.parent.GetType() == typeof(Hero))
            {
                this.hit = MessageList.HERO_HIT_DRAGON;
            } else
            {
                this.hit = MessageList.SQUIRE_HIT_DRAGON;
            }
        }
        /**
         * Method called when updating the state.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public override void Update()
        {
            this.animator.SetBool("Attacking", false);
            this.parent.HideDialogue();

            // Look at the dragon
            this.parent.transform.LookAt(((Fighter)this.parent).dragon.transform.position);
            this.animator.SetBool("Attacking", true);

            // Maybe say something
            if (Random.Range(0, 2) == 1)
            {
                this.parent.Say(((Fighter)this.parent).whileFighting[Random.Range(0, ((Fighter)this.parent).whileFighting.Count)]);
            }
            // Hit
            this.parent.SendMessage(new Message(this.parent.ID, Subscriptions.DRAGON_FIGHT, this.hit));

            // Change the state if the dragon is dead
            if (this.parent.GetMessage(MessageList.MESSAGE_DRAGON_DEAD))
            {
                if(this.parent.GetType() == typeof(Hero))
                {
                    // The hero will notify the dragon the battle is over
                    this.parent.SendMessage(new Message(this.parent.ID, Subscriptions.DRAGON_FIGHT, MessageList.CELEBRATION));
                }
                this.parent.ChangeState("Celebration");
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
