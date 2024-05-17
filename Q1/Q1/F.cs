using System;
using System.Collections.Generic;
using System.Collections;
namespace TLA
{
    class Program
    {     
        public static string Filter(string str)
        {
            List<char> charsToRemove = new List<char>() {'{' , ',', '}'};
            foreach (char c in charsToRemove) 
            {
                str = str.Replace(c.ToString(), String.Empty);
            }
            return str;
        }
        public static string Func(string states, List<string> Edges, string FinishStr, string FinalState)
        {
            string result = "Rejected";
            int size = FinishStr.Length;
            char a;
            char state = states[0];
            for(int i = 0; i < size; i++)
            {
                a = FinishStr[i];
                int boolean = 0;
                for(int j = 0; j < Edges.Count; j++)
                {
                    if(Edges[j][0] == state && Edges[j][1] == a)
                    {
                        boolean = 1;
                        state = Edges[j][2];
                    }
                }
                if(boolean == 0)
                {
                    return result;
                }      
            }
            int size1 = FinalState.Length;
            int boolean1 = 0;
            for(int i = 0 ; i < size1; i++)
            {
                if(state != FinalState[i])
                {
                    boolean1 = 1;
                    break;
                }
            }
            if(boolean1 == 0)
            {
                return result;
            }
            else{
                result = "Accepted";
                return result;
            }
        }
        public static void Main(string[] args)
        {
            // string a = "FRNZ";
            // Console.WriteLine(a[1].GetType() );
            List<string> Edges = new List<string>();
            string state = Console.ReadLine();
            state = Filter(state);

            string alphabet = Console.ReadLine(); 
            alphabet = Filter(alphabet); 

            string FinalState = Console.ReadLine();
            FinalState = Filter(FinalState);

            int numOfEdges = int.Parse(Console.ReadLine()); 
            for(int i = 0 ; i < numOfEdges ; i++)
            {
                string input = Console.ReadLine();
                input = Filter(input);
                Edges.Add(input);
            }
            string FinishStr = Console.ReadLine();
            FinishStr = Filter(FinishStr);
            string res = Func(state, Edges, FinishStr, FinalState);
            Console.WriteLine(res);
        }
    }
}