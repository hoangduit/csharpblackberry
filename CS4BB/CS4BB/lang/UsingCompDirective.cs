using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace CS4BB.lang
{
    public sealed class UsingDirectiveComp: ICommand
    {
        public bool Identify(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            bool result = false;
            if (aCurrentCodeLine.StartsWith("using") && aCurrentCodeLine.IndexOf("{") == -1 && !aSourceCode.GetNextLine(aLinePosition).StartsWith("{"))
                result = true;

            return result;
        }

        public CompileLineResult Compile(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            CompileLineResult result = new CompileLineResult(aCurrentCodeLine);
            String usingNamespace = GetUsingNamespace(aCurrentCodeLine);

            bool correctLine = IsUsingCorrect(usingNamespace);

            if (!correctLine && (IsSystemNamespace(usingNamespace) || IsProgramNamespace(aSourceCode, usingNamespace)))
                return new CompileLineResult("");
            
            if (correctLine)
            {
                // TODO: For now we import all the Java classes in that package, we don't worry much about that since the Java compiler will take care of this for us
                // Suggest that we do change this in the future if required
                StringBuilder newLine = new StringBuilder();
                newLine.Append("import ").Append(usingNamespace).Append(".*;"); 
                result = new CompileLineResult(newLine.ToString());
            }
            else
            {
                StringBuilder newLine = new StringBuilder();
                newLine.Append("//");
                newLine.Append(aCurrentCodeLine);
                newLine.Append("  // Not supported yet");
                result = new CompileLineResult(newLine.ToString());
                result.LogError(aSourceCode.GetFileName() + ": Using directive not supported yet on line: " + aLinePosition);
            }
            return result;
        }

        private bool IsSystemNamespace(string aUsingNamespace)
        {
            bool result = false;
            if (aUsingNamespace.StartsWith("System") && aUsingNamespace.IndexOf(".") == -1)
                result = true;
            
            return result;
        }

        private bool IsProgramNamespace(SourceCode aSourceCode, string aUsingNamespace)
        {
            var found = (from code in aSourceCode.GetLines()
                         where code.StartsWith("namespace") && code.IndexOf(aUsingNamespace) > -1
                         select code).FirstOrDefault();
            
            return found != null;
        }

        private bool IsUsingCorrect(string aUsingNamespace)
        {
            bool result = true;
            StreamReader str = new StreamReader("xml/usingDirectives.xml");
            try
            {
                XmlSerializer xSerializer = new XmlSerializer(typeof(UsingDirectives));
                UsingDirectives usings = (UsingDirectives) xSerializer.Deserialize(str);
                if (usings != null)
                {
                    var found = (from u in usings.directive
                                 where aUsingNamespace.IndexOf(u.Name) > -1
                                 select u).FirstOrDefault();
                    
                    if (found == null)
                        result = false;
                }
            }
            finally
            {
                if (str != null)
                    str.Close();
            }
            return result;
        }

        private static String GetUsingNamespace(string aCurrentCodeLine)
        {
            String usingNamespace = aCurrentCodeLine;
            usingNamespace = usingNamespace.Replace("using", "").Trim();
            usingNamespace = usingNamespace.Replace(";", "").Trim();
            return usingNamespace;
        }
    }
}
