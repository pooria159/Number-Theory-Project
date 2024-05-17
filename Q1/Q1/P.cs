// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace Q1
// {
//     public class FA
//     {
//         public List<string> final_state = new List<string>();
//         public string initial_state = " ";
//         public Dictionary<string , Dictionary<string , List<string>>> dis = new Dictionary<string , Dictionary<string , List<string>>>();
//         public bool Function(string ans , string st , int i)
//         {
//             if(ans.Length  <= i)
//             {
//                 if(final_state.Contains(st))
//                 {
//                     return true;
//                 }
//                 else
//                 {
//                     return false;
//                 }
//             }
//             else
//             {
//                 try
//                 {
//                     bool j = false;
//                     var inp = dis[st][ans[i].ToString()];
//                     if(dis[st]["$"].Count > 0)
//                     {
//                         var landa = dis[st]["$"];
//                         for(int a=0 ; a< landa.Count() ; a++)
//                         {
//                             j = Function(ans,landa[a],i);
//                             if(j)
//                             {
//                                 break;
//                             }
//                         }
//                     }
//                     if(!j)
//                     {
//                         for(int f=0 ; f< inp.Count() ; f++)
//                         {
//                             j = Function(ans,inp[f],i+1);
//                             if(j)
//                             {
//                                 break;
//                             }
//                         }
//                     }
//                     return j;
//                 }
//                 catch
//                 {
//                     return false;
//                 }
//             }
//         }
//     }
//     public class Program
//     {
//         static void Main(string[] args)
//         {
//             FA fa = new FA();
//             List<string> state = Console.ReadLine().Split(',').Select(x => x.Trim('{','}',' ')).ToList();
//             List<string> alfba = Console.ReadLine().Split(',').Select(x => x.Trim('{','}',' ')).ToList();
//             alfba.Add("$");
//             List<string> final = Console.ReadLine().Split(',').Select(x => x.Trim('{','}',' ')).ToList();
//             fa.final_state = final;
//             fa.initial_state = state[0];
//             int num = int.Parse(Console.ReadLine());
//             List<List<string>> Saving = new List<List<string>>();
//             for(int f=0 ; f < num ; f++)
//             {
//                 List<string> Transition = Console.ReadLine().Split(',').Select(x => x.Trim(',')).ToList();
//                 Saving.Add(Transition);
//             }
//             foreach(var a in state)
//             {
//                 Dictionary<string , List<string>> temp = new Dictionary<string , List<string>>();
//                 foreach(var alpha in alfba)
//                 {
//                     List<string> Ls = new List<string>();
//                     temp.Add(alpha , Ls);
//                 }
//                 fa.dis.Add(a , temp);
//             }
//             foreach(var b in Saving)
//             {
//                 fa.dis[b[0]][b[1]].Add(b[2]);
//             }
//             string ans = Console.ReadLine();
//             int i = 0;
//             string st = fa.initial_state;
//             var j = fa.Function(ans,st,i);
//             if(j)
//                 System.Console.WriteLine("Accepted");
//             else
//             {
//                 System.Console.WriteLine("Rejected");
//             }
//         }
//     }
// }
