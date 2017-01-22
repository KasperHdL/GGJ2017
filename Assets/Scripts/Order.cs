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
    public int difficulty;

    //UI References
    public Text tableNumberText;
    public Image[] ingredientImages;
    public Image doneImage;
    public GameObject failed;

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

    public void setDone(){
        doneImage.enabled = true;
        for(int i = 0; i < ingredientCount.Length; i++){
            ingredientImages[i].enabled = false;
        }
    }
    public void setFailed(){
        failed.SetActive(true);
        for(int i = 0; i < ingredientCount.Length; i++){
            ingredientImages[i].enabled = false;
        }
    }
	
	void Update () {
		
	}
}
