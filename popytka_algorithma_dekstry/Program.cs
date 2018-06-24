using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popytka_algorithma_dekstry
{
    class Program
    {
        public class E
        {
            public int input = 0;
            public int mass = 0;
            public E() { }
            public E(int a, int b) { this.input = a; this.mass = b; } 
        }
        public class V
        {
            public bool enable = true;
            public int number;
            public List<E> sm = new List<E>();
            public int mass = 1000;

            public V() { }
            public V(int number)
            {
                this.number = number; 
            }
            public void Add(int a,int mass)
            {
                sm.Add(new E(a, mass));
            }
        }

        static public V[] ver;
        static Queue<V> qw = new Queue<V>();

        static void Main(string[] args)
        {
            Console.Write("Введите размер: ");
            int N = Convert.ToInt32(Console.ReadLine());
            ver = new V[N];

            for (int i = 0; i < ver.Length; i++)
            {
                Console.WriteLine("Вершина номер {0}", i + 1);
                ver[i] = new V(i + 1);
                int x = 1;
                while (x != 0)
                {
                    Console.Write("Вводите смежную вершину: ");
                    x = Convert.ToInt32(Console.ReadLine());
                    if (x ==0) break;
                    Console.Write("Вводите веса: ");
                    int y = Convert.ToInt32(Console.ReadLine());
                    ver[i].Add(x, y);
                }
                Console.WriteLine();
            }

            Console.Write("Введите начало: ");
            int start = Convert.ToInt32(Console.ReadLine());
            ver[start - 1].mass = 0;
            //qw.Enqueue(ver[start - 1]);
            CreateMass(ref ver[start - 1]);
            Algo();


            for (int i = 0; i < ver.Length; i++)
            {
                if (i+1 != start)
                    Console.WriteLine("Расстояние до вершины {0} = {1}", ver[i].number, ver[i].mass);
            }

            Console.ReadKey();
        }

        static public void Algo()
        {
            while (qw.Count > 0)
            {
                V v = qw.Dequeue();
                Console.WriteLine("Работаем с вершиной {0}",v.number);
                D(ref ver[v.number-1]);
            }
        }

        static public void D(ref V v)
        {
            
            for (int i = 0; i < v.sm.Count; i++)
            {
                Console.WriteLine("Мы в D");
                int num = v.sm[i].input;
                
                CreateMass(ref ver[num - 1]);
            }
        }

        static public void CreateMass(ref V v)
        {
            Console.WriteLine("делаем массы");

            int m = v.mass;
            for (int i = 0; i < v.sm.Count; i++)
            {
                int name = v.sm[i].input;
                if (ver[name - 1].enable)
                {
                    ver[name - 1].enable = false;
                    qw.Enqueue(ver[name - 1]);
                }
                Console.WriteLine("Проверяем вес вершины {0} , было {1} , предлагается {2}",
                    name, ver[name - 1].mass, m + v.sm[i].mass);

                if (m+v.sm[i].mass < ver[name - 1].mass)
                {
                    ver[name - 1].mass = m + v.sm[i].mass;
                    Console.WriteLine("Для вершины {0} назначили веса = {1}",name,ver[name-1].mass);
                }
            }
        }
    }
}
