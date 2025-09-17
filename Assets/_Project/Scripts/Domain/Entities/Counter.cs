using Project.Domain.Exceptions;

namespace Project.Domain.Entities
{
    public class Counter
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Value { get; private set; }
        public int Step { get; private set; }
        
        public Counter(string id, string name, int value, int step)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new DomainException("Идентификатор счетчика не может быть пустым.");
            
            Id = id;
            
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Имя счетчика не может быть пустым.");
            
            Name = name;
            
            if (value < 0)
                throw new DomainException("Значение счетчика не может быть отрицательным.");
            
            Value = value;
            
            if (step < 0)
                throw new DomainException("Шаг счетчика не может быть отрицательным.");
            
            Step = step;
        }

        public void Increment()
        {
            var newValue = Value + Step;
            
            if (newValue < 0)
                throw new DomainException("Шаг счетчика не может быть отрицательным.");

            Value = newValue;
        }
        
        public void Decrement()
        {
            var newValue = Value - Step;
            
            if (newValue < 0)
                throw new DomainException("Значение счетчика не может быть отрицательным.");
            
            Value = newValue;
        }
    }
}
