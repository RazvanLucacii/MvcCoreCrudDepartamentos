using MvcCoreCrudDepartamentos.Models;
using System.Data;
using System.Data.SqlClient;

#region PROCEDIMIENTOS ALMACENADOS

//create procedure SP_INSERTDEPARTAMENTO
//(@NOMBRE NVARCHAR(50), @LOCALIDAD NVARCHAR(50))
//AS
//    DECLARE @NEXTID INT
//	SELECT @NEXTID = MAX(DEPT_NO) +1 FROM DEPT
//	INSERT INTO DEPT VALUES (@NEXTID, @NOMBRE, @LOCALIDAD)
//GO

#endregion

namespace MvcCoreCrudDepartamentos.Repositories
{
    public class RepositoryDepartamentos
    {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
        public RepositoryDepartamentos() 
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.connection = new SqlConnection(connectionString);
            this.command = new SqlCommand();
            this.command.Connection = this.connection;
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string sql = "select * from DEPT";
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;
            await this.connection.OpenAsync();
            this.reader = await this.command.ExecuteReaderAsync();
            List<Departamento> departamentos = new List<Departamento>();
            while (await this.reader.ReadAsync())
            {
                Departamento dept = new Departamento();
                dept.IdDepartamento = int.Parse(this.reader["DEPT_NO"].ToString());
                dept.Nombre = this.reader["DNOMBRE"].ToString();
                dept.Localidad = this.reader["LOC"].ToString();
                departamentos.Add(dept);
            }
            await this.reader.CloseAsync();
            await this.connection.CloseAsync();
            return departamentos;
        }

        public async Task<Departamento> FindDepartamentoAsync(int id)
        {
            string sql = "select * from DEPT where DEPT_NO=@id";
            this.command.Parameters.AddWithValue("@id", id);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;
            await this.connection.OpenAsync();
            this.reader = await command.ExecuteReaderAsync();
            Departamento departamento = null;
            if (await this.reader.ReadAsync())
            {
                departamento = new Departamento();
                departamento.IdDepartamento = int.Parse(this.reader["DEPT_NO"].ToString());
                departamento.Nombre = this.reader["DNOMBRE"].ToString();
                departamento.Localidad = this.reader["LOC"].ToString();

            }
            else
            {
                //SIN DATOS
            }
            await connection.CloseAsync();
            await this.reader.CloseAsync();
            this.command.Parameters.Clear();
            return departamento;
        }

        public async Task InsertDepartamentos(string nombre, string localidad)
        {
            string sql = "SP_INSERTDEPARTAMENTO";
            this.command.Parameters.AddWithValue("@NOMBRE", nombre);
            this.command.Parameters.AddWithValue("@LOCALIDAD", localidad);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.StoredProcedure;
            await this.connection.OpenAsync();
            int af = await this.command.ExecuteNonQueryAsync();
            await this.connection.CloseAsync();
            this.command.Parameters.Clear();
        }

        public async Task UpdateDepartamentoAsync(int id, string nombre, string localidad)
        {
            string sql = "update DEPT set DNOMBRE=@NOMBRE, LOC=@LOCALIDAD WHERE DEPT_NO=@ID";
            this.command.Parameters.AddWithValue("@NOMBRE", nombre);
            this.command.Parameters.AddWithValue("@LOCALIDAD", localidad);
            this.command.Parameters.AddWithValue("@ID", id);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;
            await this.connection.OpenAsync();
            int af = await this.command.ExecuteNonQueryAsync();
            await this.connection.CloseAsync();
            this.command.Parameters.Clear();
        }

        public async Task DeleteDepartamentoAsync(int id)
        {
            string sql = "delete from DEPT where DEPT_NO=@id";
            this.command.Parameters.AddWithValue("@id", id);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;
            await this.connection.OpenAsync();
            int af = await this.command.ExecuteNonQueryAsync();
            await this.connection.CloseAsync();
            this.command.Parameters.Clear();
        }
    }
}
