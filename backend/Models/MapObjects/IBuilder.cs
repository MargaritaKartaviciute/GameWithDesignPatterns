namespace backend.Models
{
    public interface IBuilder<T>
    {
        T Build();
    }
}