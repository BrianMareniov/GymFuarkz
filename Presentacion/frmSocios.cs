using Entidades.Socios;
using LogicaNegocio.Socios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmSocios : Form
    {

        private Socio objSocio = null;
        private readonly SocioLn objSocioLn = new SocioLn();

        public frmSocios()
        {
            InitializeComponent();
            cargarListaSocios();
            limpiarCampos();
        }

        public void cargarListaSocios()
        {
            objSocio = new Socio();
            objSocioLn.Index(ref objSocio);

            if(objSocio.MensajeError == null)
            {
                dgvSocios.DataSource = objSocio.DtResultados;
            }
            else
            {
                MessageBox.Show(objSocio.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            objSocio = new Socio()
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Dni = txtDni.Text,
                FechaDeNacimiento = dtpFechaNacimiento.Value,
                Direccion = txtDireccion.Text,
                Email = txtDireccion.Text,
                Celular = txtCelular.Text,
                EstaInactivo = cbEstado.Checked
            };

            //Al objeto socio de la ln le pasamos el objeto construido para que se mande al SP
            objSocioLn.create(ref objSocio);

            if(objSocio.MensajeError == null)
            {
                MessageBox.Show("El socio con Id: " + objSocio.ValorScalar + " fue agregado correctamente");
                //Lista de usuarios actualizada
                cargarListaSocios();
            }
            else
            {
                MessageBox.Show(objSocio.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            limpiarCampos();
        }

        private void dgvSocios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Capturar errores
            try
            {
                //Si tocamos el boton de editar
                if (dgvSocios.Columns[e.ColumnIndex].Name == "Editar")
                {
                    objSocio = new Socio()
                    {
                        IdSocio =  Convert.ToInt32(dgvSocios.Rows[e.RowIndex].Cells["IdSocio"].Value.ToString())
                    };

                    //Para que en el label muestre el Id socio
                    lblIdSocio.Text = objSocio.IdSocio.ToString();

                    //Lee, trae la info y se llena los campos
                    objSocioLn.read(ref objSocio);

                    txtNombre.Text = objSocio.Nombre;
                    txtApellido.Text = objSocio.Apellido;
                    txtDni.Text = objSocio.Dni;
                    dtpFechaNacimiento.Value = objSocio.FechaDeNacimiento;
                    txtDireccion.Text = objSocio.Direccion;
                    txtEmail.Text = objSocio.Email;
                    txtCelular.Text = objSocio.Celular;
                    cbEstado.Checked = objSocio.EstaInactivo;

                    //Estados botones
                    btnActualizar.Enabled = true;
                    btnEliminar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            DialogResult resul = MessageBox.Show("Seguro que quiere actualizar el socio?", "Actualizar Socio", MessageBoxButtons.YesNo);
            if (resul == DialogResult.Yes)
            {

                objSocio = new Socio()
                {
                    IdSocio = Convert.ToInt32(lblIdSocio.Text),
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Dni = txtDni.Text,
                    FechaDeNacimiento = dtpFechaNacimiento.Value,
                    Direccion = txtDireccion.Text,
                    Email = txtEmail.Text,
                    Celular = txtCelular.Text,
                    EstaInactivo = cbEstado.Checked
                };

                objSocioLn.update(ref objSocio);

                if (objSocio.MensajeError == null)
                {
                    MessageBox.Show("El socio fue actualizado correctamente");
                    //Lista de usuarios actualizada
                    cargarListaSocios();
                }
                else
                {
                    MessageBox.Show(objSocio.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                limpiarCampos();
            }
 
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            DialogResult resul = MessageBox.Show("Seguro que quiere eliminar el socio?", "Eliminar Socio", MessageBoxButtons.YesNo);
            if (resul == DialogResult.Yes)
            {
                objSocio = new Socio()
                {
                    IdSocio = Convert.ToInt32(lblIdSocio.Text)
                };

                objSocioLn.delete(ref objSocio);
                cargarListaSocios();
                limpiarCampos();
            }

        }

        private void limpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDni.Text = string.Empty;
            dtpFechaNacimiento.Value = DateTime.Now;
            txtDireccion.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCelular.Text = string.Empty;
            cbEstado.Checked = true;
            lblIdSocio.Text = string.Empty;

            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
  
        }

    }
}
