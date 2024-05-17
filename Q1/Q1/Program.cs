// using System;
// using System.Collections;
// using System.Collections.Generic;
// namespace FiniteAutomataAcception
// {
//     class State
//     {
//         public string Name { get; set; }
//         public bool IsFinal { get; set; }
//         public Dictionary<string , State> Transitions { get; set; }

//         public State(string Name)
//         {
//             this.Name = Name;
//             this.IsFinal = false;
//             this.Transitions = new Dictionary<string, State>();
//         }
//     }
    
//     class Program
//     {
//         public static int indexOfStatebyName(string name, List<State> states)
//         {
//             for (int i = 0; i < states.Count; i++)
//             {
//                 if (name == states[i].Name)
//                     return i;

//             }

//             return -1;
//         } 

//         public static bool IsAccepted(State currentState , List<string> alphabet , List<State> EndStates , char[] input , int index )
//         {
//             if(index == input.Length)
//             {
//                 return false;
//             }
           
//             if(currentState.IsFinal == true && input.Length - 1 == index)
//             {
//                 return true;
//             }

//            foreach(var tran in currentState.Transitions)
//             {
//                 if(input[index].ToString() == tran.Key && input[index].ToString() != "$")
//                 {
                    
//                     return IsAccepted(tran.Value, alphabet, EndStates, input, index + 1);
//                 }
//                 else if(input[index].ToString() == tran.Key && input[index].ToString() == "$")
//                 {
//                     return IsAccepted(tran.Value, alphabet, EndStates, input, index);
//                 }
//             }

//             return false;

//            //while(true)
//            // {
//            //     if (index == input.Length - 1)
//            //     {
//            //         break;
//            //     }
//            //     bool hasTransition = false;
//            //     foreach(var tran in currentState.Transitions)
//            //     {
                    
//            //     }

//            //     index++;
                
//            // }
           
//         }
//         static void Main(string[] args)
//         {
//             List < State > States = new List<State>();
//             List<string> alphabet = new List<string>();

//             List<State> EndStates = new List<State>();
 

//             string StatesInput = Console.ReadLine();
//             StatesInput = StatesInput.Remove(StatesInput.Length - 1 , 1);
//             StatesInput = StatesInput.Remove(0, 1);

//             string[] StatesStrArr = StatesInput.Split(',');
//             foreach(var x in StatesStrArr)
//             {
//                 States.Add(new State(x));
//             }

//             string alphabetInput = Console.ReadLine();
//             alphabetInput = alphabetInput.Remove(alphabetInput.Length - 1, 1);
//             alphabetInput = alphabetInput.Remove(0, 1);

//             string[] alphabetStrArr = alphabetInput.Split(',');

//             string FinalStateInput = Console.ReadLine();
//             FinalStateInput = FinalStateInput.Remove(FinalStateInput.Length - 1, 1);
//             FinalStateInput = FinalStateInput.Remove(0, 1);
//             string[] FinalStateArr = FinalStateInput.Split(',');

//             foreach(var final in FinalStateArr)
//             {
//                 foreach(var state in States)
//                 {
//                     if (final == state.Name)
//                     {
//                         state.IsFinal = true;
//                         EndStates.Add(state);
//                     }
                        
//                 }
//             }
//             List<string> transitionList = new List<string>();
//             int transitionRulesLength = int.Parse(Console.ReadLine());
//             for(int i  = 0; i<transitionRulesLength; i++)
//             {
//                 string rule = Console.ReadLine();
//                 transitionList.Add(rule);
//                 string[] rulearr = rule.Split(',');

//                 foreach(var x in States)
//                 {
//                     if(x.Name == rulearr[0])
//                     {
//                         x.Transitions.Add(rulearr[1], States[indexOfStatebyName(x.Name , States)] );
//                     }
//                 }
//             }

//             string input = Console.ReadLine();

            
           
//             if(IsAccepted(States[0] , alphabet , EndStates  , input.ToCharArray() , 0 ))
//             {
//                 Console.WriteLine("Accepted");
//             }
//             else
//             {
//                 Console.WriteLine("Rejected");
//             }








//         }
//     }
// }
