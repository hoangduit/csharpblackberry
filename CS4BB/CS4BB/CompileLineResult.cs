using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4BB
{
    public sealed class CompileLineResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        private string newSourceLine;

        public CompileLineResult(string aNewSourceLine)
        {
            this.newSourceLine = aNewSourceLine;
            this.Success = true;
            this.ErrorMessage = null;
        }

        public void LogError(string aErrorMessage)
        {
            this.ErrorMessage = aErrorMessage;
            this.Success = false;
        }

        public string GetCode()
        {
            return newSourceLine;
        }

        public bool IsValidCode()
        {
            return newSourceLine != null && newSourceLine.Length > 0;
        }
    }
}
