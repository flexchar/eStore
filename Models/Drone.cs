using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Models
{
    public class Drone
    {
        public int id { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Price, EUR")]
        public float price { get; set; }

        [DisplayName("Year")]
        public int year { get; set; }

        public int series_id { get; set; }

        [DisplayName("Drone Series")]
        public virtual Series series { get; set; }

    }
}
