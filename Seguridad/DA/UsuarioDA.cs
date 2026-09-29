using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Npgsql;

namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {

        private readonly NpgsqlConnection _connection;
        private readonly IRepositorioDapper _repositorioDapper;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _connection = _repositorioDapper.ObtenerRepositorio();
        }

        public Task<Guid> CrearUsuario(UsuarioRequest usuario)
        {
            var query = "SELECT fn_agregar_usuario(@p_id, @p_nombre, @p_apellido1, @p_apellido2, @p_passwordhash, @p_estado, @p_correo, @p_idrol)";
            var resultado = _connection.ExecuteScalarAsync<Guid>(query, new
            {
                p_id = Guid.NewGuid(),
                p_nombre = usuario.Nombre,
                p_apellido1 = usuario.Apellido1,
                p_apellido2 = usuario.Apellido2,
                p_passwordhash = usuario.PasswordHash,
                p_estado = usuario.Estado,
                p_correo = usuario.Correo,
                p_idrol = usuario.IdRol
            });
            return resultado;
        }

        public async Task<UsuarioResponse> ObtenerUsuario(string correo)
        {
            var query = "SELECT * FROM obtenerusuariocorreo(@p_correo)";
            var resultado = await _connection.QueryFirstOrDefaultAsync<UsuarioResponse>(query, new { p_correo = correo });
            return resultado;
        }

        public async Task<UsuarioResponse> ObtenerUsuarioPorId(Guid id)
        {
            var query = "SELECT * FROM obtenerusuarioporid(@p_id)";
            var resultado = await _connection.QueryFirstOrDefaultAsync<UsuarioResponse>(query, new { p_id = id });
            return resultado;
        }
    }
}
