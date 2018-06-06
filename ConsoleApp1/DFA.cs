using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE2DFA
{
    public class DFA: Automata 
    {
        Dictionary<State,List<State>> closures;
        public DFA(Automata NFA)
        {
            closures = new Dictionary<State, List<State>>();
            //创建所有节点的闭包
            foreach (State state in NFA.states)
            {
                List<State> closure = new List<State>();
                BuildClosure(state, closure);
                closures.Add(state,closure);
            }
            foreach (State state in NFA.states)
            {
                if (true)
                {
                    State newState = new State();
                    this.states.Add(newState);
                }
            }

            
        }
        //递归创建单个节点的闭包
        void BuildClosure(State state,List<State> closure)
        {
            foreach(State transfer in state.nullTransfer)
            {
                if (!closure.Contains(transfer))
                {
                    closure.Add(transfer);
                    BuildClosure(transfer, closure);
                }
            }
        }
        
    }
}
