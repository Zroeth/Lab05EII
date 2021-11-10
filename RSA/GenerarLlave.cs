using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RSA
{
    public class GenerarLlave
    {
        public List<string> generarLlave(int p, int q)
        {
            List<string> llave = new List<string>();
            int n = p * q;
            int phi = (p - 1) * (q - 1);
            int e = CalE(phi);
            int d = calD(phi, e);
            llave.Add(n + "," + e);
            llave.Add(n + "," + d);
            return llave;
        }

        int CalE(int phi)
        {
            int e = 2;
            int n = 2;

            while (phi % e == 0)
            {
                bool esPrimo = true;
                for (int i = 2; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        esPrimo = false;
                        break;
                    }
                }

                if (esPrimo)
                {
                    e = n;
                }

                n++;
            }

            return e;
        }

        int calD(int phi, int e)
        {
            int numAux = 0;
            int aux2 = 0;
            int aux3 = 0;
            int[,] intervalos = new int[2, 2];
            intervalos[0, 0] = phi;
            intervalos[0, 1] = phi;
            intervalos[1, 0] = e;
            intervalos[1, 1] = 1;

            while (intervalos[1, 0] != 1)
            {
                numAux = intervalos[0, 0] / intervalos[1, 0];
                aux2 = intervalos[0, 0];
                aux3 = intervalos[0, 1];
                intervalos[0, 0] = intervalos[1, 0];
                intervalos[0, 1] = intervalos[1, 1];
                intervalos[1, 0] = aux2 - (intervalos[1, 0] * numAux);
                intervalos[1, 1] = aux3 - (intervalos[1, 1] * numAux);

                if (intervalos[1, 1] < 0)
                {
                    int numero = intervalos[1, 1];
                    intervalos[1, 1] = (numero % phi + phi) % phi;
                }
            }

            return intervalos[1, 1];
        }
       
    }
  
}
