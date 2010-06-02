using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CS4BB.Tests.Core
{
    [TestFixture]
    class KeywordsCompTest
    {
        [Test]
        public void ConvertBase()
        {
            List<String> code = new List<String>();
            code.Add("base.setTitle(\"Hello World\");");
            Generator gen = new Generator(code, true);
            gen.Run();
            Assert.IsFalse(gen.HasErrors(), "Not suppose to have errors");
            Assert.AreEqual("super.setTitle(\"Hello World\");", gen.GetAllCode());
        }
    }
}
