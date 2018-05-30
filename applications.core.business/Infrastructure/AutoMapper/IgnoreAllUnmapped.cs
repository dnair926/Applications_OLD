namespace Applications.Core.Business
{
    using AutoMapper;

    /// <summary>
    /// Source: http://stackoverflow.com/a/8071038
    /// </summary>
    public static class MappingExpressionExtensions
    {
        /// <summary>
        /// Helper method to set all members of object not to map.
        /// </summary>
        /// <typeparam name="TSource">Source object type</typeparam>
        /// <typeparam name="TDest">Destination object type</typeparam>
        /// <param name="expression">Expression to extend</param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }
    }
}