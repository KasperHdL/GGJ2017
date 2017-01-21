using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    public class IngredientDispenser : MonoBehaviour {
        public GameObject prefab;

        private Hand interactingHand;
        private bool canDispense = true;

        private void OnHandHoverBegin(Hand hand)
        {
            if (interactingHand == null)
            {
                interactingHand = hand;
            }
        }

        private void HandHoverUpdate(Hand hand)
        {
            //Trigger got pressed
            if (hand.GetStandardInteractionButtonDown() && canDispense)
            {
                canDispense = false;
                Transform attachPoint = hand.transform;
                Instantiate(prefab, attachPoint.position, Quaternion.identity);
            }
        }

        private void OnHandHoverEnd(Hand hand)
        {
            if (interactingHand == hand)
            {
                canDispense = true;
            }
        }

        // Update is called once per frame
        void Update() {

        }

        void OnTriggerStay(Collider other)
        {
            if (other.tag == "GameController")
            {

            }
        }
    }
}
