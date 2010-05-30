using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4BB.PreValidation
{
    public sealed class OnlySingleClassPerFile: IValidate
    {
        private int totalClasses = 0;

        public String DoValidation(SourceCode aSourceCode)
        {
            String result = null;
            aSourceCode.GetLines().ForEach(CountClassName);
            if (totalClasses != 1)
                result = aSourceCode.GetFileName() + ": A C# file can only have one class just like Java. ";
            return result;
        }

        private void CountClassName(String aCode)
        {
            if (aCode.IndexOf("class") > -1)
                this.totalClasses++;
        }
    }
}
