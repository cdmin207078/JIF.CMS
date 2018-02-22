using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.CMS.Test.Core.Entities;
using System.Collections.Generic;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class TypeTest
    {
        [TestMethod]
        public void Type_Prop_Test()
        {
            ShowTypeProperties(typeof(int));
            ShowTypeProperties(typeof(string));
            ShowTypeProperties(typeof(decimal));
            ShowTypeProperties(typeof(long));
            ShowTypeProperties(typeof(double));
            ShowTypeProperties(typeof(DateTime));
            ShowTypeProperties(typeof(List<string>));
            ShowTypeProperties(typeof(string[]));
            ShowTypeProperties(typeof(Product));
        }

        private void ShowTypeProperties(Type type)
        {
            Console.WriteLine("------------------------- {0} -------------------------", type.Name);

            Console.WriteLine("type.IsAbstract : {0}", type.IsAbstract);
            Console.WriteLine("type.IsAnsiClass : {0}", type.IsAnsiClass);
            Console.WriteLine("type.IsArray : {0}", type.IsArray);
            Console.WriteLine("type.IsAutoClass : {0}", type.IsAutoClass);
            Console.WriteLine("type.IsAutoLayout : {0}", type.IsAutoLayout);
            Console.WriteLine("type.IsByRef : {0}", type.IsByRef);
            Console.WriteLine("type.IsClass : {0}", type.IsClass);
            Console.WriteLine("type.IsCOMObject  : {0}", type.IsCOMObject);
            Console.WriteLine("type.IsConstructedGenericType : {0}", type.IsConstructedGenericType);
            Console.WriteLine("type.IsContextful : {0}", type.IsContextful);
            Console.WriteLine("type.IsEnum : {0}", type.IsEnum);
            Console.WriteLine("type.IsExplicitLayout : {0}", type.IsExplicitLayout);
            Console.WriteLine("type.IsGenericParameter : {0}", type.IsGenericParameter);
            Console.WriteLine("type.IsGenericType : {0}", type.IsGenericType);
            Console.WriteLine("type.IsGenericTypeDefinition : {0}", type.IsGenericTypeDefinition);
            Console.WriteLine("type.IsImport : {0}", type.IsImport);
            Console.WriteLine("type.IsInterface  : {0}", type.IsInterface);
            Console.WriteLine("type.IsLayoutSequential : {0}", type.IsLayoutSequential);
            Console.WriteLine("type.IsMarshalByRef : {0}", type.IsMarshalByRef);
            Console.WriteLine("type.IsNested : {0}", type.IsNested);
            Console.WriteLine("type.IsNestedAssembly : {0}", type.IsNestedAssembly);
            Console.WriteLine("type.IsNestedFamANDAssem  : {0}", type.IsNestedFamANDAssem);
            Console.WriteLine("type.IsNestedFamily : {0}", type.IsNestedFamily);
            Console.WriteLine("type.IsNestedFamORAssem : {0}", type.IsNestedFamORAssem);
            Console.WriteLine("type.IsNestedPrivate  : {0}", type.IsNestedPrivate);
            Console.WriteLine("type.IsNestedPublic : {0}", type.IsNestedPublic);
            Console.WriteLine("type.IsNotPublic : {0}", type.IsNotPublic);
            Console.WriteLine("type.IsPointer : {0}", type.IsPointer);
            Console.WriteLine("type.IsPrimitive  : {0}", type.IsPrimitive);
            Console.WriteLine("type.IsPublic : {0}", type.IsPublic);
            Console.WriteLine("type.IsSealed : {0}", type.IsSealed);
            Console.WriteLine("type.IsSecurityCritical : {0}", type.IsSecurityCritical);
            Console.WriteLine("type.IsSecuritySafeCritical : {0}", type.IsSecuritySafeCritical);
            Console.WriteLine("type.IsSecurityTransparent : {0}", type.IsSecurityTransparent);
            Console.WriteLine("type.IsSerializable : {0}", type.IsSerializable);
            Console.WriteLine("type.IsSpecialName : {0}", type.IsSpecialName);
            Console.WriteLine("type.IsUnicodeClass : {0}", type.IsUnicodeClass);
            Console.WriteLine("type.IsValueType : {0}", type.IsValueType);
            Console.WriteLine("type.IsVisible : {0}", type.IsVisible);
        }

    }
}
