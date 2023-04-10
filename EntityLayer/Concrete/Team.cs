using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }

        /* Bu kod örneği, Entity Framework Core (EF Core) kullanılarak bir ilişki modelini tanımlamak için kullanılan iki ICollection özelliğidir. Bu özellikler, bir takım ile ev sahibi veya konuk olarak oynadıkları tüm maçları tutar.

          HomeMatches özelliği, takımın ev sahibi olarak oynadığı tüm maçların koleksiyonunu tutar. Bu koleksiyonda, Match sınıfından bir nesne bulunur.

          AwayMatches özelliği ise takımın konuk olarak oynadığı tüm maçların koleksiyonunu tutar. Bu koleksiyonda da Match sınıfından bir nesne bulunur.

          Bu ilişki, Match sınıfındaki HomeTeam ve GuestTeam özellikleriyle ilişkilidir. Bu özellikler, sırasıyla, maçın ev sahibi takımını ve konuk takımını belirtir. HomeMatches ve AwayMatches özellikleri, her bir takımın oynadığı maçlarla ilişkilidir ve bu özellikler, bir takım silindiğinde, bu takımla ilgili tüm maç kayıtlarının da silinmesini önlemek için kullanılır.
        */
    }
}
