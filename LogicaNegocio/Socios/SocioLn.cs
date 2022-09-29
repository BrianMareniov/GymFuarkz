using Datos.BaseDeDato;
using Entidades.Socios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Socios
{
    public class SocioLn
    {
        #region Variables privadas
        private DataBase objDataBase = null;
        #endregion

        #region Metodo Index
        //Listar informacion y mostrar en pantalla
        public void Index(ref Socio objSocio)
        {
            objDataBase = new DataBase()
            {
                NombreTabla = "SOCIOS",
                NombreSP = "[dbo].[SOCIOS_Index]",
                Scalar = false
            };
            ejecutar(ref objSocio);
        }
        #endregion

        #region CRUD

        public void create(ref Socio objSocio)
        {
            objDataBase = new DataBase()
            {
                NombreTabla = "SOCIOS",
                NombreSP = "[dbo].[SOCIOS_Create]",
                Scalar = true
            };

            objDataBase.DtParametros.Rows.Add(@"@dni","16",objSocio.Dni);
            objDataBase.DtParametros.Rows.Add(@"@nombre", "16", objSocio.Nombre);
            objDataBase.DtParametros.Rows.Add(@"@apellido", "16", objSocio.Apellido);
            objDataBase.DtParametros.Rows.Add(@"@fechaDeNacimiento", "11", objSocio.FechaDeNacimiento);
            objDataBase.DtParametros.Rows.Add(@"@direccion", "16", objSocio.Direccion);
            objDataBase.DtParametros.Rows.Add(@"@email", "16", objSocio.Email);
            objDataBase.DtParametros.Rows.Add(@"@celular", "16", objSocio.Celular);
            objDataBase.DtParametros.Rows.Add(@"@estaInactivo", "1", objSocio.EstaInactivo);
            objDataBase.DtParametros.Rows.Add(@"@balanceCuenta", "9", objSocio.BalanceCuenta);
            objDataBase.DtParametros.Rows.Add(@"@diasDisponibles", "4", objSocio.DiasDisponibles);

            ejecutar(ref objSocio);
        }

        public void read(ref Socio objSocio)
        {
            objDataBase = new DataBase()
            {
                NombreTabla = "SOCIOS",
                NombreSP = "[dbo].[SOCIOS_Read]",
                Scalar = false
            };
            objDataBase.DtParametros.Rows.Add(@"@idSocio", "4", objSocio.IdSocio);
            ejecutar(ref objSocio);
        }

        public void update(ref Socio objSocio)
        {
            objDataBase = new DataBase()
            {
                NombreTabla = "SOCIOS",
                NombreSP = "[dbo].[SOCIOS_Update]",
                Scalar = true
            };

            objDataBase.DtParametros.Rows.Add(@"@idSocio", "4", objSocio.IdSocio);
            objDataBase.DtParametros.Rows.Add(@"@dni", "16", objSocio.Dni);
            objDataBase.DtParametros.Rows.Add(@"@nombre", "16", objSocio.Nombre);
            objDataBase.DtParametros.Rows.Add(@"@apellido", "16", objSocio.Apellido);
            objDataBase.DtParametros.Rows.Add(@"@fechaDeNacimiento", "11", objSocio.FechaDeNacimiento);
            objDataBase.DtParametros.Rows.Add(@"@direccion", "16", objSocio.Direccion);
            objDataBase.DtParametros.Rows.Add(@"@email", "16", objSocio.Email);
            objDataBase.DtParametros.Rows.Add(@"@celular", "16", objSocio.Celular);
            objDataBase.DtParametros.Rows.Add(@"@estaInactivo", "1", objSocio.EstaInactivo);
            objDataBase.DtParametros.Rows.Add(@"@balanceCuenta", "9", objSocio.BalanceCuenta);
            objDataBase.DtParametros.Rows.Add(@"@diasDisponibles", "4", objSocio.DiasDisponibles);
            ejecutar(ref objSocio);
        }

        public void delete(ref Socio objSocio)
        {
            objDataBase = new DataBase()
            {
                NombreTabla = "SOCIOS",
                NombreSP = "[dbo].[SOCIOS_Delete]",
                Scalar = true
            };
            objDataBase.DtParametros.Rows.Add(@"@idSocio", "4", objSocio.IdSocio);
            ejecutar(ref objSocio);
        }

        #endregion

        #region Metodos privados
        private void ejecutar(ref Socio objSocio)
        {
            objDataBase.CRUD(ref objDataBase);

            if(objDataBase.MensajeErrorDB == null)
            {
                if(objDataBase.Scalar)
                {
                    objSocio.ValorScalar = objDataBase.ValorScalar;
                }
                else
                {
                    objSocio.DtResultados = objDataBase.DsResultados.Tables[0];
                    if(objSocio.DtResultados.Rows.Count == 1)
                    {
                        foreach (DataRow item in objSocio.DtResultados.Rows)
                        {
                            objSocio.IdSocio = Convert.ToInt32(item["IdSocio"].ToString());
                            objSocio.Dni = item["Dni"].ToString();
                            objSocio.Nombre = item["Nombre"].ToString();
                            objSocio.Apellido = item["Apellido"].ToString();
                            objSocio.FechaDeNacimiento = Convert.ToDateTime(item["FechaDeNacimiento"].ToString());
                            objSocio.Direccion = item["Direccion"].ToString();
                            objSocio.Email = item["Email"].ToString();
                            objSocio.Celular = item["Celular"].ToString();
                            objSocio.EstaInactivo = Convert.ToBoolean(item["EstaInactivo"].ToString());
                            objSocio.BalanceCuenta = Convert.ToInt32(item["BalanceCuenta"].ToString());
                            objSocio.DiasDisponibles = Convert.ToInt32(item["DiasDisponibles"].ToString());          
                        }
                    }
                }
            }
            else
            {
                objSocio.MensajeError = objDataBase.MensajeErrorDB;
            }
        }
        #endregion

    }
}
