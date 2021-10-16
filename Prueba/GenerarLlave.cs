using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prueba
{
    class GenerarLlave
    {
        readonly Config configuracion = new Config();
        public String GenerarLLaves(String llaveBinario)
        {

            List<int> p1=configuracion.stringAInt(llaveBinario);
            p1 = configuracion.P10(p1);
            
            string llave = string.Join("", p1.ToArray());
            string s1Llaveizq = llave.Substring(0, 5);
            string s1Llaveder = llave.Substring(5, 5);


            s1Llaveizq = shiftIzq(s1Llaveizq);
            //Console.WriteLine("Parte 1.2:" + s1Llaveizq);

            s1Llaveder = shiftIzq(s1Llaveder);


            string llave2 = this.generarLlave2(s1Llaveizq, s1Llaveder);
            string llave1izq = s1Llaveizq.ToString().PadLeft(5, '0');
            string llave1der = s1Llaveder.ToString().PadLeft(5, '0');

            string llave1 = llave1izq + llave1der;

            List<int> p2 = configuracion.stringAInt(llave1);
            p2 = configuracion.P8(p2);

            llave1 = string.Join("", p2.ToArray());

            return llave1 + llave2;
        }
        private String generarLlave2(String p1, String p2)
        {
            string s2Llaveizq = shiftIzq(p1);
            s2Llaveizq = shiftIzq(s2Llaveizq);

            string s2Llaveder = shiftIzq(p2);
            s2Llaveder = shiftIzq(s2Llaveder);

            string llave2 = s2Llaveizq + s2Llaveder;

            List<int> llavep2 = configuracion.stringAInt(llave2);
            llavep2 = configuracion.P8(llavep2); 


            llave2 = string.Join("", llavep2.ToArray());

            return llave2;
        }
        public static string shiftIzq(string linea)
        {

            return linea.Substring(1, linea.Length - 1) + linea.Substring(0, 1);
        }
        

    }
}
