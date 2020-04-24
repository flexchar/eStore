using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Models
{
    public class Model
    {
        public int id { get; set; }

        [Display(Name = "Series Name")]
        public string name { get; set; }

        [ForeignKey("model_id")]
        public IEnumerable<Drone> drones { get; set; }


    }
}
