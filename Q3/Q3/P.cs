// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace MinimizeDFA
// {
//     public class State
//     {
//         public string Name;
//         public Dictionary<string, State> Transitions;
//         public State(string name)
//         {
//             this.Name = name;
//             this.Transitions = new Dictionary<string, State>();
//         }

//         public void Add_Transition(string name, State state)
//         {
//             this.Transitions.Add(name, state);
//         }

//         public State Get_Transition_State(string alphabet)
//         {
//             return this.Transitions[alphabet];
//         }

//         public void Remove_Transition(string alphabet)
//         {
//             this.Transitions.Remove(alphabet);
//         }
//     }

//     public class DFA
//     {
//         public List<State> States;
//         public List<string> Alphabet;
//         public State Initial_State;
//         public List<State> Final_States;

//         public DFA(List<string> alphabets)
//         {
//             this.States = new List<State>();
//             this.Alphabet = alphabets;
//             this.Final_States = new List<State>();
//         }

//         public void Add_State(string name)
//         {
//             this.States.Add(new State(name));
//         }

//         public void Set_Initial_State(string name)
//         {
//             this.Initial_State = this.States.Where(state => state.Name == name).FirstOrDefault();
//         }

//         public void Add_Final_State(string name)
//         {
//             this.Final_States.Add(this.States.Where(state => state.Name == name).FirstOrDefault());
//         }

//         public void Add_Transition(string s, string a, string dest)
//         {
//             State start = this.States.Where(state => state.Name == s).FirstOrDefault();
//             State destination = this.States.Where(state => state.Name == dest).FirstOrDefault();
//             start.Add_Transition(a, destination);
//         }

//         public void Remove_None_Reachable_States()
//         {
//             Dictionary<State, bool> ReachedStates = new Dictionary<State, bool>();
//             foreach(var state in this.States)
//                 ReachedStates.Add(state, false);
//             Mark_State(this.Initial_State, ReachedStates);
//             var temp = new State[this.States.Count];
//             this.States.CopyTo(temp); // -------------------------------------------
//             foreach(var state in temp)
//             {
//                 if(!ReachedStates[state])
//                 {
//                     this.States.Remove(state);
//                     if(this.Final_States.Contains(state))
//                         this.Final_States.Remove(state);
//                 }
//             }
//         }

//         public void Mark_State(State state, Dictionary<State, bool> reachableStates)
//         {
//             reachableStates[state] = true; /// =1
//             foreach(var transition in state.Transitions)
//             {
//                 if(!reachableStates[transition.Value]) /// ==1
//                     Mark_State(transition.Value, reachableStates);
//             }
//         }

//         public bool Check_equality(State state_1, State state_2, List<HashSet<State>> list_of_sets)
//         {
//             foreach(var transition in state_1.Transitions)
//             {
//                 string terminal = transition.Key;
//                 State next_state_1 = transition.Value;
//                 State next_state_2 = state_2.Get_Transition_State(terminal);
//                 int i=0, j=0;
//                 for(int u = 0; u < list_of_sets.Count; u++)
//                 {
//                     if(list_of_sets[u].Contains(next_state_1))
//                         i = u;
//                     if(list_of_sets[u].Contains(next_state_2))
//                         j = u;
//                 }
//                 if(i != j)
//                     return false;
//             }
//             return true;
//         }

//         public int MinimizeDFA(List<HashSet<State>> list_of_sets)
//         {
//             bool flag = true;
//             while(flag)
//             {
//                 flag = false;
//                 List<HashSet<State>> new_list_of_sets = new List<HashSet<State>>();
//                 foreach(var set in list_of_sets)
//                 {
//                     int last_set = 1;
//                     HashSet<State> set_1 = new HashSet<State>();
//                     HashSet<State> set_2 = new HashSet<State>();
//                     set_1.Add(set.First());
//                     var list_set = set.ToList();
//                     for(int i = 1; i < list_set.Count; i++)
//                     {
//                         if(Check_equality(list_set[i], list_set[i-1], list_of_sets))
//                         {
//                             if(last_set == 1)
//                             {
//                                 set_1.Add(list_set[i]);
//                                 last_set = 1;
//                             }
//                             else
//                             {
//                                 set_2.Add(list_set[i]);
//                                 last_set = 2;
//                             }
//                         }
//                         else
//                         {
//                             if(last_set == 1)
//                             {
//                                 set_2.Add(list_set[i]);
//                                 last_set = 2;
//                             }
//                             else
//                             {
//                                 set_1.Add(list_set[i]);
//                                 last_set = 1;
//                             }
//                             flag = true;
//                         }
//                     }
//                     new_list_of_sets.Add(set_1);
//                     if(set_2.Count != 0)
//                         new_list_of_sets.Add(set_2);
//                 }
//                 list_of_sets = new_list_of_sets;
//             }
//             return list_of_sets.Count;
//         }
//     }
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             var states = Console.ReadLine().Split(new char[]{',', '{', '}'}, StringSplitOptions.RemoveEmptyEntries).ToArray();
//             var alphabets = Console.ReadLine().Split(new char[]{',', '{', '}'}, StringSplitOptions.RemoveEmptyEntries).ToList();
//             DFA dfa = new DFA(alphabets);
//             foreach(var state in states)
//                 dfa.Add_State(state);
//             dfa.Set_Initial_State(states[0]);
//             var final_states = Console.ReadLine().Split(new char[]{',', '{', '}'}, StringSplitOptions.RemoveEmptyEntries).ToArray();
//             foreach(var state in final_states)
//                 dfa.Add_Final_State(state);
//             int transition_count = int.Parse(Console.ReadLine());
//             for(int _ = 0; _ < transition_count; _++)
//             {
//                 var transition = Console.ReadLine().Split(',');
//                 dfa.Add_Transition(transition[0], transition[1], transition[2]);
//             }
//             dfa.Remove_None_Reachable_States();
//             HashSet<State> none_final_states = new HashSet<State>();
//             var final_states_sets = new HashSet<State>();
//             foreach(var state in dfa.States)
//                 if(!dfa.Final_States.Contains(state))
//                     none_final_states.Add(state);
//             foreach(var s in dfa.Final_States)
//                 final_states_sets.Add(s);
//             var list_of_sets = new List<HashSet<State>>();
//             list_of_sets.Add(none_final_states);
//             list_of_sets.Add(final_states_sets);
//             System.Console.WriteLine(dfa.MinimizeDFA(list_of_sets));
//         }
//     }
// }
