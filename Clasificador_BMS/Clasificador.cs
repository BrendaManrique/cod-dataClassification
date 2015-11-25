using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Clasificador;

namespace Clasificador_BMS
{
    public partial class Clasificador : Form
    {
        public float[,] plano = new float[3891, 14];

        public string infoFile;
        public float[,] aC1;
        public float[,] aC2;
        public float[,] aC3;
        public float[,] aC4;
        public float[,] aC5;
        public float[,] aC6;
        public float[,] aC7;

        public Matriz C1;
        public Matriz C2;
        public Matriz C3;
        public Matriz C4;
        public Matriz C5;
        public Matriz C6;
        public Matriz C7;

        public float[,] Media;
        public float[,] VariC1;
        public float[,] VariC2;
        public float[,] VariC3;
        public float[,] VariC4;
        public float[,] VariC5;
        public float[,] VariC6;
        public float[,] VariC7;


        public float[,] G;


        public Clasificador()
        {
            InitializeComponent();
            aC1 = new float[636, 13];
            aC2 = new float[321, 13];
            aC3 = new float[418, 13];
            aC4 = new float[757, 13];
            aC5 = new float[747, 13];
            aC6 = new float[608, 13];
            aC7 = new float[404, 13];


            C1 = new Matriz(636, 13);
            C2 = new Matriz(321, 13);
            C3 = new Matriz(418, 13);
            C4 = new Matriz(757, 13);
            C5 = new Matriz(747, 13);
            C6 = new Matriz(608, 13);
            C7 = new Matriz(404, 13);

            Media = new float[7, 13];

            VariC1 = new float[13, 13];
            VariC2 = new float[13, 13];
            VariC3 = new float[13, 13];
            VariC4 = new float[13, 13];
            VariC5 = new float[13, 13];
            VariC6 = new float[13, 13];
            VariC7 = new float[13, 13];

            G = new float[7, 13];
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CoorPlano();
            MatrizClases();
            btnCargar.Enabled = true;
            CalcMedia();
            CalcCovarianza(C1, 0, 636);
            CalcCovarianza(C2, 1, 321);
            CalcCovarianza(C3, 2, 418);
            CalcCovarianza(C4, 3, 757);
            CalcCovarianza(C5, 4, 747);
            CalcCovarianza(C6, 5, 608);
            CalcCovarianza(C7, 6, 404);

            modCov();

            CalcG(C1, 0, 636);
            Clases(aC1, aC2, aC3, aC4, aC5, aC6, aC7);
            this.groupBox2.Enabled = true;
            this.comboBox1.SelectedIndex = 0;

        }

        public void CoorPlano()
        {
            String linea;

            this.openFileDialog1.ShowDialog();
            StreamReader sr = new StreamReader(this.openFileDialog1.FileName);
            int f = 0;
            int c = 0;
            int k = 0;
            string s_num = "";
            while ((linea = sr.ReadLine()) != null)
            {
                for (int i = 0; i < 13; i++)
                {
                    while (linea[k] != ' ')
                    {
                        s_num += linea[k];
                        k++;
                    }
                    k = k + 2;
                    s_num = s_num.Replace(".", ",");
                    plano[f, c] = float.Parse(s_num);
                    c++;

                    s_num = Convert.ToString(linea[k]);
                    plano[f, 13] = float.Parse(s_num);

                    s_num = "";
                }
                s_num = "";
                k = 0;
                f++;
                c = 0;
            }
            sr.Close();
        }

        public void MatrizClases()
        {
            int clase = 0;
            int ctC1 = 0;
            int ctC2 = 0;
            int ctC3 = 0;
            int ctC4 = 0;
            int ctC5 = 0;
            int ctC6 = 0;
            int ctC7 = 0;
            for (int f = 0; f < 3891; f++)
            {
                clase = Convert.ToInt32(plano[f, 13]);
                switch (clase)
                {
                    case 1:
                        for (int c = 0; c < 13; c++)
                        {
                            aC1[ctC1, c] = plano[f, c];
                        }
                        ctC1++;
                        break;
                    case 2:
                        for (int c = 0; c < 13; c++)
                        {
                            aC2[ctC2, c] = plano[f, c];
                        }
                        ctC2++;
                        break;
                    case 3:
                        for (int c = 0; c < 13; c++)
                        {
                            aC3[ctC3, c] = plano[f, c];
                        }
                        ctC3++;
                        break;
                    case 4:
                        for (int c = 0; c < 13; c++)
                        {
                            aC4[ctC4, c] = plano[f, c];
                        }
                        ctC4++;
                        break;
                    case 5:
                        for (int c = 0; c < 13; c++)
                        {
                            aC5[ctC5, c] = plano[f, c];
                        }
                        ctC5++;
                        break;
                    case 6:
                        for (int c = 0; c < 13; c++)
                        {
                            aC6[ctC6, c] = plano[f, c];
                        }
                        ctC6++;
                        break;
                    case 7:
                        for (int c = 0; c < 13; c++)
                        {
                            aC7[ctC7, c] = plano[f, c];
                        }
                        ctC7++;
                        break;
                    default:
                        break;

                }
            }
            C1.DatoMatriz(aC1);
            C2.DatoMatriz(aC2);
            C3.DatoMatriz(aC3);
            C4.DatoMatriz(aC4);
            C5.DatoMatriz(aC5);
            C6.DatoMatriz(aC6);
            C7.DatoMatriz(aC7);
        }/*
        public void llenarGrid2(float[,] mat, int tam){
            DataTable table = new DataTable("Tabla1");
            table.Columns.Add(new DataColumn("N", typeof(string)));
            for (int i = 0; i < 13; i++){
                table.Columns.Add(new DataColumn("D" + i, typeof(string)));
            }

            DataRow[] row = new DataRow[tam];

            for (int f = 0; f < tam-15; f++){
                row[f] = table.NewRow();
                row[f]["N"] = f.ToString();
                for (int c = 0; c < 13; c++){
                    row[f]["D" + c] = mat[f, c].ToString();
                }
                table.Rows.Add(row[f]);
            }
            
            gridC8.DataSource = table;
        }*/
        public void CalcMedia()
        {
            txtPrueba.Text = "Calcular Media: \r\n";
            float suma = 0;
            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 636; f++)
                {
                    suma += C1.matriz[f, c];
                }
                suma = suma / 636;
                Media[0, c] = suma;
                suma = 0;
                //txtPrueba.Text += "Media [0,"+c+"]:"+Media[0, c]+"="+suma+"\r";
                txtPrueba.Text += "Media [0," + c + "]=" + suma + "\r\n";
            }
            suma = 0;

            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 321; f++)
                {
                    suma += C2.matriz[f, c];
                }
                suma = suma / 636;
                Media[0, c] = suma; txtPrueba.Text += "Media [1," + c + "]=" + suma + "\r\n";
                suma = 0;
                
            }
            suma = 0;

            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 418; f++)
                {
                    suma += C3.matriz[f, c];
                }
                suma = suma / 636;
                Media[0, c] = suma; txtPrueba.Text += "Media [2," + c + "]=" + suma + "\r\n";
                suma = 0;
              
            }
            suma = 0;

            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 757; f++)
                {
                    suma += C4.matriz[f, c];
                }
                suma = suma / 636;
                Media[0, c] = suma; txtPrueba.Text += "Media [3," + c + "]=" + suma + "\r\n";
                suma = 0;
                
            }
            suma = 0;

            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 747; f++)
                {
                    suma += C5.matriz[f, c];
                }
                suma = suma / 636;
                Media[0, c] = suma; txtPrueba.Text += "Media [4," + c + "]=" + suma + "\r\n";
                suma = 0;
            }
            suma = 0;

            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 608; f++)
                {
                    suma += C6.matriz[f, c];
                }
                suma = suma / 636;
                Media[0, c] = suma; txtPrueba.Text += "Media [5," + c + "]=" + suma + "\r\n";
                suma = 0;
            }
            suma = 0;

            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 404; f++)
                {
                    suma += C7.matriz[f, c];
                }
                suma = suma / 636;
                Media[0, c] = suma; txtPrueba.Text += "Media [6," + c + "]=" + suma + "\r\n";
                suma = 0;
            }
            suma = 0;
        }

        public void CalcCovarianza(Matriz mat, int posMed, int tam)
        {
            float suma = 0;

            for (int a = 0; a < 13; a++)
            {
                for (int b = 0; b <= a; b++)
                {
                    for (int f = 0; f < tam; f++)
                    {
                        suma += mat.matriz[f, a] - mat.matriz[f, b];
                    }
                    suma = suma / tam;

                    switch (tam)
                    {
                        case 636: VariC1[a, b] = suma;
                            break;
                        case 321: VariC2[a, b] = suma;
                            break;
                        case 418: VariC3[a, b] = suma;
                            break;
                        case 757: VariC4[a, b] = suma;
                            break;
                        case 747: VariC5[a, b] = suma;
                            break;
                        case 608: VariC6[a, b] = suma;
                            break;
                        case 404: VariC7[a, b] = suma;
                            break;
                    }
                    suma = 0;
                }
            }
            suma = 0;
        }

        public void modCov()
        {
            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 13; f++)
                {
                    VariC1[c, f] = VariC1[f, c];
                }
            }
            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 13; f++)
                {
                    VariC2[c, f] = VariC2[f, c];
                }
            }
            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 13; f++)
                {
                    VariC3[c, f] = VariC3[f, c];
                }
            }
            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 13; f++)
                {
                    VariC4[c, f] = VariC4[f, c];
                }
            }
            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 13; f++)
                {
                    VariC5[c, f] = VariC5[f, c];
                }
            }
            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 13; f++)
                {
                    VariC6[c, f] = VariC6[f, c];
                }
            }
            for (int c = 0; c < 13; c++)
            {
                for (int f = 0; f < 13; f++)
                {
                    VariC7[c, f] = VariC7[f, c];
                }
            }

        }

        public void CalcG(Matriz mat, int posMed, int tam)
        {
            double gx = 0;
            Matriz x_med = Restar(C1, 0, 636);
            Matriz covC1 = new Matriz(13, 13);
            Matriz covC2 = new Matriz(13, 13);
            Matriz covC3 = new Matriz(13, 13);
            Matriz covC4 = new Matriz(13, 13);
            Matriz covC5 = new Matriz(13, 13);
            Matriz covC6 = new Matriz(13, 13);
            Matriz covC7 = new Matriz(13, 13);

            covC1.DatoMatriz(VariC1);
            covC2.DatoMatriz(VariC2);
            covC3.DatoMatriz(VariC3);
            covC4.DatoMatriz(VariC4);
            covC5.DatoMatriz(VariC5);
            covC6.DatoMatriz(VariC6);
            covC7.DatoMatriz(VariC7);

            Matriz auxx;
            auxx = x_med * covC1.Inversa(covC1);
            auxx = auxx * x_med.Transpuesta(x_med);
            int mult = (int)auxx.matriz[0, 0];


            gx = -(0.5) * mult - ((7 / 2) * Math.Log(2 * 3.141592)) - (0.5 * Math.Log(covC1.determinante(covC1)));

        }

        public float[,] Transpues(float[,] mat, int tam)
        {
            float[,] NewMat = new float[tam, 13];
            for (int f = 0; f < tam; f++)
            {
                for (int c = 0; c < 13; c++)
                {
                    NewMat[f, c] = mat[c, f];
                }
            }
            return NewMat;
        }

        public Matriz Restar(Matriz aux, int valmed, int tam)
        {
            Matriz rest = new Matriz(1, 13);

            for (int c = 0; c < 13; c++)
            {
                rest.matriz[valmed, c] = aux.matriz[valmed, c] - Media[valmed, c];
            }
            return rest;

        }
        //float[,] MatCla;

        private void button1_Click(object sender, EventArgs e)
        {
            Clases(aC1, aC2, aC3, aC4, aC5, aC6, aC7);
            //cl.Show();
        }

        private void btnClasificar_Click(object sender, EventArgs e)
        {
            Random r;
            r = new Random();
            int al = 0;
            DataTable table = new DataTable("Tabla");
            for (int i = 0; i < 14; i++)
            {
                table.Columns.Add(new DataColumn("Dato (" + i + ")", typeof(string)));
            }
            table.Columns.Add(new DataColumn("Clase", typeof(string)));
            DataRow[] row = new DataRow[3891];
            //int cont = 0;
            for (int f = 0; f < 3891; f++)
            {
                row[f] = table.NewRow();
                for (int c = 0; c < 14; c++)
                {
                    row[f]["Dato (" + c + ")"] = plano[f, c].ToString();
                }
                row[f]["Clase"] = plano[f, 13].ToString();

                table.Rows.Add(row[f]);
            }
            for (int f = 0; f < 50; f++)
            {
                al = r.Next(0, 3891);
                row[al]["Clase"] = "1";
                al = r.Next(0, 3891);
                row[al]["Clase"] = "2";
                al = r.Next(0, 3891);
                row[al]["Clase"] = "3";
                al = r.Next(0, 3891);
                row[al]["Clase"] = "4";
                al = r.Next(0, 3891);
                row[al]["Clase"] = "5";
                al = r.Next(0, 3891);
                row[al]["Clase"] = "6";
                al = r.Next(0, 3891);
                row[al]["Clase"] = "7";

            }
            gridC4.DataSource = table;
        }
        public float[,] C_1;
        public float[,] C_2;
        public float[,] C_3;
        public float[,] C_4;
        public float[,] C_5;
        public float[,] C_6;
        public float[,] C_7;
        public void Clases(float[,] Cl1, float[,] Cl2, float[,] Cl3, float[,] Cl4, float[,] Cl5, float[,] Cl6, float[,] Cl7)
        {
            
            C_1 = Cl1;llenarGrid(C_1, 636); 
            C_2 = Cl2;
            C_3 = Cl3;
            C_4 = Cl4;
            C_5 = Cl5;
            C_6 = Cl6;
            C_7 = Cl7;
        }

        private void Clases_Load(object sender, EventArgs e)
        {


        }
        public void llenarGrid(float[,] mat, int tam)
        {
            DataTable table = new DataTable("Tabla1");
            for (int i = 0; i < 13; i++)
            {
                table.Columns.Add(new DataColumn("Dato (" + i + ")", typeof(string)));
            }

            DataRow[] row = new DataRow[tam];

            for (int f = 0; f < tam; f++)
            {
                row[f] = table.NewRow();
                for (int c = 0; c < 13; c++)
                {
                    row[f]["Dato (" + c + ")"] = mat[f, c].ToString();
                }
                table.Rows.Add(row[f]);
            }

            gridC4.DataSource = table;

        }


        private void gridC3_Navigate(object sender, NavigateEventArgs ne)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = comboBox1.SelectedIndex;
            switch (select)
            {
                case 0: llenarGrid(C_1, 636);
                    break;
                case 1: llenarGrid(C_2, 321);
                    break;
                case 2: llenarGrid(C_3, 418);
                    break;
                case 3: llenarGrid(C_4, 757);
                    break;
                case 4: llenarGrid(C_5, 747);
                    break;
                case 5: llenarGrid(C_6, 608);
                    break;
                case 6: llenarGrid(C_7, 404);
                    break;
                default: break;
            }
        }
    }
}
