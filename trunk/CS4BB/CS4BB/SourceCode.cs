﻿using System;
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
        private Dictionary<string, string> arguments;

        /// <summary>
        /// Create a new source code from C# file
        /// </summary>
        /// <param name="aSourceFile"></param>
        /// <param name="aArguments"></param>
        public SourceCode(FileInfo aSourceFile, string[] aArguments)
        {
            this.sourceFile = aSourceFile;
            this.arguments = ResolveArguments(aArguments);
            ReadSourceCode();
        }

        /// <summary>
        /// Create a new source code object that just contain a list of code
        /// </summary>
        /// <param name="aSourceCodeForTesting"></param>
        public SourceCode(List<String> aSourceCodeForTesting)
        {
            foreach (String code in aSourceCodeForTesting)
            {
                if (ContainCode(code))
                    sourceCode.Add(GetCode(code));
            }
        }

        private Dictionary<string, string> ResolveArguments(string[] aArguments)
        {
            if (this.arguments == null)
            {
                this.arguments = new Dictionary<string, string>();
                for (int i = 0; i < aArguments.Length; i++)
                {
                    if (aArguments[i].StartsWith("-"))
                    {
                        if (aArguments[i].Trim().IndexOf(":") == -1)
                        {
                            String dictKey = aArguments[i].Trim().Remove(0, 1);
                            this.arguments.Add(dictKey, "");
                        }
                        else
                        {
                            String[] st = aArguments[i].Trim().Split(':');
                            String dictKey = st[0].Trim().Remove(0, 1);
                            StringBuilder val = new StringBuilder();
                            for (int j = 1; j < st.Length; j++)
                                val.Append(st[j]);

                            this.arguments.Add(dictKey, val.ToString());
                        }
                    }
                }
            }
            return this.arguments;
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
            StringBuilder result = new StringBuilder();
            if (this.sourceFile.Name.CompareTo("Program.cs") == 0 && ContainProgramArgument("mainclass"))
            {
                String mainClassVal = GetProgramArgumentValue("mainclass");
                if (mainClassVal.Length >= 5 && mainClassVal.Length <= 20)
                {
                    result.Append(mainClassVal);
                    if (!mainClassVal.EndsWith(".java"))
                        result.Append(".java");
                }
            }
            else
                result.Append(this.sourceFile.Name.Replace(".cs", ".java"));
            
            return result.ToString();
        }

        /// <summary>
        /// Get the java destination file name full name
        /// </summary>
        /// <returns></returns>
        public string GetJavaDestinationFullName()
        {
            StringBuilder result = new StringBuilder();
            
            result.Append(this.sourceFile.DirectoryName);
            
            if (!this.sourceFile.DirectoryName.EndsWith(@"\"))
                result.Append(@"\");

            result.Append(GetJavaDestinationFileName()); 
            
            return result.ToString();
        }

        /// <summary>
        /// Get the source file name
        /// </summary>
        /// <returns></returns>
        public String GetFileName()
        {
            return this.sourceFile != null ? this.sourceFile.Name : "Unit Test";
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
            return aCodeLine != null && aCodeLine.Trim().Length > 0;
        }

        /// <summary>
        /// Get the previous code line that isn't the same as the current code line
        /// </summary>
        /// <param name="aCodeLine"></param>
        /// <param name="aLinePosition"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Indicate if program contain a given argument
        /// </summary>
        /// <param name="aSeachArgument"></param>
        /// <returns></returns>
        public bool ContainProgramArgument(string aSeachArgument)
        {
            bool result = false;

            if (arguments != null && !String.IsNullOrEmpty(aSeachArgument))
                result = this.arguments.ContainsKey(aSeachArgument);
            
            return result;
        }

        /// <summary>
        /// Indicate if there are more code
        /// </summary>
        /// <param name="aLinePosition"></param>
        /// <returns></returns>
        public bool IsThereMoreCode(int aLinePosition)
        {
            bool result = false;

            for (int i = aLinePosition; i < this.sourceCode.ToArray().Length; i++)
            {
                if (ContainCode(this.sourceCode.ToArray()[i].Trim()))
                    result = true;
            }

            return result;
        }

        /// <summary>
        /// Count how many times a token is found in a string
        /// </summary>
        /// <param name="aCurrentCodeLine"></param>
        /// <param name="aSearchChar"></param>
        /// <returns></returns>
        public int CountTokens(string aCurrentCodeLine, char aSearchChar)
        {
            int result = 0;
            char[] ch = aCurrentCodeLine.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i].CompareTo(aSearchChar) == 0)
                result++;
            }
            return result;
        }

        /// <summary>
        /// Check if the code contain a specific keyword
        /// </summary>
        /// <param name="aCode"></param>
        /// <param name="aKeyWord"></param>
        /// <returns></returns>
        public bool ContainKeyword(String aCode, String aKeyWord)
        {
            bool result = false;
            
            if (aCode.IndexOf(" ") > -1)
            {
                var found = (from c in aCode.Split(' ')
                             where c.CompareTo(aKeyWord.Trim()) == 0
                             select c).FirstOrDefault();
                result = found != null;
            }

            return result;
        }

        /// <summary>
        /// Remove a keyword from a code line
        /// </summary>
        /// <param name="aCode"></param>
        /// <param name="aKeyWord"></param>
        /// <returns></returns>
        public String RemoveKeyword(String aCode, String aKeyWord)
        {
            StringBuilder result = new StringBuilder();

            if (aCode.IndexOf(" ") > -1)
            {
                List<String> codeLeft = (from c in aCode.Split(' ')
                             where c.CompareTo(aKeyWord.Trim()) != 0
                             select c).ToList<String>();

                foreach (String code in codeLeft)
                    result.Append(code).Append(" ");
            }
            else
                result.Append(aCode);

            return result.ToString();
        }

        /// <summary>
        /// Replace one keyword with another
        /// </summary>
        /// <param name="aCode"></param>
        /// <param name="aOldKeyWord"></param>
        /// <param name="aNewKeyWord"></param>
        /// <returns></returns>
        public String ReplaceKeyword(string aCode, string aOldKeyWord, string aNewKeyWord)
        {
            StringBuilder result = new StringBuilder();

            if (aCode.IndexOf(" ") > -1)
            {
                List<String> keywords = (from c in aCode.Split(' ')
                                         select c).ToList<String>();

                foreach (String keyword in keywords)
                {
                    if (keyword.CompareTo(aOldKeyWord) == 0)
                      result.Append(aNewKeyWord).Append(" ");
                    else
                        result.Append(keyword).Append(" ");
                }
            }
            else
                result.Append(aCode);

            return result.ToString();
        }

        /// <summary>
        /// Get the program parameter value
        /// </summary>
        /// <param name="aSearchKey"></param>
        /// <returns></returns>
        public String GetProgramArgumentValue(string aSearchKey)
        {
            String result = "";
            if (this.arguments != null && this.arguments.ContainsKey(aSearchKey))
                this.arguments.TryGetValue(aSearchKey, out result);

            return result;
        }
    }
}
