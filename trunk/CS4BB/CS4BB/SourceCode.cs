using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CS4BB
{
    public class SourceCode
    {
        private FileInfo sourceFile;
        private List<String> sourceCode = new List<String>();

        public SourceCode(FileInfo aSourceFile)
        {
            this.sourceFile = aSourceFile;
            ReadSourceCode();
        }

        /// <summary>
        /// Indicate if the file does contain source code
        /// </summary>
        /// <returns></returns>
        public bool DoesHaveCode()
        {
            return this.sourceCode != null && this.sourceCode.Count > 0;
        }

        /// <summary>
        /// Get the java destination file name
        /// </summary>
        /// <returns></returns>
        public string GetJavaDestinationFileName()
        {
            return this.sourceFile.Name.Replace(".cs", ".java");
        }

        /// <summary>
        /// Get the java destination file name full name
        /// </summary>
        /// <returns></returns>
        public string GetJavaDestinationFullName()
        {
            return this.sourceFile.FullName.Replace(".cs", ".java");
        }

        /// <summary>
        /// Get the source file name
        /// </summary>
        /// <returns></returns>
        public String GetFileName()
        {
            return this.sourceFile.Name;
        }

        /// <summary>
        /// Return all the source code lines
        /// </summary>
        /// <returns></returns>
        public List<String> GetLines()
        {
            return this.sourceCode;
        }

        /// <summary>
        /// Return the next item in the source code line
        /// </summary>
        /// <param name="aLinePosition"></param>
        /// <returns></returns>
        public String GetNextLine(int aLinePosition)
        {
            //int nextLinePos = aLinePosition;
            //nextLinePos++;
            return GetCode(this.sourceCode.ElementAtOrDefault<String>(aLinePosition));
        }

        private void ReadSourceCode()
        {
            StreamReader file = null;
            try
            {
                file = new StreamReader(sourceFile.FullName);
                String line = null;
                while ((line = file.ReadLine()) != null)
                    if (ContainCode(line))
                        sourceCode.Add(GetCode(line));
                    else
                        sourceCode.Add(" ");
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }

        private string GetCode(String aCodeLine)
        {
            return ContainCode(aCodeLine) ? aCodeLine.Trim() : "";
        }

        private bool ContainCode(string aCodeLine)
        {
            return aCodeLine != null && aCodeLine.Length > 0;
        }

        public String GetPreviousLine(String aCodeLine, int aLinePosition)
        {
            String result = "";
            String currentCode = "";

            int prevLinePos = aLinePosition;
            while (currentCode.CompareTo(aCodeLine) == 0 || !ContainCode(currentCode))
            {
                prevLinePos--;
                currentCode = GetCode(this.sourceCode.ElementAtOrDefault<String>(prevLinePos));
            }

            result = currentCode;
            return result;
        }
    }
}
