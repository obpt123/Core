﻿using System.Collections.Generic;
namespace System
{
    public static class AppDomainExtentions
    {
        public static IEnumerable<Type> FindInstanceTypesByAttribute<AttributeType>(this AppDomain appDomain, Func<Type, bool> filter = null)
         where AttributeType : Attribute
        {
            var customFilter = filter ?? (type => true);

            foreach (var assembly in appDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass
                        && !type.IsAbstract
                        && Attribute.IsDefined(type, typeof(AttributeType))
                        && customFilter(type))
                    {
                        yield return type;
                    }
                }
            }
        }

        public static IEnumerable<Type> FindInstanceTypesByBaseType<BaseType>(this AppDomain appDomain, Func<Type, bool> filter = null)
        {
            var customFilter = filter ?? (type => true);
            foreach (var assembly in appDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass
                        && !type.IsAbstract
                        && typeof(BaseType).IsAssignableFrom(type)
                        && customFilter(type))
                    {
                        yield return type;
                    }
                }
            }
        }
        public static IEnumerable<Type> FindInstanceTypesByAttributeAndBaseType<AttributeType, BaseType>(this AppDomain appDomain, Func<Type, bool> filter = null)
           where AttributeType : Attribute
        {
            var customFilter = filter ?? (type => true);
            foreach (var assembly in appDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass
                        && !type.IsAbstract
                        && Attribute.IsDefined(type, typeof(AttributeType))
                        && typeof(BaseType).IsAssignableFrom(type)
                        && customFilter(type))
                    {
                        yield return type;
                    }
                }
            }
        }
    }
}
