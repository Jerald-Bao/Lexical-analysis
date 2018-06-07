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
    public class NFATest
    {
        [TestMethod()]
        public void test1()
        {
            String re = "a*$";
            Re_Resolver re_resolver = new Re_Resolver();
            if (!re_resolver.Resolve(re))
            {
                Console.WriteLine("Incorrect Format of RE");
                return;
            }
            State.count = 0;
            NFA_ParseTree nfa = new NFA_ParseTree(re_resolver.parseTree);
            Assert.IsFalse(nfa.automata.states.Count > 2);
        }

    }
}
