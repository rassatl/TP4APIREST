using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP4APIREST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP4APIREST.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace TP4APIREST.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private readonly FilmRatingsDBContext _context;
        private UtilisateursController _controller;

        public UtilisateursControllerTests()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmDB; uid=postgres; password=postgres;"); // Chaine de connexion à mettre dans les ( )
            _context = new FilmRatingsDBContext(builder.Options);
            _controller = new UtilisateursController(_context);
        }

        [TestMethod()]
        public void UtilisateursControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            var dataFromBase = _controller.GetUtilisateurs();
            var testDataFromBase = _context.Utilisateurs.ToList();
            Assert.IsInstanceOfType(dataFromBase.Result, typeof(ActionResult<IEnumerable<Utilisateur>>), "Pas un Action result ienumerable de utilisateur"); // Test du type de retour
            CollectionAssert.AreEqual(dataFromBase.Result.Value.ToList(), testDataFromBase,"Les listes ne sont pas les mêmes"); //Test de l'erreur
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest()
        {
            var dataMethod = _controller.GetUtilisateurById(1).Result;
            var data = _context.Utilisateurs.Where(c => c.UtilisateurId == 1).FirstOrDefault();
            Assert.AreEqual(dataMethod.Value, data, "Les utilisateurs ne sont pas les mêmes"); //Test de l'erreur
        }

        [TestMethod]
        public void GetById_NotExistingIdPassed_ReturnsRightItem()
        {
            var result = _controller.GetUtilisateurById(800).Result;
            Assert.IsInstanceOfType(result, typeof(ActionResult<Utilisateur>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Erreur n'est pas 404"); //Test de l'erreur
            Assert.IsNull(result.Value, "User pas null"); // Test du type du contenu (valeur) du retour
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest()
        {
            var dataMethod = _controller.GetUtilisateurByEmail("gdominguez0@washingtonpost.com").Result;
            var data = _context.Utilisateurs.Where(c => c.Mail == "gdominguez0@washingtonpost.com").FirstOrDefault();
            Assert.AreEqual(dataMethod.Value, data, "Les utilisateurs ne sont pas les mêmes"); //Test de l'erreur
        }

        public void PutUtilisateurTest_HttpResponse204()
        {
            // Arrange
            Utilisateur user = _context.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == 1);

            // Act
            var result = _controller.PutUtilisateur(1, user).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void PutUtilisateurTest_HttpResponse400_BadId()
        {
            // Arrange
            Utilisateur user = _context.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == 1);

            // Act
            var result = _controller.PutUtilisateur(2, user).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void PutUtilisateurTest_HttpResponse400_ModelError()
        {
            // Arrange
            Utilisateur user = new Utilisateur()
            {
                UtilisateurId = 1,
                Nom = "IPSUM",
                Prenom = "Lorem",
                Mobile = "1",
                Mail = "l.ipsum_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "@gmail.com",
                Pwd = "lorsum",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            Regex regex = new Regex(@"^0[0-9]{9}$");

            // Act
            if (!regex.IsMatch(user.Mobile))
            {
                _controller.ModelState.AddModelError("Mobile", "The Mobile field is not a valid phone number.");
            }
            var result = _controller.PutUtilisateur(1, user).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod()]
        public void PutUtilisateurTest_HttpResponse404()
        {
            // Arrange
            Utilisateur user = new Utilisateur()
            {
                UtilisateurId = -1,
                Nom = "IPSUM",
                Prenom = "Lorem",
                Mobile = "0606070809",
                Mail = "l.ipsum_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "@gmail.com",
                Pwd = "lorsum",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            // Act
            var result = _controller.PutUtilisateur(-1, user).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        [ExpectedException(typeof(AggregateException))]
        public void PutUtilisateurTest_AggregateException()
        {
            // Arrange
            Utilisateur user = new Utilisateur()
            {
                UtilisateurId = 1,
                Nom = "IPSUM",
                Prenom = "Lorem",
                Mobile = "0606070809",
                Mail = null,
                Pwd = "lorsum",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            // Act
            _ = _controller.PutUtilisateur(1, user).Result;
        }

        [TestMethod()]
        public void PostUtilisateurTest()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = _controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
            // Assert
            Utilisateur? userRecupere = _context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            Utilisateur userAtester = new Utilisateur()
            {
                UtilisateurId = 100,
                Nom = "loulou",
                Prenom = "lou",
                Mobile = "0606070809",
                Mail = "zozo" + chiffre + "@gmail.com",
                Pwd = "fdp1234!",
                Rue = "Chemin de travers",
                CodePostal = "934",
                Ville = "Poudlard",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            _context.Add(userAtester);
            _context.SaveChanges();

            _controller.DeleteUtilisateur(userAtester.UtilisateurId);

            Utilisateur? userRecupere = _context.Utilisateurs.Where(u => u.UtilisateurId == userAtester.UtilisateurId).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique

            Assert.IsNull(userRecupere, "l'utilisateur existe alors qu'il ne devrait pas !");

        }
    }
}