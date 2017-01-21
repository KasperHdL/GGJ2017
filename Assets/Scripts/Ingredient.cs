using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Ingredient : MonoBehaviour
{
    public enum Type
    {
        Mushroom,
        Cheese,
        Salami
    }

    public Type type;
    public ModelIngredient model;
    public Renderer renderer;
    private Valve.VR.InteractionSystem.Hand hand = null;
    public Valve.VR.InteractionSystem.IngredientDispenser dispenser;
    public int dispenserIndex;

    public void destroy()
    {
        renderer.enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        dispenser.recycleMe(dispenserIndex);
    }

    private void HandHoverUpdate(Valve.VR.InteractionSystem.Hand hand)
    {
        //Trigger got pressed
        if (hand.GetStandardInteractionButtonDown())
        {
            this.hand = hand;

        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Pizza")
        {
            if (hand != null) hand.DetachObject(gameObject);
            //Destroy(gameObject, 0.5f);
            destroy();
        }
    }
}
