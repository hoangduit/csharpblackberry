using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS4BB.lang
{
    public interface ICommand
    {
        bool Identify(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition);
        TargetCodeResult Compile(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition);
    }
}
