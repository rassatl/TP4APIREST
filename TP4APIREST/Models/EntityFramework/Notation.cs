using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TP4APIREST.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    public partial class Notation
    {
        public Notation()
        {
        }

        [ForeignKey("fk_not_utl")]
        [Column("utl_id", Order = 0)]
        public int UtilisateurId { get; set; }

        [ForeignKey("fk_not_flm")]
        [Column("flm_id", Order = 1)]
        public int FilmId { get; set; }

        [Column("not_note")]
        public int? Note { get; set; } = null!;

        [InverseProperty("Notations")]
        public virtual Film FilmNavigation { get; set; } = null!;

        [InverseProperty("NotesUtilisateur")]
        public virtual Utilisateur UtilisateurNavigation { get; set; } = null!;
    }
}