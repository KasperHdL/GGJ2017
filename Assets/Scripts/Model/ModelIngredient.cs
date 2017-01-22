using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelIngredient", menuName = "ObjectModel/Ingredient")]
public class ModelIngredient : ScriptableObject {
    [Header("Changes DO apply")]
    public Material cookedMaterial;
    public Material burntMaterial;
    public Material firedMaterial;
}

