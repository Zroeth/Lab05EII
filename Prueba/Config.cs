using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Prueba
{
    class Config
    {
        public List<int> P10(List<int> cambiar)
        {
            List<int> orden = obTenerConfiguracion("Permutations.txt", 1).Split(',').Select(int.Parse).ToList();
            cambiar = orden.Select(i => cambiar[i]).ToList();
            return cambiar;
        }
        public List<int> P8(List<int> cambiar)
        {
            List<int> orden = obTenerConfiguracion("Permutations.txt", 2).Split(',').Select(int.Parse).ToList();
            cambiar = orden.Select(i => cambiar[i]).ToList();
            return cambiar;
        }
        public List<int> P4(List<int> cambiar)
        {
            List<int> orden = obTenerConfiguracion("Permutations.txt", 3).Split(',').Select(int.Parse).ToList();
            cambiar = orden.Select(i => cambiar[i]).ToList();
            return cambiar;
        }
        public List<int> EP(List<int> cambiar)
        {
            List<int> orden = obTenerConfiguracion("Permutations.txt", 4).Split(',').Select(int.Parse).ToList();
            cambiar = orden.Select(i => cambiar[i]).ToList();
            return cambiar;
        }
        public List<int> IP(List<int> cambiar)
        {
            List<int> orden = obTenerConfiguracion("Permutations.txt", 5).Split(',').Select(int.Parse).ToList();
            cambiar = orden.Select(i => cambiar[i]).ToList();
            return cambiar;
        }

        public List<int> IP1(List<int> cambiar)
        {
            List<int> orden = obTenerConfiguracion("Permutations.txt", 6).Split(',').Select(int.Parse).ToList();
            cambiar = orden.Select(i => cambiar[i]).ToList();
            return cambiar;
        }

        public List<int> EPpK1(List<int> EP, List<int> K1)
        {
            List<int> cambiar = new List<int>();
            for (int i = 0; i < K1.Count; i++)
            {
                int temp = 0;
                if (EP[i] == 0 && K1[i] == 1)
                {
                    temp = 1;
                }
                else if (EP[i] == 1 && K1[i] == 0)
                {
                    temp = 1;
                }
                else
                {
                    temp = 0;
                }
                cambiar.Add(temp);
            }
            return cambiar;
        }

        public List<int> S0Tabla(List<int> verificar)
        {
            List<int> cambiar = new List<int>();
            string[,] S0 = new string[,]
            {
                { "01", "00", "11", "10" },
                { "11", "10", "01", "00" },
                { "00", "10", "01", "11" },
                { "11", "01", "11", "10" }
            };

            int fila = 0;
            int columna = 0;
            List<int> listaF = new List<int>();
            listaF.Add(verificar[0]);
            listaF.Add(verificar[3]);
            string intString = intAstring(listaF);
            if (intString.Equals("00"))
            {
                fila = 0;
            }
            else if (intString.Equals("01"))
            {
                fila = 1;
            }
            else if (intString.Equals("10"))
            {
                fila = 2;
            }
            else
            {
                fila = 3;
            }
            List<int> listaC = new List<int>();
            listaC.Add(verificar[1]);
            listaC.Add(verificar[2]);
            string intStringC = intAstring(listaC);

            if (intStringC.Equals("00"))
            {
                columna = 0;
            }
            else if (intStringC.Equals("01"))
            {
                columna = 1;
            }
            else if (intStringC.Equals("10"))
            {
                columna = 2;
            }
            else
            {
                columna = 3;
            }

            string tabla = S0[fila, columna];
            cambiar = stringAInt(tabla);

            return cambiar;
        }

        public List<int> S1Tabla(List<int> verificar)
        {
            List<int> cambiar = new List<int>();
            string[,] S1 = new string[,]
            {
                { "00", "01", "10", "11" },
                { "10", "00", "01", "11" },
                { "11", "00", "01", "00" },
                { "10", "01", "00", "11" }
            };

            int fila = 0;
            int columna = 0;
            List<int> listaF = new List<int>();
            listaF.Add(verificar[0]);
            listaF.Add(verificar[3]);
            string intString = intAstring(listaF);
            if (intString.Equals("00"))
            {
                fila = 0;
            }
            else if (intString.Equals("01"))
            {
                fila = 1;
            }
            else if (intString.Equals("10"))
            {
                fila = 2;
            }
            else
            {
                fila = 3;
            }
            List<int> listaC = new List<int>();
            listaC.Add(verificar[1]);
            listaC.Add(verificar[2]);
            string intString2 = intAstring(listaC);

            if (intString2.Equals("00"))
            {
                columna = 0;
            }
            else if (intString2.Equals("01"))
            {
                columna = 1;
            }
            else if (intString2.Equals("10"))
            {
                columna = 2;
            }
            else
            {
                columna = 3;
            }
            var tabla = S1[fila, columna];
            cambiar = stringAInt(tabla);

            return cambiar;
        }

        public List<int> XOR(List<int> p1, List<int> p2)
        {
            List<int> cambiar = p1;
            cambiar.AddRange(p2);
            return cambiar;
        }

  

        public List<int> SW(List<int> leftInitList, List<int> outputPtwotList)
        {
            List<int> output = new List<int>();
            for (int i = 0; i < leftInitList.Count; i++)
            {
                var temp = 0;
                if (leftInitList[i] == 0 && outputPtwotList[i] == 1)
                    temp = 1;
                else if (leftInitList[i] == 1 && outputPtwotList[i] == 0)
                    temp = 1;
                else
                    temp = 0;

                output.Add(temp);
            }
            return output;
        }



        public List<int> stringAInt(string codigo)
        {
            List<int> salida = new List<int>();
            for (int i = 0; i < codigo.Length; i++)
            {
                salida.Add((int)Char.GetNumericValue(codigo[i]));
            }
            return salida;
        }

        public string intAstring(List<int> codigo)
        {
            StringBuilder temp = new StringBuilder();
            foreach (var i in codigo)
            {
                temp.Append(i);
            }
            return temp.ToString();
        }
        static string obTenerConfiguracion(string archivo, int linea)
        {
            string[] lines = File.ReadAllLines(archivo);

            return lines[linea - 1];
        }
    }
}

