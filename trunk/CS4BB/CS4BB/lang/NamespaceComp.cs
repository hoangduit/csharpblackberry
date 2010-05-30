using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4BB.lang
{
    public sealed class NamespaceComp: ICommand
    {
        public bool Identify(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            bool result = false;
            
            if (aCurrentCodeLine.StartsWith("namespace") && (aCurrentCodeLine.EndsWith("{") || aSourceCode.GetNextLine(aLinePosition).StartsWith("{")))
                result = true;
            
            return result;
        }

        public CompileLineResult Compile(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            CompileLineResult result = new CompileLineResult(aCurrentCodeLine);
            
            if (!aSourceCode.ContainProgramArgument("-nopackage"))
            {
                String namespaceName = GetNamespaceName(aCurrentCodeLine);
                StringBuilder newLine = new StringBuilder();
                newLine.Append("package ").Append(namespaceName).Append(";");
                result = new CompileLineResult(newLine.ToString());
            }
            else
                result = new CompileLineResult("");

            return result;
        }

        private string GetNamespaceName(string aCurrentCodeLine)
        {
            return aCurrentCodeLine.Replace("namespace", "").Replace(";", "").Trim();
        }
    }
}
