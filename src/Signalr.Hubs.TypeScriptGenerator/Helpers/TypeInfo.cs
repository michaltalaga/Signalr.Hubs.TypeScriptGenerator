using System;
using System.Linq;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Helpers
{
	internal abstract class TypeInfo
	{
		public abstract TypeKind Kind { get; }

		public abstract bool IsUnion { get; }

		public abstract string Name { get; }

		protected string FormatNestedName()
		{
			return IsUnion ? $"({Name})" : Name;
		}

		protected TypeInfo()
		{
		}

		private sealed class TypeInfoPredefined : TypeInfo
		{
			public override TypeKind Kind => TypeKind.Null;
			public override bool IsUnion => false;
			public override string Name { get; }

			public TypeInfoPredefined(string name)
			{
				Name = name;
			}
		}

		public static readonly TypeInfo Null = new TypeInfoPredefined("null");

		public static readonly TypeInfo Void = new TypeInfoPredefined("void");

		public static readonly TypeInfo Date = new TypeInfoPredefined("Date");

		public static readonly TypeInfo Number = new TypeInfoPredefined("number");

		public static readonly TypeInfo Boolean = new TypeInfoPredefined("boolean");

		public static readonly TypeInfo String = new TypeInfoPredefined("string");

		public static readonly TypeInfo Any = new TypeInfoPredefined("any");

		public static TypeInfo Simple(string name)
		{
			return new TypeInfoSimple(name);
		}

		public static TypeInfo Array(TypeInfo elementType)
		{
			if (elementType == null)
				throw new ArgumentNullException(nameof(elementType));
			return new TypeInfoArray(elementType);
		}

		public static TypeInfo Union(params TypeInfo[] items)
		{
			if (items == null)
				throw new ArgumentNullException(nameof(items));
			if (items.Length <= 1)
				throw new ArgumentException("Number of items must be bigger than one.", nameof(items));
			return new TypeInfoUnion(items);
		}

		private sealed class TypeInfoSimple : TypeInfo
		{
			public override TypeKind Kind => TypeKind.Custom;

			public override bool IsUnion => false;

			public override string Name { get; }

			public TypeInfoSimple(string name)
			{
				Name = name;
			}
		}

		private sealed class TypeInfoArray : TypeInfo
		{
			private readonly TypeInfo elementType;

			public override TypeKind Kind => TypeKind.Array;

			public override bool IsUnion => false;

			public override string Name
			{
				get { return elementType.FormatNestedName() + "[]"; }
			}
			public TypeInfoArray(TypeInfo elementType)
			{
				this.elementType = elementType;
			}
		}

		private sealed class TypeInfoUnion : TypeInfo
		{
			private readonly TypeInfo[] items;

			public override TypeKind Kind => TypeKind.Custom;

			public override bool IsUnion => true;

			public override string Name
			{
				get
				{
					return string.Join(" | ", items.Select(item => item.IsUnion ? $"({item.Name})" : item.Name));
				}
			}

			public TypeInfoUnion(TypeInfo[] items)
			{
				this.items = items;
			}
		}
	}
}