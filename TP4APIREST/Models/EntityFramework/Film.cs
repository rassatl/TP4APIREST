using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;

namespace TP4APIREST.Models.EntityFramework
{
    [Table("t_e_film_flm")]
    public partial class Film
    {
        public Film()
        {
        }

        [Key]
        [Column("flm_id")]
        public int FilmId { get; set; }

        [Column("flm_titre")]
        [StringLength(100)]
        public string Titre { get; set; } = null!;

        [Column("flm_resume")]
        public string? Resume { get; set; }

        [Column("flm_datesortie")]
        public DateTime? DateSortie { get; set; }

        [Column("flm_duree")]
        [Range(3,0)]
        public Decimal? Duree { get; set; }

        [Column("flm_genre")]
        [StringLength(30)]
        public String? Genre { get; set; }

        [InverseProperty("NotesFilms")]
        public virtual ICollection<Notation> Notation { get; set; }
    }
}