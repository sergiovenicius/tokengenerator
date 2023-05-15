namespace common.Mapper
{
    public interface IMapper<TDestination, TSource>
    {
        TDestination Map(TSource source);
    }
}
