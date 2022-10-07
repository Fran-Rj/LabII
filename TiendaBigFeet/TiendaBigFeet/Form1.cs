using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiendaBigFeet
{
    public partial class Form1 : Form
    {
        List<ClsProducto> listInventario = new List<ClsProducto>();

        public int idProd = 0;
        int id = 1;

        public Form1()
        {
            InitializeComponent();
            IdProductos();
        }

        public void Limpiar()
        {
            txtMarca.Clear();
            txtDesrip.Clear();
            cbCateg.Text = "";
            txtTalla.Clear();
            txtCant.Clear();
            txtPrice.Clear();
        }

        // ID
        public void IdProductos()
        {
            txtId.Text = id.ToString();
            id = id + 1;
        }

        // MOSTRAR
        public void MostrarInvetario()
        {
            lvInventario.Items.Clear();

            foreach (ClsProducto producto in listInventario)
            {
                ListViewItem item = new ListViewItem();

                item = lvInventario.Items.Add(producto.id.ToString());
                item.SubItems.Add(producto.marca);
                item.SubItems.Add(producto.descripcion);
                item.SubItems.Add(producto.categoria);
                item.SubItems.Add(producto.talla.ToString());
                item.SubItems.Add(producto.cantidad.ToString());
                item.SubItems.Add(producto.pCompra.ToString("0.00"));
                item.SubItems.Add(producto.pVenta.ToString("0.00"));
            }
        }

        // FILTROS
        public void Filtros()
        {
            if (cbFiltro.Text == "" || txtFiltro.Text == "")
            {
                MessageBox.Show("Selecciona un campo e ingresa un valor!");
            }
            else if (cbFiltro.SelectedItem.Equals("Categoria"))
            {
                var consulta = from c in listInventario
                               where c.categoria == txtFiltro.Text
                               select c;

                if (txtFiltro.Text == "Dama" || txtFiltro.Text == "Caballero" || txtFiltro.Text == "Niño" || txtFiltro.Text == "Niña")
                {
                    lvFiltrado.Items.Clear();

                    foreach (ClsProducto producto in consulta)
                    {
                        ListViewItem item = new ListViewItem();

                        item = lvFiltrado.Items.Add(producto.id.ToString());
                        item.SubItems.Add(producto.marca);
                        item.SubItems.Add(producto.descripcion);
                        item.SubItems.Add(producto.categoria);
                        item.SubItems.Add(producto.talla.ToString());
                        item.SubItems.Add(producto.cantidad.ToString());
                        item.SubItems.Add(producto.pCompra.ToString("0.00"));
                        item.SubItems.Add(producto.pVenta.ToString("0.00"));
                    };
                }
                else
                {
                    if (txtFiltro.Text == "")
                    {
                        lvFiltrado.Items.Clear();

                        MessageBox.Show("Ingresa una Categoria");
                    }
                    else
                    {
                        lvFiltrado.Items.Clear();
                        MessageBox.Show("La categoria no existe!");
                    }
                }

            }
            else
            {

                if (cbFiltro.SelectedItem.Equals("Marca"))
                {
                    var consulta = from m in listInventario
                                   where m.marca == txtFiltro.Text
                                   select m;


                    lvFiltrado.Items.Clear();

                    foreach (ClsProducto producto in consulta)
                    {
                        ListViewItem item = new ListViewItem();

                        item = lvFiltrado.Items.Add(producto.id.ToString());
                        item.SubItems.Add(producto.marca);
                        item.SubItems.Add(producto.descripcion);
                        item.SubItems.Add(producto.categoria);
                        item.SubItems.Add(producto.talla.ToString());
                        item.SubItems.Add(producto.cantidad.ToString());
                        item.SubItems.Add(producto.pCompra.ToString("0.00"));
                        item.SubItems.Add(producto.pVenta.ToString("0.00"));
                    }
                }
            }
        }

        // BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtIdP.Text != "")
            {
                foreach (ClsProducto p in listInventario)
                {
                    int Id = int.Parse(txtIdP.Text);

                    if (p.id == Id)
                    {
                        txtMarca.Text = p.marca;
                        txtDesrip.Text = p.descripcion;
                        cbCateg.Text = p.categoria;
                        txtTalla.Text = p.talla.ToString();
                        txtCant.Text = p.cantidad.ToString();
                        txtPrice.Text = p.pCompra.ToString("0.00");

                        idProd = p.id;
                        break;
                    }

                }
            }
            else
            {
                MessageBox.Show("Ingresa un Id");
            }
        }

        // REGISTRAR
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                ClsProducto producto = new ClsProducto();

                double pVenta = 0;

                if (txtMarca.Text != "" && txtDesrip.Text != "" && cbCateg.Text != "" && txtTalla.Text != "" && txtCant.Text != "" && txtPrice.Text != "")
                {
                    producto.id = int.Parse(txtId.Text);
                    producto.marca = txtMarca.Text;
                    producto.descripcion = txtDesrip.Text;
                    producto.categoria = cbCateg.Text;
                    producto.talla = double.Parse(txtTalla.Text);
                    producto.cantidad = int.Parse(txtCant.Text);

                    producto.pCompra = double.Parse(txtPrice.Text);

                    double total = double.Parse(txtPrice.Text) * 0.13;

                    pVenta = (producto.pCompra + total);
                    producto.pVenta = pVenta;

                    listInventario.Add(producto);

                    Limpiar();
                    IdProductos();
                    MostrarInvetario();
                }
                else
                {
                    MessageBox.Show("Debes llenar todos los campos!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }

        // ACTUALIZAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                double pVenta = 0;

                if (txtIdP.Text != "")
                {
                    foreach (ClsProducto p in listInventario)
                    {
                        if (p.id == idProd)
                        {
                            DialogResult request = MessageBox.Show("Quieres actualizar los datos?", "Editar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (request == DialogResult.Yes)
                            {
                                p.marca = txtMarca.Text;
                                p.descripcion = txtDesrip.Text;
                                p.categoria = cbCateg.Text;
                                p.talla = double.Parse(txtTalla.Text);
                                p.cantidad = int.Parse(txtCant.Text);
                                p.pCompra = double.Parse(txtPrice.Text);

                                double total = double.Parse(txtPrice.Text) * 0.13;
                                pVenta = (p.pCompra + total);
                                p.pVenta = pVenta;

                                MostrarInvetario();
                                Limpiar();
                            }

                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Busca un registro!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo actualizar", ex.Message);
            }
        }

        // ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIdP.Text != "")
                {
                    foreach (ClsProducto p in listInventario)
                    {
                        if (p.id == idProd)
                        {
                            DialogResult request = MessageBox.Show("Estás seguro de eliminar el registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (request == DialogResult.Yes)
                            {
                                listInventario.Remove(p);
                                MostrarInvetario();
                                Limpiar();
                            }

                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Busca un registro!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo eliminar!");
            }
        }

        // VALIDACIONES
        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
            {
                MessageBox.Show("Solo numeros!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo letras!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtDesrip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo letras!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                e.Handled = false;
            }
        }

        private void cbCateg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo letras!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtTalla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo numeros y el punto!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtCant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
            {
                MessageBox.Show("Solo numeros!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo numeros y el punto!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            Filtros();
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo letras!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}