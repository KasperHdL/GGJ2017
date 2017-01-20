using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour {

    public ModelOrder model;

    public int id;
    public int slot;
    public int tableNumber;
    public int[] ingredientCount;

    //UI References
    public Text tableNumberText;
    public Image[] ingredientImages;

	public void set(int tableNumber, int[] ingredientCount) {
        this.tableNumber = tableNumber;
        this.ingredientCount = ingredientCount;
        //go through ingredients

        int total = 0;
        for(int i = 0; i < ingredientCount.Length; i++){
            for(int j = 0; j < ingredientCount[i]; j++){
                ingredientImages[total].sprite = model.ingredientSprites[i];
                ingredientImages[total].enabled = true;

                total++;
            }
        }

        tableNumberText.text = (tableNumber + 1) + "";
	}
	
	void Update () {
		
	}
}
