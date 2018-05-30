namespace Applications.Core.Business
{
    using AutoMapper;

    /// <summary>
    /// Interface for objects that has mapping
    /// </summary>
    public interface IMap
    {
        void CreateMap(Profile profile);
    }
}