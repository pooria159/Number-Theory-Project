using System;
using System.Collections.Generic;
using System.Linq;

namespace Q2
{
    public class FA
    {
        public List<string> final_state = new List<string>();
        public string initial_state = " ";
        public List<string> Alfa = new List<string>();
        public List<string> States = new List<string>();
        public Dictionary<string , Dictionary<string , List<string>>> dis = new Dictionary<string , Dictionary<string , List<string>>>();
        public string dis_st(HashSet<string> Ls)
        {
            string result = "";
            var ls = Ls.ToList();
            for(int f=0  ; f< ls.Count ; f++)
            {
                if((ls.Count()-1) != f)
                {
                    result += (ls[f]+ ",");
                }
                else
                {
                    result += (ls[f]);
                }
            }
            return result;
        }
        public bool Equal_st(HashSet<string> Ls , FA DFA)
        {
            bool final = false;
            string[] dfa_names = Ls.ToArray();
            foreach(var s in DFA.dis)
            {
                var N = s.Key.Split(',');
                if(N.Length == dfa_names.Length)
                {
                    for(int i = 0; i < dfa_names.Length; i++)
                    {
                        final = N.Contains(dfa_names[i]);
                        if(!final)
                            break;
                        else
                            continue;
                    }
                }
                if(final)
                    break;
            }
            return final;
        }
        public void Function(HashSet<string> Ls , FA DFA)
        {
            if(Ls != null && Ls.Count > 1 && Ls.Contains("trap"))
            {
                Ls.Remove("trap");
            }
            if(Ls != null && !Equal_st(Ls, DFA))
            {
                string states = dis_st(Ls);
                Dictionary<string, List<string>> d = new Dictionary<string, List<string>>();
                foreach(var a in Alfa)
                    d.Add(a, new List<string>());
                // if(DFA.dic.ContainsKey(states))
                //     DFA.dic[states]
                DFA.dis.Add(states, d);
                var dfa_states = new HashSet<string>[Alfa.Count];
                for(int i = 0; i < Alfa.Count; i++)
                {
                    if(Alfa[i] != "$")
                    {
                        HashSet<string> state = new HashSet<string>();
                        foreach(var st in Ls)
                        {
                            if(dis.ContainsKey(st))
                            {
                                var tr_st = dis[st][Alfa[i]];
                                foreach(var s in tr_st)
                                {
                                    state.Add(s);
                                }
                            }
                                if(state.Count == 0){
                                    state.Add("trap");
                                }
                                dfa_states[i] = state;
                        }
                    }
                }
                for(int i =0 ;i < Alfa.Count ; i++)
                {
                    if(Alfa[i] != "$")
                    {
                        DFA.dis[states][Alfa[i]].Add(dis_st(dfa_states[i]));
                    }
                }
                foreach(var v in dfa_states)
                {
                    Function(v,DFA);
                }
            }
        }
    public class Program
    {
        
        static void Main(string[] args)
        {
            FA fa = new FA();
            List<string> state = Console.ReadLine().Split(',').Select(x => x.Trim('{','}',' ')).ToList();
            List<string> alfba = Console.ReadLine().Split(',').Select(x => x.Trim('{','}',' ')).ToList();
            alfba.Add("$");
            List<string> final = Console.ReadLine().Split(',').Select(x => x.Trim('{','}',' ')).ToList();
            fa.final_state = final;
            fa.States = state;
            fa.Alfa = alfba;
            fa.initial_state = state[0];
            int num = int.Parse(Console.ReadLine());
            List<List<string>> Saving = new List<List<string>>();
            for(int f=0 ; f < num ; f++)
            {
                List<string> Transition = Console.ReadLine().Split(',').Select(x => x.Trim(',')).ToList();
                Saving.Add(Transition);
            }
            foreach(var a in state)
            {
                Dictionary<string , List<string>> temp = new Dictionary<string , List<string>>();
                foreach(var alpha in alfba)
                {
                    List<string> Ls = new List<string>();
                    temp.Add(alpha , Ls);
                }
                fa.dis.Add(a , temp);
            }
            foreach(var b in Saving)
            {
                fa.dis[b[0]][b[1]].Add(b[2]);
            }
            foreach(var c in fa.States)
            {
                var tr = fa.dis[c]["$"];
                var tr_lenght = tr.Count();
                if(tr_lenght > 0){
                    for(int r=0 ; r < tr_lenght ; r++)
                    {
                        foreach(var t in fa.dis[tr[r]])
                        {
                            if(fa.dis[c].ContainsKey(t.Key))
                            {
                                for(int i=0 ; i < t.Value.Count ; i++)
                                    fa.dis[c][t.Key].Add(t.Value[i]);
                            }
                            else
                                fa.dis[c].Add(t.Key , t.Value);
                        }
                    }
                }
            }
            string st = fa.initial_state;
            FA DFA = new FA();
            DFA.Alfa = alfba;
            HashSet<string> Al = new HashSet<string>();
            Al.Add(state[0]);
            fa.Function(Al , DFA);
            System.Console.WriteLine(DFA.dis.Count);
            }
        }
    }
}
