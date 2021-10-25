using System;

namespace GenericWebAPI.Models
{
    public partial class Car
    {
        public Guid Guid { get; set; }

        public string Series { get; set; }

        public CarType Type { get; set; } = CarType.Undefined;

        public DateTime Created = DateTime.Now;

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public int Power { get; set; }

        public Nullable<Guid> ProducerGuid { get; set; }
    }

    public partial class Car
    {
        public CarProducer Producer { get; set; }
    }
}
