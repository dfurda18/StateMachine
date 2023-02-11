using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

using FSM;
using System.Linq;

namespace FSM
{
    /**
    * Entity class.
    * @author DarioUrdapilleta
    * @since 02/10/2023
    * @version 1.0
    */
    public abstract class Entity : MonoBehaviour
    {
        /**
         * The Entity Manager.
         */
        public EntityManager parent = null;
        /**
         * The current state.
         */
        public State currentState = null;
        /**
         * The Previous state.
         */
        public State previousSate = null;
        /**
         * The Entity's ID.
         */
        public string ID;
        /**
         * The State list.
         */
        public Dictionary<string, State> stateMap;
        /**
         * The wandering locations.
         */
        public List<Transform> wanderLocations;
        /**
         * The State to change to after wandering.
         */
        public string stateAfterWandering;
        /**
         * The CMEssages list.
         */
        [HideInInspector]
        public List<Message> messageList;
        /**
         * The Entity's subscriptions.
         */
        public List<Subscriptions> subscriptions;
        /**
         * The Dialogue object.
         */
        public GameObject dialogue;
        /**
         * The Dialogue's text.
         */
        public TextMeshPro text;
        /**
         * The point to look at.
         */
        public Transform lookAt;
        /**
         * The Amount to rotate.
         */
        private float amountToRotate;
        /**
         * The times to rotate.
         */
        private int timesToRotate;
        /**
         * The current rotation.
         */
        private int currentRotation;
        /**
        * Entity Entity.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public Entity()
        {
            this.stateMap = new Dictionary<string, State>();
            this.messageList = new List<Message>();
            this.subscriptions = new List<Subscriptions>();
        }
        /**
        * Logic excecuted at the start.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void Start()
        {
            currentState.OnEnter();
        }
        /**
        * Changes the entity's state
        * @param stateID The state's ID
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void ChangeState(string stateID)
        {
            // Exit the state
            previousSate = currentState;
            previousSate.OnExit();

            Assert.IsTrue(stateMap.ContainsKey(stateID));

            // Enter the new state
            currentState = stateMap[stateID];
            currentState.OnEnter();
        }
        /**
        * Registers a state to this entity
        * @param state The state to register
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void RegisterState(State state)
        {
            state.parent = this;
            stateMap[state.ID] = state;
        }
        /**
        * Changes to a blip state
        * @param state The state to blip to
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void ChangeToBlipState(string stateID)
        {
            previousSate = currentState;

            Assert.IsTrue(stateMap.ContainsKey(stateID));

            // enter the new state
            currentState = stateMap[stateID];
            currentState.OnEnter();
        }
        /**
        * Instant update.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void Update()
        {
            this.dialogue.transform.LookAt(lookAt);
            this.ReactToMessages();
        }
        /**
        * Fixed update
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void FixedUpdate()
        {
            // Do the rotations
            if (this.currentRotation < this.timesToRotate)
            {
                this.currentRotation++;
                this.transform.Rotate(0, this.amountToRotate / this.timesToRotate, 0);
            }
        }
        /**
        * Returns to the previous state.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void ReturnToPreviousState()
        {
            State temp = currentState;
            currentState.OnExit();
            currentState = previousSate;
            previousSate = temp;
        }
        /**
        * Update the Entity's state
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void UpdateEntity()
        {
            this.HideDialogue();
            currentState.Update();
        }
        /**
        * Get's a particular message and consumes it.
        * @param message The message to fetch
        * @return Tru if the message was found false, otherwise.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public bool GetMessage(MessageList message)
        {
            foreach(Message m in this.messageList)
            {
                if(m.message == message)
                {
                    this.messageList.Remove(m);
                    return true;
                }
            }
            return false;
        }
        /**
        * Sends a message to the EntityManager.
        * @param message The message to send.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void SendMessage(Message message)
        {
            parent.SendMessage(message);
        }
        /**
        * Adds a message to the message list.
        * @param message The message to add.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void ReceiveMessage(Message message)
        {
            messageList.Add(message);
        }
        /**
        * Destroys the entity.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void Destroy()
        {
            messageList.Clear();

            foreach (KeyValuePair<string, State> state in stateMap)
            {
                state.Value.Destroy();
            }
            stateMap.Clear();
        }
        /**
        * Says a comment.
        * @param dialogue The dialogue to say.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void Say(string dialogue)
        {
            this.text.SetText(dialogue);
            this.dialogue.SetActive(true);
        }
        /**
        * Hides a dialogue
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void HideDialogue()
        {
            this.text.SetText("");
            this.dialogue.SetActive(false);
        }
        /**
        * Rotates the entity given an amount and time.
        * @param amount The angle to rotate.
        * @param rotationTime The seconds to take to rotate.
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public void Rotate(float amount, float rotationTime)
        {
            this.currentRotation = 0;

            // Calculate the times needed to complete the rotation
            this.timesToRotate = (int)(rotationTime / Time.fixedDeltaTime);
            this.amountToRotate = amount;
        }
        /**
        * React to messages
        * @author DarioUrdapilleta
        * @since 02/10/2023
        */
        public virtual void ReactToMessages()
        {

        }
    }
}
