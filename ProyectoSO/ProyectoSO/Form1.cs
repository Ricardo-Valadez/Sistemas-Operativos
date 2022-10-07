using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace ProyectoSO
{

    public partial class Form1 : Form
    {
        //Varibles Locales
        public int Contador, Quantum, QuantumUpdate, TiempoEjecucion, MemoriaMax = 1000, Memoria = 0;
        public bool Bandera;

        //Ejecucion al momento de abri el forms
        public Form1()
        {
            InitializeComponent();

            //HERRAMIENTAS QUE NO QUEREMOS QUE SEAN ACCECIBLES AL INICIO DEL PROGRAMA
            btn_iniciat.Enabled = false;
            rdb_disco.Enabled = false;
            rdb_impresora.Enabled = false;
            rdb_teclado.Enabled = false;
            txb_tiempo_dispositvo.Enabled = false;
        }

        //Al momento de ingresar los valores
        private void btn_acepta_Click(object sender, EventArgs e)
        {

            btn_iniciat.Enabled = true;

            if (txb_nombre.Text == "" || txb_tiempo.Text == "" || txb_memoria.Text == "")
            {
                MessageBox.Show("Por favor ingrese los datos necesarios");
            }
            else
            {
                DataGridViewRow valores = new DataGridViewRow();
                valores.CreateCells(dgv_proceso);
                valores.Cells[0].Value = txb_nombre.Text;
                valores.Cells[1].Value = txb_tiempo.Text;
                valores.Cells[2].Value = "Nuevo";
                valores.Cells[6].Value = txb_memoria.Text;

                if (ckb_dispositivo.Checked)
                {
                    valores.Cells[3].Value = "TRUE";

                    if (rdb_disco.Checked)
                    {
                        valores.Cells[4].Value = "Disco";
                        valores.Cells[5].Value = txb_tiempo_dispositvo.Text;
                    }
                    if (rdb_impresora.Checked)
                    {
                        valores.Cells[4].Value = "Impresora";
                        valores.Cells[5].Value = txb_tiempo_dispositvo.Text;
                    }
                    if (rdb_teclado.Checked)
                    {
                        valores.Cells[4].Value = "Teclado";
                        valores.Cells[5].Value = txb_tiempo_dispositvo.Text;
                    }
                }

                dgv_proceso.Rows.Add(valores);
            }
        }

        private void ckb_dispositivo_CheckedChanged(object sender, EventArgs e)
        {
            //SE PUEDA ACCESAR CUANDO SE REQUIERE UNA LLAMADA A UN DISPOSITICO
            if (ckb_dispositivo.Checked == true)
            {
                rdb_disco.Enabled = true;
                rdb_impresora.Enabled = true;
                rdb_teclado.Enabled = true;
                txb_tiempo_dispositvo.Enabled = true;
            }
        }

        private void ckb_dispositivo_CheckStateChanged(object sender, EventArgs e)
        {
            if (ckb_dispositivo.Checked == false)
            {
                //PARA QUE SE DESACTIVE LAS HERRAMIENTAS DE ENTRADA Y SALIDA
                rdb_disco.Enabled = false;
                rdb_impresora.Enabled = false;
                rdb_teclado.Enabled = false;
                txb_tiempo_dispositvo.Enabled = false;
            }
        }

        private void btn_iniciat_Click(object sender, EventArgs e)
        {
            //AL MOMENTO DE DARLE AL BOTON DE INICIAR, COMIENZA EL TIMER 
            timer1.Enabled = true;
            Quantum = Int32.Parse(cmb_quantum.Text);
            cmb_quantum.Enabled = false;
            timer1.Start();
        }


        //METODO PARA EMPEZAR EL PROGRAMA DESDE SU PRIMERA INSTANCIA
        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //METODO PARA LIMPIAR LA CASILLAS 
        private void btn_Limpiar_Click(object sender, EventArgs e)
        {
            txb_nombre.Text = "";
            txb_tiempo.Text = "";
            txb_memoria.Text = "";


            if (dgv_ejecucion.Rows.Count > 1 && dgv_ejecucion.Rows != null)
            {
                lbl_ejecucion.Text = "Se terminaron los procesos";
            }
        }

        //MEDIANTE UN TIMER SE ELABORA EL SIGUIENTE METODO PARA INGRESAR DATOS 
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Contador dentro del Forms
            Contador = Int32.Parse(label2.Text);
            Contador++;
            label2.Text = Convert.ToString(Contador);

            //Cuentas especificas de los valores dentro de los DGV
            int EstadoNuevo = dgv_proceso.Rows.Count;  //cantidad de cosas dentro del tabla
            int EstadoListo = dgv_espera.Rows.Count;
            int EstadoEjecucion = dgv_ejecucion.Rows.Count;

            if (Memoria + Convert.ToInt32(dgv_proceso.Rows[0].Cells[6].Value) <= 1000)
            {
                Memoria = Memoria + Convert.ToInt32(dgv_proceso.Rows[0].Cells[6].Value);
                label7.Text = Convert.ToString(Memoria);

                //Cuando cuente mas de un valor dentro del DGV comenzara el proceso
                if (EstadoNuevo > 1)
                {
                    //Se crea un objeto DGV para poder mandar toda la fila al otro DGV
                    DataGridViewRow valores = new DataGridViewRow();
                    valores.CreateCells(dgv_espera);
                    valores.Cells[0].Value = dgv_proceso.Rows[0].Cells[0].Value;
                    valores.Cells[1].Value = dgv_proceso.Rows[0].Cells[1].Value;           //tabla de espera
                    valores.Cells[2].Value = dgv_proceso.Rows[0].Cells[2].Value;
                    valores.Cells[3].Value = dgv_proceso.Rows[0].Cells[3].Value;
                    valores.Cells[4].Value = dgv_proceso.Rows[0].Cells[4].Value;
                    valores.Cells[5].Value = dgv_proceso.Rows[0].Cells[5].Value;
                    valores.Cells[6].Value = dgv_proceso.Rows[0].Cells[6].Value;

                    DataGridViewRow valores2 = new DataGridViewRow();
                    valores2.CreateCells(dgv_memoria);
                    valores2.Cells[0].Value = dgv_proceso.Rows[0].Cells[0].Value;
                    valores2.Cells[1].Value = dgv_proceso.Rows[0].Cells[1].Value;           //tabla de espera
                    valores2.Cells[2].Value = dgv_proceso.Rows[0].Cells[2].Value;
                    valores2.Cells[3].Value = dgv_proceso.Rows[0].Cells[3].Value;
                    valores2.Cells[4].Value = dgv_proceso.Rows[0].Cells[4].Value;
                    valores2.Cells[5].Value = dgv_proceso.Rows[0].Cells[5].Value;
                    valores2.Cells[6].Value = dgv_proceso.Rows[0].Cells[6].Value;

                    //Se agregan los valores de DGV PROCESOS al DGV LISTOS
                    dgv_proceso.Rows.Remove(dgv_proceso.Rows[0]);
                    dgv_espera.Rows.Add(valores);
                    dgv_memoria.Rows.Add(valores2);

                }
            }
            //Cuando existan valores dentro de los dos DGV se ejecutara las siguientes instrucciones
            if (EstadoListo > 1 && EstadoEjecucion == 1)
            {
                //Ejemplificar el metodo actual en ejecucion
                string ProcesoEJEC = Convert.ToString(dgv_espera.Rows[0].Cells[0].Value);

                if (dgv_ejecucion.Rows.Count != 0)
                {
                    lbl_ejecucion.Text = Convert.ToString(dgv_espera.Rows[0].Cells[0].Value);
                    timer1.Stop();
                    MessageBox.Show("El proceso " + ProcesoEJEC + " entro al estado de ejecución");
                    timer1.Start();
                    dgv_ejecucion.Refresh();
                }


                //La lineas son eliminados en el DGV Listos y se tranfieren al DGV Ejecucion
                DataGridViewRow valores = new DataGridViewRow();
                valores.CreateCells(dgv_espera);
                valores.Cells[0].Value = dgv_espera.Rows[0].Cells[0].Value;
                valores.Cells[1].Value = dgv_espera.Rows[0].Cells[1].Value;
                valores.Cells[2].Value = dgv_espera.Rows[0].Cells[2].Value;
                valores.Cells[3].Value = dgv_espera.Rows[0].Cells[3].Value;
                valores.Cells[4].Value = dgv_espera.Rows[0].Cells[4].Value;
                valores.Cells[5].Value = dgv_espera.Rows[0].Cells[5].Value;
                valores.Cells[6].Value = dgv_espera.Rows[0].Cells[6].Value;

                dgv_espera.Rows.Remove(dgv_espera.Rows[0]);
                dgv_ejecucion.Rows.Add(valores);

                Bandera = true;
            }

            if (EstadoEjecucion == 2)
            {
                if (Bandera == true)
                {
                    TiempoEjecucion = Convert.ToInt32(dgv_ejecucion.Rows[0].Cells[1].Value);
                    QuantumUpdate = Quantum;
                    Bandera = false;
                }


                if (TiempoEjecucion > 0 && QuantumUpdate > 0)
                {
                    if (dgv_ejecucion.Rows[0].Cells[4].Value != null)
                    {
                        int Bloqueo = Convert.ToInt32(dgv_ejecucion.Rows[0].Cells[5].Value);

                        if (TiempoEjecucion == Bloqueo)
                        {
                            if (dgv_ejecucion.Rows[0].Cells[4].Value == "Disco")
                            {
                                //manda los valores a espera y de ahi los deja para ver cuales fueron los procesos que llegaron a espera y luego regresan a listo 
                                DataGridViewRow valores2 = new DataGridViewRow();
                                valores2.CreateCells(dgv_bloqueado_disco);
                                valores2.Cells[0].Value = dgv_ejecucion.Rows[0].Cells[0].Value;
                                valores2.Cells[1].Value = dgv_ejecucion.Rows[0].Cells[1].Value;
                                valores2.Cells[2].Value = dgv_ejecucion.Rows[0].Cells[2].Value;
                                valores2.Cells[3].Value = dgv_ejecucion.Rows[0].Cells[3].Value;
                                valores2.Cells[4].Value = dgv_ejecucion.Rows[0].Cells[4].Value;
                                valores2.Cells[5].Value = dgv_ejecucion.Rows[0].Cells[5].Value;
                                valores2.Cells[6].Value = dgv_ejecucion.Rows[0].Cells[6].Value;


                                DataGridViewRow valores = new DataGridViewRow();
                                valores.CreateCells(dgv_bloqueado_disco);
                                valores.Cells[0].Value = dgv_ejecucion.Rows[0].Cells[0].Value;
                                valores.Cells[1].Value = dgv_ejecucion.Rows[0].Cells[1].Value;
                                valores.Cells[2].Value = dgv_ejecucion.Rows[0].Cells[2].Value;
                                valores.Cells[3].Value = dgv_ejecucion.Rows[0].Cells[3].Value;
                                valores.Cells[4].Value = dgv_ejecucion.Rows[0].Cells[4].Value;
                                valores.Cells[6].Value = dgv_ejecucion.Rows[0].Cells[6].Value;

                                dgv_ejecucion.Rows.Remove(dgv_ejecucion.Rows[0]);
                                dgv_bloqueado_disco.Rows.Add(valores2);
                                dgv_espera.Rows.Add(valores);

                            }


                            if (dgv_ejecucion.Rows[0].Cells[4].Value == "Impresora")
                            {
                                //manda los valores a espera y de ahi los deja para ver cuales fueron los procesos que llegaron a espera y luego regresan a listo 
                                DataGridViewRow valores2 = new DataGridViewRow();
                                valores2.CreateCells(dgv_bloqueado_impresora);
                                valores2.Cells[0].Value = dgv_ejecucion.Rows[0].Cells[0].Value;
                                valores2.Cells[1].Value = dgv_ejecucion.Rows[0].Cells[1].Value;
                                valores2.Cells[2].Value = dgv_ejecucion.Rows[0].Cells[2].Value;
                                valores2.Cells[3].Value = dgv_ejecucion.Rows[0].Cells[3].Value;
                                valores2.Cells[4].Value = dgv_ejecucion.Rows[0].Cells[4].Value;
                                valores2.Cells[5].Value = dgv_ejecucion.Rows[0].Cells[5].Value;
                                valores2.Cells[6].Value = dgv_ejecucion.Rows[0].Cells[6].Value;


                                DataGridViewRow valores = new DataGridViewRow();
                                valores.CreateCells(dgv_bloqueado_disco);
                                valores.Cells[0].Value = dgv_ejecucion.Rows[0].Cells[0].Value;
                                valores.Cells[1].Value = dgv_ejecucion.Rows[0].Cells[1].Value;
                                valores.Cells[2].Value = dgv_ejecucion.Rows[0].Cells[2].Value;
                                valores.Cells[3].Value = dgv_ejecucion.Rows[0].Cells[3].Value;
                                valores.Cells[4].Value = dgv_ejecucion.Rows[0].Cells[4].Value;
                                valores.Cells[6].Value = dgv_ejecucion.Rows[0].Cells[6].Value;

                                dgv_ejecucion.Rows.Remove(dgv_ejecucion.Rows[0]);
                                dgv_bloqueado_impresora.Rows.Add(valores2);
                                dgv_espera.Rows.Add(valores);

                            }

                            if (dgv_ejecucion.Rows[0].Cells[4].Value == "Teclado")
                            {
                                //manda los valores a espera y de ahi los deja para ver cuales fueron los procesos que llegaron a espera y luego regresan a listo 
                                DataGridViewRow valores2 = new DataGridViewRow();
                                valores2.CreateCells(dgv_bloqueado_teclado);
                                valores2.Cells[0].Value = dgv_ejecucion.Rows[0].Cells[0].Value;
                                valores2.Cells[1].Value = dgv_ejecucion.Rows[0].Cells[1].Value;
                                valores2.Cells[2].Value = dgv_ejecucion.Rows[0].Cells[2].Value;
                                valores2.Cells[3].Value = dgv_ejecucion.Rows[0].Cells[3].Value;
                                valores2.Cells[4].Value = dgv_ejecucion.Rows[0].Cells[4].Value;
                                valores2.Cells[5].Value = dgv_ejecucion.Rows[0].Cells[5].Value;
                                valores2.Cells[6].Value = dgv_ejecucion.Rows[0].Cells[6].Value;

                                DataGridViewRow valores = new DataGridViewRow();
                                valores.CreateCells(dgv_bloqueado_disco);
                                valores.Cells[0].Value = dgv_ejecucion.Rows[0].Cells[0].Value;
                                valores.Cells[1].Value = dgv_ejecucion.Rows[0].Cells[1].Value;
                                valores.Cells[2].Value = dgv_ejecucion.Rows[0].Cells[2].Value;
                                valores.Cells[3].Value = dgv_ejecucion.Rows[0].Cells[3].Value;
                                valores.Cells[4].Value = dgv_ejecucion.Rows[0].Cells[4].Value;
                                valores.Cells[6].Value = dgv_ejecucion.Rows[0].Cells[6].Value;

                                dgv_ejecucion.Rows.Remove(dgv_ejecucion.Rows[0]);
                                dgv_bloqueado_teclado.Rows.Add(valores2);
                                dgv_espera.Rows.Add(valores);
                            }

                        }
                    }

                    //El TIEMPO DE EJECUCION SE REDUCE MENOS UNO POR CADA TICK HASTA LLEGAR A 0
                    TiempoEjecucion--;
                    QuantumUpdate--;
                    dgv_ejecucion.Rows[0].Cells[1].Value = Convert.ToString(TiempoEjecucion);
                }
                else if (TiempoEjecucion == 0)
                {
                    //se crea una fila para mandar a guardar los procesos terminados 
                    DataGridViewRow valores = new DataGridViewRow();
                    valores.CreateCells(dgv_ejecucion);
                    valores.Cells[0].Value = dgv_ejecucion.Rows[0].Cells[0].Value;
                    valores.Cells[1].Value = dgv_ejecucion.Rows[0].Cells[1].Value;
                    valores.Cells[2].Value = dgv_ejecucion.Rows[0].Cells[2].Value;
                    valores.Cells[3].Value = dgv_ejecucion.Rows[0].Cells[3].Value;
                    valores.Cells[4].Value = dgv_ejecucion.Rows[0].Cells[4].Value;
                    valores.Cells[5].Value = dgv_ejecucion.Rows[0].Cells[5].Value;
                    valores.Cells[6].Value = dgv_ejecucion.Rows[0].Cells[6].Value;

                    //Para que se actualice y se termine de utilizar la memoria
                    Memoria = Memoria - Convert.ToInt32(dgv_ejecucion.Rows[0].Cells[6].Value);
                    label7.Text = Convert.ToString(Memoria);

                    dgv_ejecucion.Rows.Remove(dgv_ejecucion.Rows[0]);
                    dgv_memoria.Rows.Remove(dgv_memoria.Rows[0]);
                    dgv_terminado.Rows.Add(valores);
                }
                else
                {
                    //se crea una fila para mandar los procesos al datagrid de listos 
                    DataGridViewRow valores = new DataGridViewRow();
                    valores.CreateCells(dgv_ejecucion);
                    valores.Cells[0].Value = dgv_ejecucion.Rows[0].Cells[0].Value;
                    valores.Cells[1].Value = dgv_ejecucion.Rows[0].Cells[1].Value;
                    valores.Cells[2].Value = dgv_ejecucion.Rows[0].Cells[2].Value;
                    valores.Cells[3].Value = dgv_ejecucion.Rows[0].Cells[3].Value;
                    valores.Cells[4].Value = dgv_ejecucion.Rows[0].Cells[4].Value;
                    valores.Cells[5].Value = dgv_ejecucion.Rows[0].Cells[5].Value;
                    valores.Cells[6].Value = dgv_ejecucion.Rows[0].Cells[6].Value;

                    dgv_ejecucion.Rows.Remove(dgv_ejecucion.Rows[0]);
                    dgv_espera.Rows.Add(valores);
                }
            }



        }


    }
}
