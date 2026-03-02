using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Extentions
{
  public static class MappingExtensions
  {
    public static TDestination MapTo<TSource, TDestination>(this TSource source)
    {
      return Mapper.Map<TSource, TDestination>(source);
    }

    public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
    {
      return Mapper.Map(source, destination);
    }
  }
}
