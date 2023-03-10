using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;
    public Image customCursor;
    public Slot[] craftingSlots;
    public List<Item> itemList;
    private string[] recipes;
    public Item[] recipeResult;
    public Slot resultSlot;
    public TMP_Text resultText;

    void Start()
    {
        recipes = new string[recipeResult.Length];

        for (int i = 0; i < recipeResult.Length; i++)
        {
            string recipeResultName = recipeResult[i].gameObject.name;
            recipes[i] = recipeResultName;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                Slot nearestSlot = null;
                float nearestDistance = float.MaxValue;

                foreach (Slot slot in craftingSlots)
                {
                    float distance = Vector3.Distance(slot.transform.position, Input.mousePosition);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestSlot = slot;
                    }

                }

                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;

                itemList[nearestSlot.index] = currentItem;

                currentItem = null;

                CheckForCreatedRecipe();
            }
        }
    }

    public void OnMouseDownItem(Item item)
    {
        if (currentItem == null)
        {
            currentItem = item;
            customCursor.sprite = item.GetComponent<Image>().sprite;
            
            StartCoroutine(ActivateCursor());
        }
    }

    IEnumerator ActivateCursor()
    {
        yield return new WaitForEndOfFrame();
        customCursor.gameObject.SetActive(true);
    }

    void CheckForCreatedRecipe()
    {
        resultSlot.gameObject.SetActive(true);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach (Item item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResult[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResult[i];

                resultText.text = recipeResult[i].itemDescription;
            }
        }
    }

    public void OnClickSlot(Slot slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipe();
    }

}
