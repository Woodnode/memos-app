using MemosWebApplicationCore.Entites;

namespace MemosWebApplicationCore.Interfaces
{
    public interface IMemoService
    {
        Task<Memo> CreerMemoAsync(string titre, string description, string nomUtilisateur);
        Task<IEnumerable<Memo>> ObtenirMemosParUtilisateurAsync(string nomUtilisateur);
        Task SupprimerMemoAsync(int id);
    }
}
