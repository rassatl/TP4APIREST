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

        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [Key]
        [Column("flm_id")]
        public int FilmId { get; set; }

        [Column("not_note")]
        [Range(0,5)]
        public int? Note { get; set; } = null!;

        [InverseProperty("FilmNote")]
        public virtual ICollection<Film> Film { get; set; }

        [InverseProperty("UtilisateurNotant")]
        public virtual ICollection<Utilisateur> Utilisateur { get; set; }
    }
}