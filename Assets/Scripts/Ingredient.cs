using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {
    public enum Type{
        Mushroom,
        Cheese,
        Salami
    }

    public Type type;
    public ModelIngredient model;
}
