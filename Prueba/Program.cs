using System;


namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escribir llave");
            string llave = (Console.ReadLine());

            string llave1 = "";
            string llave2 = "";


            if (esBinario(llave) && llave.Length == 10)
            {
                var generarLlave = new GenerarLlave();

                var Gllave = generarLlave.GenerarLLaves(llave);
                Console.WriteLine(Gllave.Substring(0, 8));
                Console.WriteLine(Gllave.Substring(8, 8));
                llave1 = Gllave.Substring(0, 8);
                llave2 = Gllave.Substring(8, 8);

            }

            Console.WriteLine("Escribir cosa a encriptar");
            string codigo = (Console.ReadLine());


            if (esBinario(codigo) && codigo.Length == 8)
            {
                var encriptar = new Encriptar();

                Console.WriteLine("Codigo: " + encriptar.encriptar(llave1, llave2, codigo));

            }

            Console.WriteLine("Escribir cosa a desencriptar");
            string codigoDes = (Console.ReadLine());


            if (esBinario(codigoDes) && codigoDes.Length == 8)
            {
                var desencriptar = new Desencriptar();

                Console.WriteLine("Codigo desencriptado: " + desencriptar.desencriptar(llave1, llave2, codigoDes));

            }


            Console.ReadKey();

        }
        static bool esBinario(string s)
        {
            foreach (var c in s)
            {
                if (c != '0' && c != '1')
                {
                    return false;
                }
            }
            return true;
        }
    }
    
}
