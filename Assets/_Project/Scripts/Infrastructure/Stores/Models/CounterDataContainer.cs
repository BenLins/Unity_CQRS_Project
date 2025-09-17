using System;

namespace Project.Infrastructure.Stores.Models
{
    [Serializable]
    public class CounterDataContainer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Step { get; set; }
    }
}
