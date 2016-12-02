using System;
using System.Text;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	/// <summary>
	/// Base class for different types of contracts. Provides common properties.
	/// </summary>
	public abstract class MemberInfo
	{
		/// <summary>
		/// Gets the name of a type system member.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Indicates if the type system member is deprecated.
		/// </summary>
		public bool IsDeprecated { get; }

		/// <summary>
		/// Gets the text message describing the reason for the contract being marked as obsolete (optional).
		/// </summary>
		public string ReasonDeprecated { get; }

		/// <summary>
		/// Initializes new instance of<see cref="MemberInfo"/> with supplied values.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="isDeprecated">Indicates if the type system member is deprecated.</param>
		/// <param name="reasonDeprecated">text message describing the reason for the contract being marked as obsolete (optional).</param>
		protected MemberInfo(string name, bool isDeprecated, string reasonDeprecated)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));

			Name = name;
			IsDeprecated = isDeprecated;
			ReasonDeprecated = reasonDeprecated;
		}

		public override string ToString()
		{
			var builder = new StringBuilder($"Name:{Name}");
			if (IsDeprecated)
			{
				builder.Append("; Deprecated");
				if (!string.IsNullOrEmpty(ReasonDeprecated))
					builder.Append($"({ReasonDeprecated})");
			}
			return builder.ToString();
		}
	}
}
