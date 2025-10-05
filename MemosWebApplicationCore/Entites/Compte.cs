

namespace MemosWebApplicationCore.Entites;

public class Compte: BaseEntity
{
    public string NomUtilisateur { get; set; } = string.Empty;

    public string MotDePasse { get; set; } = string.Empty;

    public DateTime DateCreation { get; set; }

    public DateTime? DateDerniereConnexion { get; set; }

    public virtual ICollection<Memo> Memos { get; set; } = new List<Memo>();
}
