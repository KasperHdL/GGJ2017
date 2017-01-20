using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour {

    public ModelOrder model;

    public int id;
    public int slot;
    public int tableNumber;
    public int[] ingredients;

    //UI References
    public Text tableNumberText;
    public Image[] ingredientImages;

	public void set(int tableNumber, int[] ingredients) {
        this.tableNumber = tableNumber;
        this.ingredients = ingredients;
        //go through ingredients

        for(int i = 0; i < 9; i++){
            if(i < ingredients.Length)
                ingredientImages[i].sprite = model.ingredientSprites[ingredients[i]];
            else
                ingredientImages[i].enabled = false;
        }
        tableNumberText.text = (tableNumber + 1) + "";
	}
	
	void Update () {
		
	}
}
