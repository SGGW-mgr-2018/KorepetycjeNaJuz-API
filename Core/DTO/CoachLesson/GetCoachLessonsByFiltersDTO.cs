﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class GetCoachLessonsByFiltersDTO
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public double Latitiude { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public double Longitiude { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public double Radius { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Level { get; set; }

        public int CoachId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public DateTime DateFrom { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public DateTime DateTo { get; set; }


        [JsonIgnore]
        public int StudentId { get; set; }
    }
}
