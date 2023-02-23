using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TP4APIREST.Models.EntityFramework
{
    [Table("t_e_utilisateur_utl")]
    public partial class Utilisateur
    {
        public Utilisateur()
        {
        }

        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [Column("utl_nom")]
        [StringLength(50)]
        public String? Nom { get; set; }

        [Column("utl_prenom")]
        [StringLength(50)]
        public String? Prenom { get; set; }

        [Column("utl_mobile", TypeName = "char(10)")]
        public String? Mobile { get; set; }

        [Column("utl_mail")]
        [StringLength(100)]
        public String? Mail { get; set; } = null!;

        [Column("utl_pwd")]
        [StringLength(64)]
        public String? Pwd { get; set; } = null!;

        [Column("utl_rue")]
        [StringLength(200)]
        public String? Rue { get; set; }

        [Column("utl_cp", TypeName = "char(5)")]
        public String? CodePostal { get; set; }

        [Column("utl_ville")]
        [StringLength(50)]
        public String? Ville { get; set; }

        [Column("utl_pays")]
        [StringLength(50)]
        public String? Pays { get; set; }

        [Column("utl_latitude")]
        public float? Latitude { get; set; }

        [Column("utl_longitude")]
        public float? Longitude { get; set; }

        [Required]
        [Column("utl_datecreation")]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [InverseProperty(nameof(Notation.UtilisateurNavigation))]
        public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();
    }
}