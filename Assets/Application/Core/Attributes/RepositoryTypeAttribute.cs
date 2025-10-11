using System;

namespace Application.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RepositoryTypeAttribute : Attribute
    {
        public readonly Type[] Types;

        public RepositoryTypeAttribute(params Type[] types)
        {
            Types = types;
        }
    }
}