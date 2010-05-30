using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4BB.lang
{
    public sealed class MethodDefinitionComp: ICommand
    {
        public bool Identify(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            // TODO: We need to also cater for C# properties later on

            bool result = false;
            if ((aCurrentCodeLine.StartsWith("public") || aCurrentCodeLine.IndexOf("void") > -1) &&
                aSourceCode.CountTokens(aCurrentCodeLine, '(') == 1 &&
                aSourceCode.CountTokens(aCurrentCodeLine, ')') == 1 &&
                aCurrentCodeLine.IndexOf(aSourceCode.GetFileName()) == -1 &&
                aCurrentCodeLine.Split(' ').Length > 2 &&
                !MainMethodComp.IdentifyMainMethod(aSourceCode, aCurrentCodeLine, aLinePosition) &&
                (aCurrentCodeLine.EndsWith("{") || aSourceCode.GetNextLine(aLinePosition).StartsWith("{")))
                result = true;

            return result;
        }

        public CompileLineResult Compile(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            String line = aCurrentCodeLine;
            if (line.IndexOf("override") > -1)
                line = line.Replace("override", "");

            if (line.IndexOf("virtual") > -1)
                line = line.Replace("virtual", "");

            if (line.IndexOf("internal") > -1)
                line = line.Replace("internal", "protected");

            if (line.IndexOf("internal protected") > -1)
                line = line.Replace("internal protected", "protected");

            StringBuilder newLine = new StringBuilder();

            newLine.Append(line);
            if (aSourceCode.ContainProgramArgument("-throwexceptions"))
            newLine.Append(" throws Exception");
            
            return new CompileLineResult(newLine.ToString());
        }
    }
}
