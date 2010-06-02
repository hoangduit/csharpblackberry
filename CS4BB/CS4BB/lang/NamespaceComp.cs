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

        public TargetCodeResult Compile(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            TargetCodeResult result = new TargetCodeResult(aCurrentCodeLine);
            
            //if (!aSourceCode.ContainProgramArgument("-nopackage"))
            //{
            //    String namespaceName = GetNamespaceName(aSourceCode, aCurrentCodeLine);
            //    StringBuilder newLine = new StringBuilder();
            //    newLine.Append("package ").Append(namespaceName).Append(";");
            //    result = new TargetCodeResult(newLine.ToString());
            //}
            //else
                result = new TargetCodeResult("");

            return result;
        }

        private string GetNamespaceName(SourceCode aSourceCode, string aCurrentCodeLine)
        {
            return aSourceCode.ReplaceKeyword(aCurrentCodeLine, "namespace", "").Replace(";", "").Replace("{", "").Trim();
        }
    }
}
