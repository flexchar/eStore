using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Models
{
    public class Series
    {
        [Key]
        public int id { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("Drones")]
        [ForeignKey("series_id")]
        public IEnumerable<Drone> drones { get; set; }


    }
}
