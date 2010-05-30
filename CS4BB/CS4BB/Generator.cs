using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4BB
{
    class Generator
    {
        private SourceCode sourceCode;
        private bool displayProgress;
        private List<String> errors = new List<String>();

        public Generator(SourceCode sourceCode)
        {
            this.sourceCode = sourceCode;
        }

        public Generator(SourceCode sourceCode, bool aDisplayProgress)
        {
            this.sourceCode = sourceCode;
            this.displayProgress = aDisplayProgress;
        }
        
        /// <summary>
        /// Start the compile process
        /// </summary>
        public void Run()
        {
            if (displayProgress)
                Console.WriteLine("Start compiling... File: {0}", this.sourceCode.GetFileName());
        }

        /// <summary>
        /// Indicate if there are any compile errors
        /// </summary>
        /// <returns></returns>
        public bool HasErrors()
        {
            return this.errors != null && this.errors.Count > 0;
        }

        /// <summary>
        /// Return compile errors
        /// </summary>
        /// <returns></returns>
        public List<String> GetErrors()
        {
            return this.errors;
        }
    }
}
