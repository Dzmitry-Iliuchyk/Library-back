namespace Library.Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        protected Entity(Guid Id)
        {
            this.Id = Id;
        }

    }
}
