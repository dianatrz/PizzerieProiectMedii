using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppWeb_Proiect.Data;

namespace AppWeb_Proiect.Models
{
    public class PizzaIngredientsPageModel : PageModel
    {
        public List<AssignedIngredientData> AssignedIngredientDataList;
        public void PopulateAssignedIngredientData(AppWeb_ProiectContext context,
        Pizza pizza)
        {
            var allIngredients = context.Ingredient;
            var pizzaIngredients = new HashSet<int>(
            pizza.PizzaIngredients.Select(c => c.IngredientID)); //
            AssignedIngredientDataList = new List<AssignedIngredientData>();
            foreach (var cat in allIngredients)
            {
                AssignedIngredientDataList.Add(new AssignedIngredientData
                {
                    IngredientID = cat.ID,
                    Nume = cat.NumeIngredient,
                    Assigned = pizzaIngredients.Contains(cat.ID)
                });
            }
        }
        public void UpdatePizzaIngredients(AppWeb_ProiectContext context,
        string[] selectedIngredients, Pizza pizzaToUpdate)
        {
            if (selectedIngredients == null)
            {
                pizzaToUpdate.PizzaIngredients = new List<PizzaIngredient>();
                return;
            }
            var selectedIngredientsHS = new HashSet<string>(selectedIngredients);
            var pizzaIngredients = new HashSet<int>
            (pizzaToUpdate.PizzaIngredients.Select(c => c.Ingredient.ID));
            foreach (var cat in context.Ingredient)
            {
                if (selectedIngredientsHS.Contains(cat.ID.ToString()))
                {
                    if (!pizzaIngredients.Contains(cat.ID))
                    {
                        pizzaToUpdate.PizzaIngredients.Add(
                        new PizzaIngredient
                        {
                            PizzaID = pizzaToUpdate.ID,
                            IngredientID = cat.ID
                        });
                    }
                }
                else
                {
                    if (pizzaIngredients.Contains(cat.ID))
                    {
                        PizzaIngredient courseToRemove
                        = pizzaToUpdate
                        .PizzaIngredients
                       .SingleOrDefault(i => i.IngredientID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
