using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RE2DFA;
namespace Tests
{
    [TestClass()]
    class DFATest
    {
        [TestMethod()]
        public void test1()
        {
            DFA dfa = DFA_Builder.BuildDFA("a*bc");
            //    dfa.closures.Add
        }
    }
}
