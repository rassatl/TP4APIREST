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
        private readonly FilmRatingsDBContext context;
        private UtilisateursController controller;

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            // Arrange
            List<Utilisateur> users = context.Utilisateurs.ToList();

            // Act
            var result = controller.GetUtilisateurs().Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Utilisateur>>));

            // Act
            List<Utilisateur> usersresult = result.Value.ToList();

            // Assert
            CollectionAssert.AreEqual(users, usersresult);
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest_HttpResponse200()
        {
            // Arrange
            Utilisateur user = context.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == 1);

            // Act
            var result = controller.GetUtilisateurById(1).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Utilisateur>));
            Assert.IsNull(result.Result);
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur));
            Assert.AreEqual(user, result.Value);
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest_HttpResponse404()
        {
            // Act
            var result = controller.GetUtilisateurById(-1).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Utilisateur>));
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_HttpResponse200()
        {
            // Arrange
            Utilisateur user = context.Utilisateurs.FirstOrDefault(u => u.Mail == "clilleymd@last.fm");

            // Act
            var result = controller.GetUtilisateurByEmail("clilleymd@last.fm").Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Utilisateur>));
            Assert.IsNull(result.Result);
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur));
            Assert.AreEqual(user, result.Value);
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_HttpResponse404()
        {
            // Act
            var result = controller.GetUtilisateurByEmail("dummymail@gmail.com").Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Utilisateur>));
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void PutUtilisateurTest_HttpResponse204()
        {
            // Arrange
            Utilisateur user = context.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == 1);

            // Act
            var result = controller.PutUtilisateur(1, user).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void PutUtilisateurTest_HttpResponse400_BadId()
        {
            // Arrange
            Utilisateur user = context.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == 1);

            // Act
            var result = controller.PutUtilisateur(2, user).Result;

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
                controller.ModelState.AddModelError("Mobile", "The Mobile field is not a valid phone number.");
            }
            var result = controller.PutUtilisateur(1, user).Result;

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
            var result = controller.PutUtilisateur(-1, user).Result;

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
            _ = controller.PutUtilisateur(1, user).Result;
        }

        [TestMethod()]
        public void PostUtilisateurTest_HttpResponse201()
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
            var result = controller.PostUtilisateur(user).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Utilisateur>));
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));

            // Dearrange
            context.Utilisateurs.Remove(user);
            context.SaveChanges();
        }

        [TestMethod()]
        public void PostUtilisateurTest_HttpResponse400()
        {
            // Arrange
            Utilisateur user = new Utilisateur()
            {
                UtilisateurId = -1,
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
                controller.ModelState.AddModelError("Mobile", "The Mobile field is not a valid phone number.");
            }
            var result = controller.PostUtilisateur(user).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Utilisateur>));
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod()]
        [ExpectedException(typeof(AggregateException))]
        public void PostUtilisateurTest_AggregateException()
        {
            // Arrange
            Utilisateur user = new Utilisateur()
            {
                UtilisateurId = -1,
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
            _ = controller.PostUtilisateur(user).Result;
        }

        [TestMethod()]
        public void DeleteUtilisateurTest_HttpResponse204()
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
            context.Utilisateurs.Add(user);
            context.SaveChanges();

            // Act
            var result = controller.DeleteUtilisateur(user.UtilisateurId).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));

            // Act
            var verif = context.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == user.UtilisateurId);

            // Assert
            Assert.IsNull(verif);
        }

        [TestMethod()]
        public void DeleteUtilisateurTest_HttpResponse404()
        {
            // Act
            var result = controller.DeleteUtilisateur(-1).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}