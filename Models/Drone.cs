using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Models
{
    public class Drone
    {
        public int id { get; set; }

        public string name { get; set; }

        public string model_id { get; set; }
        public virtual Model model { get; set; }

    }
}
