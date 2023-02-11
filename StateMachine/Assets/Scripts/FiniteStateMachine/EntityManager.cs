using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

using FSM;
using Unity.VisualScripting;

namespace FSM
{
    /**
    * Entity Manager class.
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public class EntityManager : MonoBehaviour
    {
        /**
         * The entity map.
         */
        public Dictionary<string, Entity> entityMap;
        /**
         * The message lists.
         */
        private List<Message> messageList;
        /**
         * The time for each tick.
         */
        public float tickTime = 1;
        /**
         * The last tick.
         */
        private float lastTick;
        /**
         * The subscriptions list.
         */
        private Dictionary<Subscriptions, List<string>> subscriptions;
        /**
         * Calles at the start.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public void Start()
        {
            this.entityMap = new Dictionary<string, Entity>();
            this.subscriptions = new Dictionary<Subscriptions, List<string>>();
            this.messageList = new List<Message>();
            lastTick = 0;

            // Register entities.
            foreach (Transform child in transform)
            {
                if(child.gameObject.GetComponent("Entity") as Entity != null)
                {
                    this.RegisterEntity(child.gameObject.GetComponent<Entity>());
                    foreach(Subscriptions subscription in child.GetComponent<Entity>().subscriptions)
                    {
                        this.Subscribe(subscription, child.GetComponent<Entity>().ID);
                    }
                }
            }

        }
        /**
         * Updates the entities.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public void Update()
        {
            lastTick += Time.deltaTime;
            if (lastTick > tickTime)
            {
                lastTick -= tickTime;

                // Send all messages to their subscribed members
                foreach (Message message in this.messageList)
                {
                    if(this.subscriptions.ContainsKey(message.suscription))
                    {
                        foreach (string entityId in this.subscriptions[message.suscription])
                        {
                            this.entityMap[entityId].ReceiveMessage(message);
                        }
                    }
                }
                this.messageList.Clear();

                // Update all the entities
                foreach (KeyValuePair<string, Entity> entity in entityMap)
                {
                    entity.Value.UpdateEntity();
                }
            }
 
        }
        /**
         * Subscribes an entity to listen messages.
         * @param subscription The subscription to sobscribe.
         * @param entityID The entity to subscribe.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public void Subscribe(Subscriptions subscription, string entityID)
        {
            if (!this.subscriptions.ContainsKey(subscription))
            {
                this.subscriptions[subscription] = new List<string>();
            }
            this.subscriptions[subscription].Add(entityID);
        }
        /**
         * Adds a message to the list.
         * @param message The message to add.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public void SendMessage(Message message)
        {
            messageList.Add(message);
        }
        /**
         * Register an Entity.
         * @param entity The entity.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public void RegisterEntity(Entity entity)
        {
            entity.parent = this;
            entityMap[entity.ID] = entity;
        }
        /**
         * Kills an Entity.
         * @param ID The entity ID.
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public void KillEntity(string ID)
        {
            Assert.IsTrue(entityMap.ContainsKey(ID));

            entityMap[ID].Destroy();
            entityMap.Remove(ID);
        }
        /**
         * Destroys the Entity Manager
         * @author Dario Urdapilleta
         * @since 02/10/2023
         */
        public void Destroy()
        {
            messageList.Clear();

            // Destroy the entities.
            foreach (KeyValuePair<string, Entity> entity in entityMap)
            {
                entity.Value.Destroy();
            }
            entityMap.Clear();
        }
    }
}
