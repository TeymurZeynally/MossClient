using System.Collections.Generic;

namespace Moss.Client
{
	internal static class MossLanguageMap
	{
		public static readonly Dictionary<MossLanguage, string> Dictionary = new()
		{
			{ MossLanguage.C, "c" },
			{ MossLanguage.Cpp, "cc" },
			{ MossLanguage.Java, "java" },
			{ MossLanguage.Ml, "ml" },
			{ MossLanguage.Pascal, "pascal" },
			{ MossLanguage.Ada, "ada" },
			{ MossLanguage.Lisp, "lisp" },
			{ MossLanguage.Scheme, "scheme" },
			{ MossLanguage.Haskell, "haskell" },
			{ MossLanguage.Fortran, "fortran" },
			{ MossLanguage.Ascii, "ascii" },
			{ MossLanguage.Vhdl, "vhdl" },
			{ MossLanguage.Perl, "perl" },
			{ MossLanguage.Matlab, "matlab" },
			{ MossLanguage.Python, "python" },
			{ MossLanguage.Mips, "mips" },
			{ MossLanguage.Prolog, "prolog" },
			{ MossLanguage.Spice, "spice" },
			{ MossLanguage.Vb, "vb" },
			{ MossLanguage.Csharp, "csharp" },
			{ MossLanguage.Modula2, "modula2" },
			{ MossLanguage.A8086, "a8086" },
			{ MossLanguage.JavaScript, "javascript" },
			{ MossLanguage.PlSQL, "plsql" },
		};
	}
}
