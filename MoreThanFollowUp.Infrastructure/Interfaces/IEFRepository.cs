using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Interfaces
{
    public interface IEFRepository<T> where T : class
    {
        public ICollection<T> Listar();
        public Task<ICollection<T>> ListarAsync();
        public Task<T> AdicionarAsync(T objeto);

        public Task<T> AtualizarAsync(T objeto);

        public Task DeletarAsync(T objeto);

        public Task<T?> RecuperarPorAsync(Expression<Func<T, bool>> condicao);

        public Task CadastrarEmMassaAsync(ICollection<T> listObjects);

      


    }
}
