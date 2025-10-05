using MemosWebApplicationCore.Entites;
using MemosWebApplicationCore.Interfaces;

namespace MemosWebApplicationCore.Services
{
    public class CompteService : ICompteService
    {
        private readonly IAsyncRepository<Compte> _compteRepository;

        public CompteService(IAsyncRepository<Compte> compteRepository)
        {
            _compteRepository = compteRepository;
        }

        public async Task<Compte> CreerCompteAsync(string nomUtilisateur, string motDePasse)
        {
            if (string.IsNullOrWhiteSpace(nomUtilisateur) || string.IsNullOrWhiteSpace(motDePasse))
            {
                throw new ArgumentException("Le nom d'utilisateur et le mot de passe sont obligatoires.");
            }

            var comptes = await _compteRepository.ListAsync(c =>
                c.NomUtilisateur.ToLower() == nomUtilisateur.Trim().ToLower());
            if (comptes.Any())
            {
                throw new InvalidOperationException("Un compte avec ce nom d'utilisateur existe déjà.");
            }

            var compte = new Compte
            {
                NomUtilisateur = nomUtilisateur.Trim(),
                MotDePasse = motDePasse,
                DateCreation = DateTime.Now
            };

            return await _compteRepository.AddAsync(compte);
        }

        public async Task<Compte> AuthentifierAsync(string nomUtilisateur, string motDePasse)
        {
            if (string.IsNullOrWhiteSpace(nomUtilisateur) || string.IsNullOrWhiteSpace(motDePasse))
            {
                throw new ArgumentException("Le nom d'utilisateur et le mot de passe sont obligatoires.");
            }

            var comptes = await _compteRepository.ListAsync(c =>
                c.NomUtilisateur.ToLower() == nomUtilisateur.Trim().ToLower() &&
                c.MotDePasse == motDePasse);

            var compte = comptes.FirstOrDefault();
            if (compte == null)
            {
                throw new UnauthorizedAccessException("Nom d'utilisateur ou mot de passe incorrect.");
            }

            compte.DateDerniereConnexion = DateTime.Now;
            return await _compteRepository.EditAsync(compte);
        }

    }
}
