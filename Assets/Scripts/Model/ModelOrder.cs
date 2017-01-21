using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelOrder", menuName = "ObjectModel/Order")]
public class ModelOrder : ScriptableObject {

    [Header("Changes do NOT apply")]
    public Ingredient.Type[] ingredientTypes;
    [Header("Changes DO apply")]
    public Sprite[] ingredientSprites;
}
