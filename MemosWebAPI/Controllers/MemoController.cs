using Microsoft.AspNetCore.Mvc;
using MemosWebAPI.DTO;
using MemosWebApplicationCore.Interfaces;
using MemosWebApplicationCore.Entites;

namespace MemosWebAPI.Controllers
{
    /// <summary>
    /// Contrôleur pour gérer les mémos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService _memoService;
        private readonly ILogger<MemoController> _logger;

        public MemoController(IMemoService memoService, ILogger<MemoController> logger)
        {
            _memoService = memoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtenir tous les mémos d'un utilisateur
        /// </summary>
        /// <param name="nomUtilisateur">Nom d'utilisateur pour filtrer les mémos</param>
        /// <returns>Liste des mémos de l'utilisateur</returns>
        /// <response code="200">Liste des mémos récupérée avec succès</response>
        /// <response code="400">Nom d'utilisateur invalide</response>
        /// <response code="500">Erreur serveur lors de l'obtention des mémos</response>
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Memo>>> ObtenirMemosParUtilisateur([FromQuery] string nomUtilisateur)
        {
            try
            {
                var result = await _memoService.ObtenirMemosParUtilisateurAsync(nomUtilisateur);
                return Ok(result);
            }
            catch (ArgumentException aEx)
            {
                _logger.LogWarning(aEx, aEx.Message);
                return BadRequest(aEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de l'obtention des mémos pour l'utilisateur {nomUtilisateur}.");
                return StatusCode(500, "Une erreur est survenue lors de l'obtention des mémos.");
            }
        }


        /// <summary>
        /// Créer un nouveau mémo
        /// </summary>
        /// <param name="dto">Données du mémo à créer</param>
        /// <returns>Le mémo créé</returns>
        /// <response code="200">Mémo créé avec succès</response>
        /// <response code="400">Données invalides</response>
        /// <response code="500">Erreur serveur lors de la création du mémo</response>
        [HttpPost("[action]")]
        public async Task<ActionResult<string>> CreerMemo([FromBody] NouveauMemoDto dto)
        {
            try
            {
                await _memoService.CreerMemoAsync(dto.Titre, dto.Description, dto.NomUtilisateur);
                return Ok("Mémo créé avec succès.");
            }
            catch (ArgumentException aEx)
            {
                _logger.LogWarning(aEx, aEx.Message);
                return BadRequest(aEx.Message);
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogWarning(knfEx, knfEx.Message);
                return BadRequest(knfEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du mémo.");
                return StatusCode(500, "Une erreur est survenue lors de la création du mémo.");
            }
        }

        /// <summary>
        /// Supprimer un mémo existant
        /// </summary>
        /// <param name="id">Identifiant unique du mémo à supprimer</param>
        /// <returns>Aucun contenu en cas de succès</returns>
        /// <response code="200">Mémo supprimé avec succès</response>
        /// <response code="404">Mémo avec l'ID spécifié non trouvé</response>
        /// <response code="500">Erreur serveur lors de la suppression du mémo</response>
        [HttpDelete("[action]/{id:int}")]
        public async Task<ActionResult<string>> SupprimerMemo([FromRoute] int id)
        {
            try
            {
                await _memoService.SupprimerMemoAsync(id);
                return Ok("Mémo supprimé avec succès.");
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogWarning(knfEx, knfEx.Message);
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la suppression du mémo avec l'ID {id}.");
                return StatusCode(500, "Une erreur est survenue lors de la suppression du mémo.");
            }
        }
    }
}