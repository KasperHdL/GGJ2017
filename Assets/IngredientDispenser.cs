using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    public class IngredientDispenser : MonoBehaviour {
        public GameObject prefab;

        private void HandHoverUpdate(Hand hand)
        {
            //Trigger got pressed
            if (hand.GetStandardInteractionButtonDown())
            {
                Transform attachPoint = hand.transform;
                GameObject ingredient = Instantiate(prefab, hand.GetAttachmentTransform().position, Quaternion.identity);
                Throwable throwIng = ingredient.GetComponent<Throwable>();

                hand.AttachObject(ingredient, throwIng.attachmentFlags, throwIng.attachmentPoint);
            }
        }
    }
}
