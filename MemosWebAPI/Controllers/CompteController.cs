using Microsoft.AspNetCore.Mvc;
using MemosWebAPI.DTO;
using MemosWebApplicationCore.Interfaces;
using MemosWebApplicationCore.Entites;

namespace MemosWebAPI.Controllers
{
    /// <summary>
    /// Contrôleur pour gérer les comptes utilisateurs
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CompteController : ControllerBase
    {
        private readonly ICompteService _compteService;
        private readonly ILogger<CompteController> _logger;

        public CompteController(ICompteService compteService, ILogger<CompteController> logger)
        {
            _compteService = compteService;
            _logger = logger;
        }


        /// <summary>
        /// Créer un nouveau compte utilisateur
        /// </summary>
        /// <param name="dto">Données du compte à créer</param>
        /// <returns>Le compte créé</returns>
        /// <response code="200">Compte créé avec succès</response>
        /// <response code="400">Données invalides</response>
        /// <response code="500">Erreur serveur lors de la création du compte</response>
        [HttpPost("[action]")]
        public async Task<ActionResult<string>> AjouterCompte([FromBody] NouveauCompteDto dto)
        {
            try
            {
                await _compteService.CreerCompteAsync(dto.NomUtilisateur, dto.MotDePasse);
                return Ok("Compte créé avec succès.");
            }
            catch (ArgumentException aEx)
            {
                _logger.LogWarning(aEx, aEx.Message);
                return BadRequest(aEx.Message);
            }
            catch (InvalidOperationException ioEx)
            {
                _logger.LogWarning(ioEx, ioEx.Message);
                return BadRequest(ioEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du compte.");
                return StatusCode(500, "Une erreur est survenue lors de la création du compte.");
            }
        }


        /// <summary>
        /// Authentifier un utilisateur
        /// </summary>
        /// <param name="dto">Données de connexion (nom d'utilisateur et mot de passe)</param>
        /// <returns>Informations de connexion avec jeton</returns>
        /// <response code="200">Connexion réussie</response>
        /// <response code="400">Données de connexion invalides</response>
        /// <response code="401">Nom d'utilisateur ou mot de passe incorrect</response>
        /// <response code="500">Erreur serveur lors de l'authentification</response>
        [HttpPost("[action]")]
        public async Task<ActionResult<object>> Connexion([FromBody] ConnexionDto dto)
        {
            try
            {
                var compte = await _compteService.AuthentifierAsync(dto.NomUtilisateur, dto.MotDePasse);
                return Ok(new { jeton = compte.NomUtilisateur, nomUtilisateur = compte.NomUtilisateur });
            }
            catch (ArgumentException aEx)
            {
                _logger.LogWarning(aEx, aEx.Message);
                return BadRequest(aEx.Message);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                _logger.LogWarning(uaEx, uaEx.Message);
                return Unauthorized(uaEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'authentification.");
                return StatusCode(500, "Une erreur est survenue lors de l'authentification.");
            }
        }
    }
}