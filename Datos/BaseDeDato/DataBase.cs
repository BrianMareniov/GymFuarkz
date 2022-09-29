
using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos.BaseDeDato
{
    public class DataBase
    {
        #region Variables privadas

        private SqlConnection _objetoSqlConnection;
        //Forma para permitir hacer una lectura de datos(consultas select)
        private SqlDataAdapter _objetoSqlDataAdapter;
        //Para enviar comandos para crear, actualizar y borrar informacion
        private SqlCommand _objetoSqlCommand;
        //Para obtener una lista de tablas
        private DataSet _dsResultados;
        private DataTable _dtParametros;
        private string _nombreTabla, _nombreSP, _mensajeErrorDB, _valorScalar, _nombreDB;
        private bool _scalar;

        #endregion

        #region Variables publicas
        public SqlConnection ObjetoSqlConnection { get => _objetoSqlConnection; set => _objetoSqlConnection = value; }
        public SqlDataAdapter ObjetoSqlDataAdapter { get => _objetoSqlDataAdapter; set => _objetoSqlDataAdapter = value; }
        public SqlCommand ObjetoSqlCommand { get => _objetoSqlCommand; set => _objetoSqlCommand = value; }
        public DataSet DsResultados { get => _dsResultados; set => _dsResultados = value; }
        public DataTable DtParametros { get => _dtParametros; set => _dtParametros = value; }
        public string NombreTabla { get => _nombreTabla; set => _nombreTabla = value; }
        public string NombreSP { get => _nombreSP; set => _nombreSP = value; }
        public string MensajeErrorDB { get => _mensajeErrorDB; set => _mensajeErrorDB = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public string NombreDB { get => _nombreDB; set => _nombreDB = value; }
        public bool Scalar { get => _scalar; set => _scalar = value; }
        #endregion

        #region Constructores

        public DataBase()
        {
            DtParametros = new DataTable("SpParametros");
            DtParametros.Columns.Add("Nombre");
            DtParametros.Columns.Add("TipoDato");
            DtParametros.Columns.Add("Valor");

            NombreDB = "GymFuarkz";
        }

        #endregion

        #region Métodos privados

        private void crearConexionBaseDatos(ref DataBase objDataBase)
        {
            switch (objDataBase._nombreDB)
            {
                case "GymFuarkz":
                    objDataBase.ObjetoSqlConnection = new SqlConnection(Properties.Settings.Default.cadenaConexion_GymFuarkz);
                    break;
                case "OtraBaseDatos":
                    //Por si queremos crear otra instancia para otra base
                    break;
                default:
                    break;
            }
        }

        private void validarConexionBaseDatos(ref DataBase objDataBase)
        {
            if(objDataBase.ObjetoSqlConnection.State == ConnectionState.Closed)
            {
                objDataBase.ObjetoSqlConnection.Open();
            }
            else
            {
                objDataBase.ObjetoSqlConnection.Close();
                //Quita de memoria
                objDataBase.ObjetoSqlConnection.Dispose();
            }
        }

        private void agregarParametros(ref DataBase objDataBase)
        {
            if(objDataBase.DtParametros != null)
            {
                SqlDbType tipoDatoSQL = new SqlDbType();
                foreach (DataRow item in objDataBase.DtParametros.Rows)
                {
                    switch (item[1])
                    {
                        case "1":
                            tipoDatoSQL = SqlDbType.Bit;
                            break;
                        case "2":
                            tipoDatoSQL = SqlDbType.TinyInt;
                            break;
                        case "3":
                            tipoDatoSQL = SqlDbType.SmallInt;
                            break;
                        case "4":
                            tipoDatoSQL = SqlDbType.Int;
                            break;
                        case "5":
                            tipoDatoSQL = SqlDbType.BigInt;
                            break;
                        case "6":
                            tipoDatoSQL = SqlDbType.Decimal;
                            break;
                        case "7":
                            tipoDatoSQL = SqlDbType.SmallMoney;
                            break;
                        case "8":
                            tipoDatoSQL = SqlDbType.Money;
                            break;
                        case "9":
                            tipoDatoSQL = SqlDbType.Float;
                            break;
                        case "10":
                            tipoDatoSQL = SqlDbType.Real;
                            break;
                        case "11":
                            tipoDatoSQL = SqlDbType.Date;
                            break;
                        case "12":
                            tipoDatoSQL = SqlDbType.Time;
                            break;
                        case "13":
                            tipoDatoSQL = SqlDbType.SmallDateTime;
                            break;
                        case "14":
                            tipoDatoSQL = SqlDbType.Char;
                            break;
                        case "15":
                            tipoDatoSQL = SqlDbType.NChar;
                            break;
                        case "16":
                            tipoDatoSQL = SqlDbType.VarChar;
                            break;
                        case "17":
                            tipoDatoSQL = SqlDbType.NVarChar;
                            break;
                        case "18":
                            tipoDatoSQL = SqlDbType.Text;
                            break;
                        default:
                            break;
                    }

                    if (objDataBase.Scalar)
                            {
                                if(item[2].ToString().Equals(string.Empty))
                                {
                                    objDataBase.ObjetoSqlCommand.Parameters.Add(item[0].ToString(), tipoDatoSQL).Value = DBNull.Value;
                                }
                                else
                                {
                                    objDataBase.ObjetoSqlCommand.Parameters.Add(item[0].ToString(), tipoDatoSQL).Value = item[2].ToString();
                                }
                    }
                    else
                    {
                         if (item[2].ToString().Equals(string.Empty))
                         {
                            objDataBase.ObjetoSqlDataAdapter.SelectCommand.Parameters.Add(item[0].ToString(), tipoDatoSQL).Value = DBNull.Value;
                         }
                         else
                         {
                            objDataBase.ObjetoSqlDataAdapter.SelectCommand.Parameters.Add(item[0].ToString(), tipoDatoSQL).Value = item[2].ToString();
                         }
                    }
                    
                }
            }
        }

        private void prepararConexionBaseDatos(ref DataBase objDataBase)
        {
            crearConexionBaseDatos(ref objDataBase);
            validarConexionBaseDatos(ref objDataBase);
        }

        private void ejecutarDataAdapter(ref DataBase objDataBase)
        {
            try
            {
                prepararConexionBaseDatos(ref objDataBase);
                objDataBase.ObjetoSqlDataAdapter = new SqlDataAdapter(objDataBase.NombreSP,objDataBase._objetoSqlConnection);
                objDataBase.ObjetoSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                agregarParametros(ref objDataBase);
                objDataBase.DsResultados = new DataSet();
                objDataBase.ObjetoSqlDataAdapter.Fill(objDataBase.DsResultados, objDataBase.NombreTabla);
            }
            catch (Exception ex)
            {
                objDataBase.MensajeErrorDB = ex.Message.ToString();
            }
            finally
            {
                if(objDataBase.ObjetoSqlConnection.State == ConnectionState.Open)
                {
                    validarConexionBaseDatos(ref objDataBase);
                }
            }
        }

        private void ejecutarCommand(ref DataBase objDataBase)
        {
            try
            {
                prepararConexionBaseDatos(ref objDataBase);
                objDataBase.ObjetoSqlCommand = new SqlCommand(objDataBase.NombreSP, objDataBase._objetoSqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                agregarParametros(ref objDataBase);

                if(objDataBase.Scalar)
                {
                    objDataBase.ValorScalar = objDataBase.ObjetoSqlCommand.ExecuteScalar().ToString().Trim();
                }
                else
                {
                    objDataBase.ObjetoSqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                objDataBase.MensajeErrorDB = ex.Message.ToString();
            }
            finally
            {
                if (objDataBase.ObjetoSqlConnection.State == ConnectionState.Open)
                {
                    validarConexionBaseDatos(ref objDataBase);
                }
            }
        }

        #endregion

        #region Métodos publicos
        public void CRUD(ref DataBase objDataBase)
        {
            if(objDataBase.Scalar)
            {
                ejecutarCommand(ref objDataBase);
            }
            else
            {
                ejecutarDataAdapter(ref objDataBase);
            }
        }
        #endregion
    }
}
