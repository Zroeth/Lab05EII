using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba
{
    class Desencriptar
    {
        Config configurar = new Config();
        public string desencriptar(string llave1, string llave2, string codigo)
        {
            return SdesDesencriptar(llave1, llave2, codigo);
        }
        private string SdesDesencriptar(string llave1, string llave2, string codigo)
        {
            List<int> llave1L = configurar.stringAInt(llave1);
            List<int> llave2L = configurar.stringAInt(llave2);
            List<int> codigoL = configurar.stringAInt(codigo);

            // IP 
            List<int> IP = configurar.IP(codigoL);

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
            List<int> EP = configurar.EP(ladoder);

            // EP +Llave"1" =2
            List<int> EPK1 = configurar.EPpK1(EP, llave2L);

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
            // S0/S1
            List<int> S0 = configurar.S0Tabla(ladoizqXor);
            List<int> S1 = configurar.S1Tabla(ladoderXor);

            // +
            List<int> S0PS1 = configurar.XOR(S0, S1);

            // P4 
            List<int> P4 = configurar.P4(S0PS1);

            // SW
            List<int> SW = configurar.SW(ladoizq, P4);

            // EP2
            List<int> EP2 = configurar.EP(SW);

            // EP2 + Llave "2" = 1
            List<int> EPK2 = configurar.EPpK1(EP2, llave1L);

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
            List<int> S02 = configurar.S0Tabla(ladoizqXor2);
            List<int> S12 = configurar.S1Tabla(ladoderXor2);

            // +
            List<int> S0PS12 = configurar.XOR(S02, S12);

            // P4
            List<int> P42 = configurar.P4(S0PS12);

            // P4+LadoDer
            List<int> SW2 = configurar.SW(ladoder, P42);

            // + /4
            List<int> FS = configurar.XOR(SW2, SW);

            // IP-1
            List<int> IP1 = configurar.IP1(FS);

            return configurar.intAstring(IP1);
        }
    }
}

