using System;
using System.Collections.Generic;

namespace Flashcards
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Question> pool = MakePool();

            string ans = "";
            Random random = new Random();
            int i = 0;
            int correct = 0;
            int total = 0;

            while (ans.ToUpper() != "Q")
            {
                if (pool.Count < 1)
                {
                    Console.WriteLine("No (more) questions avilable in the pool!");
                    ans = Console.ReadKey().KeyChar.ToString();
                    break;
                }

                i = random.Next(0, pool.Count);
                Console.WriteLine("Correct: " + correct + "   Total: " + total);
                Console.WriteLine(pool[i].id + "\n");
                Console.WriteLine("  " + pool[i].questionText);
                Console.WriteLine("    " + pool[i].A);
                Console.WriteLine("    " + pool[i].B);
                Console.WriteLine("    " + pool[i].C);
                Console.WriteLine("    " + pool[i].D);
                ans = Console.ReadKey().KeyChar.ToString();
                if (ans.ToUpper() == pool[i].answer)
                {
                    Console.WriteLine("\nCorrect!");
                    correct++;
                    total++;
                }
                else
                {
                    Console.WriteLine("\nThe correct answer was: " + pool[i].answer);
                    total++;
                }
                //System.Threading.Thread.Sleep(2500);
                ans = Console.ReadKey().KeyChar.ToString();

                //stop repeats
                pool.RemoveAt(i);

                Console.Clear();
            }
        }

        static private List<Question> MakePool()
        {
            string[] raw = System.IO.File.ReadAllLines("arrl.txt");
            List<Question> pool = new List<Question>();
            int i = 0;
            while (i < raw.Length)
            {
                // start of a question block
                if (raw[i].Length > 5 && raw[i].Substring(0, 1) == "T" && int.TryParse(raw[i].Substring(3, 2), out int n))
                {
                    Question q = new Question();
                    q.id = raw[i].Substring(0, 5);
                    q.answer = raw[i].Substring(7, 1);
                    q.questionText = raw[i + 1];
                    //q.A = raw[i + 2].Substring(3);
                    //q.B = raw[i + 3].Substring(3);
                    //q.C = raw[i + 4].Substring(3);
                    //q.D = raw[i + 5].Substring(3);
                    q.A = raw[i + 2];
                    q.B = raw[i + 3];
                    q.C = raw[i + 4];
                    q.D = raw[i + 5];
                    pool.Add(q);
                    i = i + 6;
                }
                // not, keep looking
                else
                {
                    //Console.WriteLine("Skipping: " + raw[i]);
                    i++;
                }
            }

            return pool;
        }
    }

    class Question
    {
        public string id;
        public string answer;
        public string questionText;
        public string A;
        public string B;
        public string C;
        public string D;
    }
}
