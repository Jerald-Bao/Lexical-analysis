﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE2DFA
{
    public class State
    {
        public Dictionary<Symbol,State> transfer;
        public List<State> nullTransfer;
        public State()
        {
            transfer = new Dictionary<Symbol, State>();
            nullTransfer = new List<State>();
        }
        public State Transfer(Symbol symbol)
        {
            return transfer[symbol];
        }
    }
    
}
