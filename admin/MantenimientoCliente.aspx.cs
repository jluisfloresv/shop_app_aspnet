﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

    public partial class admin_adminClientes_MantenimientoCliente : System.Web.UI.Page
    {

    protected void Page_Load(object sender, EventArgs e)
    {
        cargarDatos();

    }

    void cargarDatos()
    {
        using (DBDataContext dbContext = new DBDataContext())
        {
            GridView1.DataSource = dbContext.clientes;
            GridView1.DataBind();
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;
        txtIdModi.Text = row.Cells[1].Text;
        txtNombresModi.Text = row.Cells[2].Text;
        txtApellidosModi.Text = row.Cells[3].Text;
        listGeneroModi.Text = row.Cells[4].Text;
        txtNacimientoModi.Text = row.Cells[5].Text;
        txtMailModi.Text = row.Cells[6].Text;
        txtContraseñaModi.Text = row.Cells[7].Text;
 
        lblIdelimininar.Text = row.Cells[1].Text;

        btnModalModificar.Enabled = true;
        btnModalEliminar.Enabled = true;

    }

    protected void btnModalAgregar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAgregar();", true);
    }

    protected void btnModalEliminar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEliminar();", true);
    }

    protected void btnModalModificar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalModificar();", true);
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        using (DBDataContext dbContext = new DBDataContext())
        {

            dbContext.agregarCliente(txtNombresAgregar.Text, txtApellidosAgregar.Text, listGeneroAgregar.SelectedValue, DateTime.Parse(txtNacimientoAgregar.Text), txtMailAgregar.Text, txtContraseñaAgregar.Text);
        }
        limpiar();
        
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        using (DBDataContext dbContext = new DBDataContext())
        {

            dbContext.modificarCliente(int.Parse(txtIdModi.Text), txtNombresModi.Text, txtApellidosModi.Text, listGeneroModi.SelectedValue, DateTime.Parse(txtNacimientoModi.Text), txtMailModi.Text, txtContraseñaModi.Text);
        }
        limpiar();

    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        using (DBDataContext dbContext = new DBDataContext())
        {
            dbContext.eliminarCliente(int.Parse(txtIdModi.Text));

        }
        limpiar();
    }

    void limpiar()
    {
        cargarDatos();
        GridView1.SelectedIndex = -1;
        btnModalModificar.Enabled = false;
        btnModalEliminar.Enabled = false;
        foreach (Control ctrl in Panel1.Controls)
        {
            if (ctrl is TextBox)
            {
                ((TextBox)ctrl).Text = "";
            }
        }
        Response.Redirect(Request.Url.AbsoluteUri);//Para evitar doble insercion al refrescar navegador
    }

}
