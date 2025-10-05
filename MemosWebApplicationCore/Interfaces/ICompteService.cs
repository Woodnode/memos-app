
using MemosWebApplicationCore.Entites;

namespace MemosWebApplicationCore.Interfaces
{
    public interface ICompteService
    {
        Task<Compte> CreerCompteAsync(string nomUtilisateur, string motDePasse);
        Task<Compte> AuthentifierAsync(string nomUtilisateur, string motDePasse);
    }
}
