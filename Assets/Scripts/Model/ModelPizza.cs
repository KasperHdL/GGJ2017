using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelPizza", menuName = "ObjectModel/Pizza")]
public class ModelPizza : ScriptableObject {
    [Header("Changes DO NOT apply")]
    public Ingredient.Type[] ingredientTypes;
    [Header("Changes DO apply")]
    public GameObject[] prefabIngredients;
    public Material cookedMaterial;
    public Material burntMaterial;
    public Material firedMaterial;
}
