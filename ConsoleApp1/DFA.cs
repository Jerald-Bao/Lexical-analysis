using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE2DFA
{
    public class DFA: Automata 
    {
        public Dictionary<State, List<State>> closures;
        public List<IState> iStates = new List<IState>();
        public DFA(Automata NFA)
        {
            if (NFA.states.Count == 0)
                return;
            closures = new Dictionary<State, List<State>>();
            //创建所有节点的闭包
            foreach (State state in NFA.states)
            {
                List<State> closure = new List<State>();
                BuildClosure(state, closure);
                closures.Add(state,closure);
            }

            //设置NFA入口State闭包为DFA入口State
            IState iState0 = new IState(iStates.Count);
            entry = iState0;
            closures.TryGetValue(NFA.entry,out iState0.collection);
            iStates.Add(iState0);
            findNewIState(iState0);
            states = new List<State>();
            terminators = new List<State>();
            
            //将IState转换成标准State
            foreach(IState iState in iStates)
            {
                State s=iState;
                states.Add(s);
                if (iState.terminate)
                    this.terminators.Add(s);
            }
            //递归创建单个节点的闭包
            void BuildClosure(State state, List<State> closure)
            {
                foreach (State transfer in state.nullTransfer)
                {
                    if (!closure.Contains(transfer))
                    {
                        closure.Add(transfer);
                        BuildClosure(transfer, closure);
                    }
                }
            }
            //递归找到所有的DFA节点
            void findNewIState(IState iState)
            {
                iState.transfer = new Dictionary<Symbol, IState>();
                //计算{Symbol,ε-closure(move(IState,Symbol))}
                foreach (State state in iState.collection)
                {
                    foreach (Symbol symbol in state.transfer.Keys)
                    {
                        IState nextIState;
                        State nextNFAState;
                        state.transfer.TryGetValue(symbol, out nextNFAState);
                        if (iState.transfer.TryGetValue(symbol, out nextIState))
                        {
                            if (!nextIState.collection.Contains(nextNFAState))
                            {
                                List<State> closure;
                                closures.TryGetValue(nextNFAState, out closure);
                                nextIState.collection.AddRange(closure);
                            }
                        }
                        else
                        {
                            nextIState = new IState(iStates.Count);
                            nextIState.collection = new List<State>();
                            List<State> closure;
                            closures.TryGetValue(nextNFAState, out closure);
                            nextIState.collection.AddRange(closure);
                        }
                    }
                }
                //计算IState等价性
                foreach (Symbol symbol in iState.transfer.Keys)
                {
                    IState nextIState;
                    iState.transfer.TryGetValue(symbol, out nextIState);
                    if (iStates.Contains(nextIState))
                    {
                        nextIState = iStates.Find((x) => nextIState.Equals(x));
                        iState.transfer.Add(symbol, nextIState);
                    }
                    else
                    {
                        iStates.Add(nextIState);
                        iState.transfer.Add(symbol, nextIState);
                        if (nextIState.collection.Contains(NFA.terminator))
                            nextIState.terminate = true;

                        findNewIState(nextIState);
                    }
                }
            }
        }
        
        

        
        
    }
}
