using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clasificador
{
    public class Matriz
    {
        public float[,] matriz;
        public int filas, columnas;

        public Matriz()
        {
        }

        public Matriz(int fila, int columna)
        {
            filas = fila;
            columnas = columna;
            matriz = new float[filas, columnas];
        }

        public void DatoMatriz(float[,] matr){
            matriz = matr;
        }
        public void leer()
        {
            for (int i = 1; i <= filas; i++)
            {
                for (int j = 1; j <= columnas; j++)
                {
                    Console.WriteLine("Ingrese el valor en la posicion [" + i + ", " + j + "]");
                    matriz[i - 1, j - 1] = Convert.ToInt32(Console.ReadLine());
                }
            }
        }

        public override string ToString()
        {
            return imprime_matriz();
        }

        public static Matriz operator +(Matriz m1, Matriz m2)
        {
            int fm1 = m1.filas;
            int cm1 = m1.columnas;
            int fm2 = m2.filas;
            int cm2 = m2.columnas;
            Matriz aux = new Matriz(fm1, cm1);
            if ((fm1 == fm2) && (cm1 == cm2))
            {
                for (int i = 1; i <= aux.filas; i++)
                {
                    for (int j = 1; j <= aux.columnas; j++)
                    {
                        aux.matriz[i - 1, j - 1] = m1.matriz[i - 1, j - 1] + m2.matriz[i - 1, j - 1];
                    }
                }
            }
            return aux;
        }

        public static Matriz operator *(Matriz m1, Matriz m2)
        {
            int fm1 = m1.filas;
            int cm1 = m1.columnas;
            int fm2 = m2.filas;
            int cm2 = m2.columnas;
           // int cont, cont1;
            Matriz aux = new Matriz(fm1, cm2);
            if (cm1 == fm2)
            {
                int i, j, k;
                for (i = 0; i < aux.filas; i++)
                {
                   // cont1 = 0;
                    for (j = 0; j < aux.columnas; j++)
                    {
                        for (k = 0; k < cm1; k++)
                        {
                            aux.matriz[i, j] = aux.matriz[i, j] + (m1.matriz[i, k] * m2.matriz[k, j]);
                        }
                    }
                }
            }
            return aux;
        }

        public static Matriz operator *(int n, Matriz m1)
        {
            int fm1 = m1.filas;
            int cm1 = m1.columnas;
           // int cont, cont1;
            Matriz aux = new Matriz(fm1, cm1);
            int i, j; //k;
            for (i = 0; i < aux.filas; i++)
            {
                //cont1 = 0;
                for (j = 0; j < aux.columnas; j++)
                {
                    aux.matriz[i, j] = m1.matriz[i, j] * n;
                }
            }
            return aux;
        }

        public static Matriz operator -(Matriz m1, Matriz m2)
        {
            int fm1 = m1.filas;
            int cm1 = m1.columnas;
            int fm2 = m2.filas;
            int cm2 = m2.columnas;
            Matriz aux = new Matriz(fm1, cm1);
            if ((fm1 == fm2) && (cm1 == cm2))
            {
                for (int i = 1; i <= aux.filas; i++)
                {
                    for (int j = 1; j <= aux.columnas; j++)
                    {
                        aux.matriz[i - 1, j - 1] = m1.matriz[i - 1, j - 1] - m2.matriz[i - 1, j - 1];
                    }
                }
            }
            return aux;
        }

        public float determinante(Matriz m1)
        {
            int fm1 = m1.filas;
            int cm1 = m1.columnas;
            float aux = 1;
            float aux1 = 1, aux2 = 1, det = 0;
            for (int i = 0; i < m1.filas; i++)
            {
                for (int j = 0; j < m1.columnas; j++)
                {
                    if (i == j)
                    {
                        aux = m1.matriz[i, j];
                        aux1 = aux * aux1;
                    }
                    else
                    {
                        aux = m1.matriz[i, j];
                        aux2 = aux * aux2;
                    }
                }
            }
            det = aux1 - aux2;
            return det;
        }

        public Matriz Inversa(Matriz m1)
        {
            int fm1 = m1.filas;
            int cm1 = m1.columnas;
            Matriz aux = new Matriz(fm1, cm1);

            for (int i = 0; i < m1.filas; i++)
            {
                for (int j = 0; j < m1.columnas; j++)
                {
                    aux.matriz[i, j] = m1.matriz[i, j] / determinante(m1);
                }
            }
            return aux;
        }

        public Matriz Transpuesta(Matriz m1)
        {
            int fm1 = m1.filas;
            int cm1 = m1.columnas;
            Matriz aux = new Matriz(cm1, fm1);

            for (int i = 0; i < m1.filas; i++)
            {
                for (int j = 0; j < m1.columnas; j++)
                {
                    aux.matriz[j, i] = m1.matriz[i, j];
                }
            }
            return aux;
        }

        public string imprime_matriz()
        {
            string cad = "", cad1 = "", cad2 = "";
            for (int i = 1; i <= filas; i++)
            {
                for (int j = 1; j <= columnas; j++)
                {
                    cad = "" + matriz[i - 1, j - 1] + "\t";
                    cad1 = cad1 + cad;
                }
                cad1 = cad1 + "\n";
                cad2 = cad2 + cad1;
            }
            return cad1;
        }

    }
}
