using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS4BB.Exceptions;

namespace CS4BB.lang
{
    class ClassDefinitionComp: ICommand
    {
        public bool Identify(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            bool result = false;

            if ((aCurrentCodeLine.StartsWith("public") || 
                 aCurrentCodeLine.StartsWith("class") || 
                 aCurrentCodeLine.StartsWith("protected") ||
                 aCurrentCodeLine.StartsWith("private") ||
                 aCurrentCodeLine.StartsWith("sealed") ||
                 aCurrentCodeLine.StartsWith("internal") ||
                 aCurrentCodeLine.StartsWith("iternal protected")) &&
                aCurrentCodeLine.IndexOf("class") > -1 &&
                (aCurrentCodeLine.EndsWith("{") || aSourceCode.GetNextLine(aLinePosition).StartsWith("{")))
                result = true;

            return result;
        }

        public CompileLineResult Compile(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            CompileLineResult result = new CompileLineResult(aCurrentCodeLine);

            String superClassName = GetSupperClassName(aCurrentCodeLine);
            if (superClassName != null)
            {
                if (superClassName.IndexOf(",") > -1)
                    throw new CompileErrorException(aSourceCode.GetFileName() + ": C# doesn't support multiple inheritance and interfaces aren't supported yet");

                StringBuilder newLine = new StringBuilder();
                newLine.Append(GetClassDefinition(aCurrentCodeLine)).Append(" extends ").Append(superClassName);

                result = new CompileLineResult(newLine.ToString());
            }
            return result;
        }

        private String GetClassDefinition(string aCurrentCodeLine)
        {
            String result = null;
            String[] st = aCurrentCodeLine.Split(':');
            if (st.Length > 0)
                result = st[0].Trim();

            return result;
        }

        private string GetSupperClassName(string aCurrentCodeLine)
        {
            String result = null;
            String[] st = aCurrentCodeLine.Split(':');
            if (st.Length > 0)
                result = st[1].Trim();
            
            return result;
        }
    }
}
