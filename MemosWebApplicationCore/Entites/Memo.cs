

namespace MemosWebApplicationCore.Entites;

public class Memo: BaseEntity
{
    public string Titre { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime DateCreation { get; set; }

    public int IdCompte { get; set; } 

    public virtual Compte? IdCompteNavigation { get; set; } = null;
}
