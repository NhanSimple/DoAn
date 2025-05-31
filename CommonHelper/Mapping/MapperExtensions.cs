using AutoMapper;
using System;
namespace CommonHelper.Mapping
{
    public class MapperExtensions
    {
        private static void IgnoreUnmappedProperties(TypeMap map, IMappingExpression expr)
        {
            foreach (string propName in map.GetUnmappedPropertyNames())
            {

            }
        }
    }
}
