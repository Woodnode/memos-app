using MemosWebApplicationCore.Entites;
using MemosWebApplicationCore.Interfaces;

namespace MemosWebApplicationCore.Services
{
    public class MemoService : IMemoService
    {
        private readonly IAsyncRepository<Memo> _memoRepository;
        private readonly IAsyncRepository<Compte> _compteRepository;

        public MemoService(IAsyncRepository<Memo> memoRepository, IAsyncRepository<Compte> compteRepository)
        {
            _memoRepository = memoRepository;
            _compteRepository = compteRepository;
        }

        public async Task<Memo> CreerMemoAsync(string titre, string description, string nomUtilisateur)
        {
            if (string.IsNullOrWhiteSpace(titre) || string.IsNullOrWhiteSpace(nomUtilisateur))
            {
                throw new ArgumentException("Titre et nom d'utilisateur sont obligatoires.");
            }

            var compte = await ObtenirCompteParNomUtilisateurAsync(nomUtilisateur);
            if (compte == null)
            {
                throw new KeyNotFoundException("Utilisateur introuvable.");
            }

            var memo = new Memo
            {
                Titre = titre,
                Description = description,
                DateCreation = DateTime.Now,
                IdCompte = compte.Id
            };

            return await _memoRepository.AddAsync(memo);
        }

        public async Task<IEnumerable<Memo>> ObtenirMemosParUtilisateurAsync(string nomUtilisateur)
        {
            if (string.IsNullOrWhiteSpace(nomUtilisateur))
            {
                throw new ArgumentException("Nom d'utilisateur obligatoire.");
            }

            var compte = await ObtenirCompteParNomUtilisateurAsync(nomUtilisateur);
            if (compte == null)
            {
                return new List<Memo>();
            }

            return await _memoRepository.ListAsync(m => m.IdCompte == compte.Id);
        }

        public async Task SupprimerMemoAsync(int id)
        {
            var memo = await _memoRepository.GetByIdAsync(id);
            if (memo == null)
            {
                throw new KeyNotFoundException("Mémo introuvable.");
            }

            await _memoRepository.DeleteAsync(memo);
        }

        private async Task<Compte?> ObtenirCompteParNomUtilisateurAsync(string nomUtilisateur)
        {
            var comptes = await _compteRepository.ListAsync(c =>
                c.NomUtilisateur.ToLower() == nomUtilisateur.Trim().ToLower());
            return comptes.FirstOrDefault();
        }
    }
}
