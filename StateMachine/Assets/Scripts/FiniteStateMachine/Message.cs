using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /**
    * Class that contains a message information.
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public struct Message
    {
        /**
         * The sender ID
         */
        public string senderID { get; set; }
        /**
         * The subscription
         */
        public Subscriptions suscription { get; set; }
        /**
         * The message.
         */
        public MessageList message;
        /**
         * Message constructor.
         * @param sender The message sender.
         * @param suscription The suscription.
         * @param theMessage The message.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public Message(string sender, Subscriptions suscription, MessageList theMessage)
        {
            this.senderID = sender;
            this.suscription = suscription;
            this.message = theMessage;
        }
    }
}
