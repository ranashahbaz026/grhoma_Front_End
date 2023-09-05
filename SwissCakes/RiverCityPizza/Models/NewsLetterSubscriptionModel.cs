using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SwissConfectionery.Models
{
    public class NewsLetterSubscriptionModel
    {
        public NewsLetterSubscriptionModel()
        {
            SubscriberLocations = new List<SelectListItem>();
            SubscriberLocations.Add(new SelectListItem
            {
                Text = "Otis Orchards",
                Value = "Otis Orchards"
            });
            SubscriberLocations.Add(new SelectListItem
            {
                Text = "Spokane Valley",
                Value = "Spokane Valley"
            });
            SubscriberLocations.Add(new SelectListItem
            {
                Text = "Coeur d'Alene",
                Value = "Coeur d'Alene"
            });
        }

        [Required(ErrorMessage = "Name is required")]
        public string SubscriberName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Not valid email")]
        public string SubscriberEmail { get; set; }
        public string SubscriberLocation { get; set; }
        public IList<SelectListItem> SubscriberLocations { get; set; }
        public string ReturnUrl { get; set; }
    }
}
