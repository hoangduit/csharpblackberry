using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CS4BB
{
    class SourceCode
    {
        private FileInfo sourceFile;
        private List<String> sourceCode = new List<String>();

        public SourceCode(FileInfo aSourceFile)
        {
            this.sourceFile = aSourceFile;
            ReadSourceCode();
        }

        private void ReadSourceCode()
        {
            StreamReader file = null;
            try
            {
                file = new StreamReader(sourceFile.FullName);
                String line = null;
                while ((line = file.ReadLine()) != null)
                    sourceCode.Add(line);
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
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
        /// Get the source file name
        /// </summary>
        /// <returns></returns>
        public String GetFileName()
        {
            return this.sourceFile.Name;
        }
    }
}
