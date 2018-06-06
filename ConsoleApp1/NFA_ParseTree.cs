namespace RE2DFA
{
    public class NFA_ParseTree :ParseTree
    {
        public new NFA_Node root;
        public Automata automata;
        public NFA_ParseTree(ParseTree parseTree)
        {
            root = (NFA_Node)parseTree.root;
            root.BuildAutomata();
            automata = root.automata;
        }

    }
}