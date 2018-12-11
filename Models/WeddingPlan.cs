using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
  public class WeddingPlan
    {
        [Key]
        public int PlanId {get;set;}

        [Required]
        [Display(Name="Wedder One")]
        public string WedderOne { get; set; }
        
        [Required]
        [Display(Name="Wedder Two")]
        public string WedderTwo { get; set; }
        
        [Required]
        [FutureDate]
        [Display(Name="Date")]
        public DateTime WeddingDate { get; set; }

        [Required]
        [Display(Name="Wedding Address")]
        public string WeddingAddress { get; set; }
        public int? CreatorId { get; set; }
        public List<WeddingGuest> Guests {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}