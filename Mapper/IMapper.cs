namespace Mapper
{
    public interface IMapper
    {
        T2 MapValues<T1, T2>(T1 objectWithNewValues, T2 objectToUpdate);
    }
}