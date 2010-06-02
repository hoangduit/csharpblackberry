﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace CS4BB.lang
{
    public sealed class KeywordsComp: ICommand
    {
        public bool Identify(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            bool result = false;

            var found = (from k in GetKeyworksToConvert()
                         where aCurrentCodeLine.IndexOf(k.CSharp) > -1
                         select k).FirstOrDefault();
            if (found != null)
                result = true;

            return result;
        }

        public TargetCodeResult Compile(SourceCode aSourceCode, string aCurrentCodeLine, int aLinePosition)
        {
            String codeResult = aCurrentCodeLine;

            foreach (KeywordsKeyword keyword in GetKeyworksToConvert())
            {
                if (codeResult.IndexOf(keyword.CSharp) > -1)
                       codeResult = codeResult.Replace(keyword.CSharp, keyword.Java);
            }

            return new TargetCodeResult(codeResult);
        }

        private KeywordsKeyword[] GetKeyworksToConvert()
        {
            KeywordsKeyword[] result = null;

            StreamReader str = new StreamReader("xml/Keywords.xml");
            try
            {
                XmlSerializer xSerializer = new XmlSerializer(typeof(Keywords));
                Keywords toConvertKeywords = (Keywords)xSerializer.Deserialize(str);
                if (toConvertKeywords != null)
                    result = toConvertKeywords.Items;
            }
            finally
            {
                if (str != null)
                    str.Close();
            }

            return result;
        }
    }
}
