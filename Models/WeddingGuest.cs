using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
  public class WeddingGuest
    {
        [Key]
        public int GuestId {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public int PlanId {get; set;}
        public WeddingPlan WeddingPlan {get; set;}
    }
}