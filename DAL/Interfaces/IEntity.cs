namespace DAL.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
