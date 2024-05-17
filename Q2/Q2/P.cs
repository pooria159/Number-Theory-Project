using System;
using System.Collections.Generic;
using System.Linq;

namespace Convert_NFA_TO_DFA
{
    public class State
    {
        public string Name;
        public Dictionary<string, List<State>> Transitions;
        public State(string name)
        {
            this.Name = name;
            this.Transitions = new Dictionary<string, List<State>>();
        }

        public void Add_Transition(string name, State state)
        {
            if(!this.Transitions.ContainsKey(name))
                this.Transitions.Add(name, new List<State>());
            this.Transitions[name].Add(state);
        }

        public List<State> Get_Transition_State(string alphabet)
        {
            if(!this.Transitions.ContainsKey(alphabet))
                return new List<State>();
            return this.Transitions[alphabet];
        }
    }

    public class NFA
    {
        public List<State> States;
        public List<string> Alphabet;
        public State Initial_State;
        public List<State> Final_States;

        public NFA(){}
        public NFA(List<string> alphabets)
        {
            this.States = new List<State>();
            this.Alphabet = alphabets;
            this.Final_States = new List<State>();
        }

        public State Add_State(string name)
        {
            State s = new State(name);
            this.States.Add(s);
            return s;
        }

        public void Set_Initial_State(string name)
        {
            this.Initial_State = this.States.Where(state => state.Name == name).FirstOrDefault();
        }

        public void Add_Final_State(string name)
        {
            this.Final_States.Add(this.States.Where(state => state.Name == name).FirstOrDefault());
        }

        public void Add_Transition(string s, string a, string dest)
        {
            State start = this.States.Where(state => state.Name == s).FirstOrDefault();
            State destination = this.States.Where(state => state.Name == dest).FirstOrDefault();
            start.Add_Transition(a, destination);
        }

        public void Check_Transition(State state)
        {
            var lambda_transition = state.Get_Transition_State("$");
            int len = lambda_transition.Count;;
            if(lambda_transition.Count > 0)
            {
                for(int i = 0; i < len; i++)
                {
                    foreach(var item in lambda_transition[i].Transitions)
                        foreach(var j in item.Value)
                            state.Add_Transition(item.Key, j);
                }
            }
        }

        public bool Check_String(State start, int i, string str)
        {
            if(i >= str.Length)
            {
                if(this.Final_States.Contains(start))
                    return true;
                return false;
            }
            bool accepted = false;
            List<State> next_states;
            if(start.Transitions.ContainsKey("$"))
            {
                next_states = start.Get_Transition_State("$");
                foreach(var state in next_states)
                {
                    accepted = Check_String(state, i, str);
                    if(accepted)
                        break;
                }
            }
            if(!accepted)
            {
                if(!start.Transitions.ContainsKey(str[i].ToString()))
                    return accepted;
                next_states = start.Get_Transition_State(str[i].ToString());
                foreach(var state in next_states)
                {
                    accepted = Check_String(state, i + 1, str);
                    if(accepted)
                        break;
                }
            }
            return accepted;
        }

        public NFA Convert_To_DFA(HashSet<State> states, NFA dfa)
        {
            if(have_states(dfa, states))
                return dfa;
            var st = dfa.Add_State(merge_name(states));
            HashSet<State>[] new_states = new HashSet<State>[this.Alphabet.Count];
            for(int i = 0; i < this.Alphabet.Count; i++)
            {
                    HashSet<State> new_state = new HashSet<State>();
                    foreach(State state in states)
                    {
                        var transition_state = state.Get_Transition_State(this.Alphabet[i]);
                        foreach(State s in transition_state)
                        {
                            new_state.Add(s);
                        }
                    }
                    if(new_state.Count == 0)
                        new_state.Add(new State("trap"));
                    new_states[i] = new_state;
            }
            for(int i = 0; i < new_states.Length; i++)
            {
                st.Add_Transition(this.Alphabet[i], new State(merge_name(new_states[i])));
            }
            foreach(var item in new_states)
                Convert_To_DFA(item, dfa);

            return dfa;
        }
        public bool have_states(NFA dfa, HashSet<State> states)
        {
            bool result = false;
            string[] name = states.Select(s => s.Name).ToArray();
            var dfa_states = dfa.States;
            foreach(var state in dfa_states)
            {
                var names = state.Name.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
                if(names.Length == name.Length)
                {
                    for(int i = 0; i<name.Length; i++)
                    {
                        result = names.Contains(name[i]);
                        if(!result) break;
                    }
                }
                if(result) break;
            }
            return result;
        }

        public string merge_name(HashSet<State> states)
        {
            var other = states.ToList();
            string name = "";
            for(int i = 0; i < other.Count; i++)
                name += (i != other.Count - 1) ? $"{other[i].Name}," : $"{other[i].Name}";
            return name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var states = Console.ReadLine().Split(new char[]{',', '{', '}'}, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var alphabets = Console.ReadLine().Split(new char[]{',', '{', '}'}, StringSplitOptions.RemoveEmptyEntries).ToList();
            alphabets.Remove("$");
            NFA nfa = new NFA(alphabets);
            foreach(var state in states)
                nfa.Add_State(state);
            nfa.Set_Initial_State(states[0]);
            var final_states = Console.ReadLine().Split(new char[]{',', '{', '}'}, StringSplitOptions.RemoveEmptyEntries).ToArray();
            foreach(var state in final_states)
                nfa.Add_Final_State(state);
            int transition_count = int.Parse(Console.ReadLine());
            for(int _ = 0; _ < transition_count; _++)
            {
                var transition = Console.ReadLine().Split(',');
                nfa.Add_Transition(transition[0], transition[1], transition[2]);
            }

            foreach(var state in nfa.States)
            {
                nfa.Check_Transition(state);
            }

            NFA dfa = new NFA(alphabets);
            var DFA = nfa.Convert_To_DFA(new HashSet<State>{nfa.Initial_State}, dfa);
            System.Console.WriteLine(DFA.States.Count);
        }
    }
}
