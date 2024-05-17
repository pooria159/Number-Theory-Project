using System;
using System.Collections.Generic;
using System.Linq;

namespace Q3
{
    public class FA
    {
        public List<string> final_state = new List<string>();
        public string initial_state = " ";
        public List<string> Alfa = new List<string>();
        public List<string> States = new List<string>();
        public Dictionary<string , Dictionary<string , List<string>>> dis = new Dictionary<string , Dictionary<string , List<string>>>();
        public void pointer(string st , Dictionary<string , int> Rechable)
        {
            Rechable[st]=1;
            foreach(var tr in dis[st]){
                if(tr.Value.Count > 0 && Rechable[tr.Value[0]]==0){
                    pointer(tr.Value[0], Rechable);
                }
            }
        }
        public void nonRechable()
        {
            Dictionary<string , int> rh = new Dictionary<string , int>();
            foreach(var st in States){
                rh.Add(st,0);
            }pointer(initial_state , rh);
            var buff = new string[States.Count()];
            for(int a=0 ; a < States.Count() ; a++){
                buff[a] = States[a];
            }
            foreach(var newst in buff){
                if(rh[newst]==0){
                    States.Remove(newst);
                    if(final_state.Contains(newst)){
                        final_state.Remove(newst);
                    }
                }
            }
        }
        public int have_st(List<List<string>> equal , string stt , string ost )
        {
            foreach(var st in dis[stt]){
                string next_st = st.Value[0];
                string next_ost = dis[ost][st.Key][0];
                int check_one = 0;
                int check_two = 0;
                for(int f=0 ; f< equal.Count() ; f++){
                    if(equal[f].Contains(next_st)){
                        check_one = f;
                    }
                    if(equal[f].Contains(next_ost)){
                        check_two =f;
                    }
                }
                if(check_one != check_two){
                    return 0;
                }
            }return 1;
        }
        public void Function(List<List<string>> Ls)
        {
            int temp = 1 ;
            int res = 0;
            while(temp==1)
            {
                temp = 0;
                List<List<string>> St_in = new List<List<string>>();
                foreach(var st in Ls){
                    bool pop = true;
                    List<string> append_one = new List<string>();
                    List<string> append_two = new List<string>();
                    append_one.Add(st[0]);
                    for(int a=1 ; a< st.Count() ; a++)
                    {
                        if(have_st(Ls,st[a],st[a-1])==1){
                            if(pop == true){
                                append_one.Add(st[a]);
                            }
                            else{
                                append_two.Add(st[a]);
                                pop = false;
                            }
                        }
                        else{
                            if(pop == true){
                                append_two.Add(st[a]);
                                pop = false;
                            }
                            else{
                                append_one.Add(st[a]);
                                pop = true;
                            }temp = 1 ;
                        }
                    }
                    St_in.Add(append_one);
                    if(append_two.Count() !=0){
                        St_in.Add(append_two);
                    }
                }
                Ls = St_in;
            }
            res = Ls.Count();
            Console.WriteLine(res);
        }
    public class Program
    {
        
        static void Main(string[] args)
        {
            FA fa = new FA();
            List<string> state = Console.ReadLine().Split(',').Select(x => x.Trim('{','}',' ')).ToList();
            List<string> alfba = Console.ReadLine().Split(',').Select(x => x.Trim('{','}',' ')).ToList();
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
            fa.nonRechable();
            List<string> none_final_states = new List<string>();
            List<string> final_sts = new List<string>();
            foreach(var s in fa.States)
                if(!fa.final_state.Contains(s))
                    none_final_states.Add(s);
            
            foreach(var s in fa.final_state)
                final_sts.Add(s);
            List<List<string>> Al = new List<List<string>>();
            Al.Add(final_sts);
            Al.Add(none_final_states);
            fa.Function(Al);
            }
        }
    }
}
