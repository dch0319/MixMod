using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

namespace MixMod
{
	public static class MonoExtensions
	{
		public static MethodDefinition GetMethod(this TypeDefinition self, string name)
		{
			return self.Methods.Where((MethodDefinition m) => m.Name == name).First();
		}

		public static FieldDefinition GetField(this TypeDefinition self, string name)
		{
			return self.Fields.Where((FieldDefinition f) => f.Name == name).First();
		}

		public static TypeDefinition ToDefinition(this Type self)
		{
			return (TypeDefinition)ModuleDefinition.ReadModule(new MemoryStream(File.ReadAllBytes(self.Module.FullyQualifiedName))).LookupToken(self.MetadataToken);
		}

		public static MethodDefinition ToDefinition(this MethodBase method)
		{
			return (MethodDefinition)method.DeclaringType.ToDefinition().Module.LookupToken(method.MetadataToken);
		}

		public static FieldDefinition ToDefinition(this FieldInfo field)
		{
			return (FieldDefinition)field.DeclaringType.ToDefinition().Module.LookupToken(field.MetadataToken);
		}

		public static MethodReference MakeGenericMethod(this MethodReference self, params TypeReference[] arguments)
		{
			if (self.GenericParameters.Count != arguments.Length)
			{
				throw new ArgumentException();
			}
			GenericInstanceMethod genericInstanceMethod = new GenericInstanceMethod(self);
			foreach (TypeReference item in arguments)
			{
				genericInstanceMethod.GenericArguments.Add(item);
			}
			return genericInstanceMethod;
		}
	}
}
