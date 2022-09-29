using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Socios
{
    public class Socio
    {
        #region atributos privados
        private int idSocio;
        private string dni;
        private string nombre;
        private string apellido;
        private DateTime fechaDeNacimiento;
        private string direccion;
        private string email;
        private string celular;
        private bool estaInactivo;
        private float balanceCuenta;
        private int diasDisponibles;
        private string _mensajeError, _valorScalar;
        private DataTable _dtResultados;
        #endregion atributos

        #region atributos publicos
        //Getters and setters
        public int IdSocio { get => idSocio; set => idSocio = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public DateTime FechaDeNacimiento { get => fechaDeNacimiento; set => fechaDeNacimiento = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Email { get => email; set => email = value; }
        public string Celular { get => celular; set => celular = value; }
        public bool EstaInactivo { get => estaInactivo; set => estaInactivo = value; }
        public float BalanceCuenta { get => balanceCuenta; set => balanceCuenta = value; }
        public int DiasDisponibles { get => diasDisponibles; set => diasDisponibles = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion

        //Constructor
        public Socio(int idSocio, string dni, string nombre, string apellido, DateTime fechaDeNacimiento, string direccion, string email, string celular, bool estaInactivo, float balanceCuenta, int diasDisponibles)
        {
            this.IdSocio = idSocio;
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.FechaDeNacimiento = fechaDeNacimiento;
            this.Direccion = direccion;
            this.Email = email;
            this.Celular = celular;
            this.EstaInactivo = estaInactivo;
            this.BalanceCuenta = balanceCuenta;
            this.DiasDisponibles = diasDisponibles;
        }

        //Constructor vacio 
        public Socio()
        {
        }
    }
}
