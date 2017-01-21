using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ingredient : MonoBehaviour {
    public enum Type{
        Mushroom,
        Cheese,
        Salami
    }

    public Type type;
    public ModelIngredient model;
    public Renderer renderer;

    
}
