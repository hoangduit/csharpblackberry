using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4BB
{
    public sealed class TargetCodeResult
    {
        public TargetCode CurrentCodeLine { get; set; }
        public List<TargetCode> allTargetCode;

        /// <summary>
        /// Create a new source target
        /// </summary>
        /// <param name="aCode"></param>
        public TargetCodeResult(string aCode): this()
        {            
            this.CurrentCodeLine.Code = aCode;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TargetCodeResult()
        {
            this.CurrentCodeLine = new TargetCode();
            this.CurrentCodeLine.Success = true;
            this.CurrentCodeLine.ErrorMessage = null;
        }

        /// <summary>
        /// Log the error
        /// </summary>
        /// <param name="aErrorMessage"></param>
        public void LogError(string aErrorMessage)
        {
            this.CurrentCodeLine.ErrorMessage = aErrorMessage;
            this.CurrentCodeLine.Success = false;
        }

        /// <summary>
        /// Get the current target Java code line
        /// </summary>
        /// <returns></returns>
        public string GetCurrentCode()
        {
            String result = this.CurrentCodeLine.Code;
            if (result.CompareTo("<br>") == 0)
                result = "";
            return result;
        }

        /// <summary>
        /// Get all the target code
        /// </summary>
        /// <returns></returns>
        public String GetAllCode()
        {
            StringBuilder result = new StringBuilder();
            foreach (TargetCode code in this.allTargetCode)
                result.Append(code.Code);

            return result.ToString();
        }

        /// <summary>
        /// Check if code is valid
        /// </summary>
        /// <returns></returns>
        public bool IsValidCode()
        {
            return CurrentCodeLine != null && CurrentCodeLine.Code != null && CurrentCodeLine.Code.Length > 0;
        }

        /// <summary>
        /// Add code end result is that this class will contain the entire target code
        /// </summary>
        /// <param name="aTarget"></param>
        public void AddTargetCode(TargetCode aTarget)
        {
            if (this.allTargetCode == null)
                this.allTargetCode = new List<TargetCode>();

            this.allTargetCode.Add(aTarget);
        }
    }
}
