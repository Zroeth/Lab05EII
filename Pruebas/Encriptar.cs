using System;
using System.Collections.Generic;
using System.Text;

namespace Pruebas
{
    class Encriptar
    {
        readonly Config configuracion = new Config();
        public string encriptar(string llave1, string llave2, string codigo)
        {
            return SdesEncriptar(llave1, llave2, codigo);
        }

        private string SdesEncriptar(string llave1, string llave2, string codigo)
        {
            List<int> llave1L = configuracion.stringAInt(llave1);
            List<int> llave2L = configuracion.stringAInt(llave2);
            List<int> codigoL = configuracion.stringAInt(codigo);

            // IP 
            List<int> IP = configuracion.IP(codigoL);

            // /4
            List<int> ladoizq = new List<int>();
            List<int> ladoder = new List<int>();
            for (int i = 0; i < codigoL.Count; i++)
            {
                var temp = IP[i];
                if (i < 4)
                {
                    ladoizq.Add(temp);
                }
                else
                {
                    ladoder.Add(temp);
                }
            }

            //  EP
            List<int> EP = configuracion.EP(ladoder);

            // EP + Llave1
            List<int> EPK1 = configuracion.EPpK1(EP, llave1L);

            // + /4
            List<int> ladoizqXor = new List<int>();
            List<int> ladoderXor = new List<int>();
            for (int i = 0; i < EPK1.Count; i++)
            {
                var temp = EPK1[i];
                if (i < 4)
                {
                    ladoizqXor.Add(temp);
                }
                else
                {
                    ladoderXor.Add(temp);
                }
            }

            // S0 / S1
            List<int> S0 = configuracion.S0Tabla(ladoizqXor);
            List<int> S1 = configuracion.S1Tabla(ladoderXor);

            // + 
            List<int> S0PS1 = configuracion.XOR(S0, S1);

            // P4
            List<int> P4 = configuracion.P4(S0PS1);

            // SW
            List<int> SW = configuracion.SW(ladoizq, P4);

            // EP2
            List<int> EP2 = configuracion.EP(SW);

            // EP2 + Llave2
            List<int> EPK2 = configuracion.EPpK1(EP2, llave2L);

            // +/4
            List<int> ladoizqXor2 = new List<int>();
            List<int> ladoderXor2 = new List<int>();
            for (int i = 0; i < EPK2.Count; i++)
            {
                var temp = EPK2[i];
                if (i < 4)
                {
                    ladoizqXor2.Add(temp);
                }
                else
                {
                    ladoderXor2.Add(temp);
                }
            }

            // S0 / S1
            List<int> S02 = configuracion.S0Tabla(ladoizqXor2);
            List<int> S12 = configuracion.S1Tabla(ladoderXor2);

            // +
            List<int> S0PS12 = configuracion.XOR(S02, S12);

            // P4
            List<int> P42 = configuracion.P4(S0PS12);

            //  P4+LadoDer
            List<int> SW2 = configuracion.SW(ladoder, P42);

            // + /4
            List<int> FS = configuracion.XOR(SW2, SW);

            // IP-1
            List<int> IP1 = configuracion.IP1(FS);

            return configuracion.intAstring(IP1);
        }
    }
}
