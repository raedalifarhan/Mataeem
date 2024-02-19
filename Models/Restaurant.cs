﻿using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models
{
    public class Restaurant : BaseEntity
    {

        [Required]
        public string RestaurantName { get; set; } = default!;

        public double? LocationLatitude { get; set; }

        public double? LocationLongitude { get; set; }

        public string? City { get; set; } = default!;

        public string? District { get; set; }

        public string? Address { get; set; } = default!;

        public string? ContactNumber { get; set; }
        
        public string? PhoneNumber { get; set; }

        public string? Website { get; set; }

        public bool IsActive { get; set; } = true;


        // nav properties
        public Menu? Menu {  get; set; }
        public Guid? MenuId { get; set; }

        public IList<BusinessHours>? OpeningHours { get; set; }


        // auditing properties
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public AppUser? CreatedBy { get; set; }
        public string? CreatedById { get; set; }

        public AppUser? UpdatedBy { get; set; }
        public string? UpdatedById { get; set; }
    }
}