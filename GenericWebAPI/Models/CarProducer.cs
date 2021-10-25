using System;
using System.Collections.Generic;

namespace GenericWebAPI.Models
{
    public partial class CarProducer
    {
        public Guid Guid { get; set; }

        public DateTime Created = DateTime.Now;

        public string Name { get; set; }

        public DateTime Founded { get; set; }
    }

    public partial class CarProducer
    {
        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}
