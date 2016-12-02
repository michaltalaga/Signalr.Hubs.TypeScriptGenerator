namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class MethodInfo : MemberInfo
	{
		public string Arguments { get; }

		public string ReturnType { get; }

		/// <summary>
		/// Initializes new instance of<see cref="MethodInfo"/> with supplied values.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="isDeprecated"></param>
		/// <param name="reasonDeprecated"></param>
		/// <param name="arguments"></param>
		/// <param name="returnType"></param>
		public MethodInfo(
			string name, bool isDeprecated, string reasonDeprecated, string arguments, string returnType = null)
			: base(name, isDeprecated, reasonDeprecated)
		{
			Arguments = arguments;
			ReturnType = returnType;
		}

		public override string ToString()
		{
			return string.Concat(base.ToString(), $"; ReturnType:{ReturnType}; Arguments:[{Arguments}]");
		}
	}
}