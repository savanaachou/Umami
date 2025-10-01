using UnityEngine;
using UnityEngine.UI;

public class CookingGame : MonoBehaviour
{
    public GameObject recipeBookScreen;   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recipeBookScreen.SetActive(false);
    }

    public void ShowRecipeBook()
    {
        recipeBookScreen.SetActive(true);
    }
    
    public void HideRecipeBook()
    {
        recipeBookScreen.SetActive(false);
    }
    
}
