using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    public class IngredientDispenser : MonoBehaviour {
        public GameObject prefab;
        public List<GameObject> objects;
        public List<int> freeObjects;

        public void Start()
        {
            objects = new List<GameObject>();
            freeObjects = new List<int>();
        }

        private void HandHoverUpdate(Hand hand)
        {
            //Trigger is pressed
            if (hand.GetStandardInteractionButton())
            {
                Transform attachPoint = hand.transform;
                GameObject g;
                if(freeObjects.Count == 0)
                    g = Instantiate(prefab, hand.GetAttachmentTransform().position, Quaternion.identity);
                else
                {
                    g = objects[freeObjects[0]];
                    freeObjects.RemoveAt(0);
                    g.GetComponent<Collider>().enabled = true;
                    g.GetComponent<Rigidbody>().isKinematic = false;
                    g.transform.position = hand.GetAttachmentTransform().position;

                }

                Ingredient i = g.GetComponent<Ingredient>();
                i.renderer.enabled = true;
                
                i.dispenser = this;
                i.dispenserIndex = objects.Count;
                Throwable throwIng = g.GetComponent<Throwable>();

                hand.AttachObject(g, throwIng.attachmentFlags, throwIng.attachmentPoint);
                objects.Add(g);
            }
        }

        public void recycleMe(int index)
        {
            freeObjects.Add(index);
        }
    }
}
