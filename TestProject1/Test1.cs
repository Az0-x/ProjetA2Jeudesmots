using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetA2AlexandreAlbin;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestIndependanceMotsJoueurs()
        {
            // Arrange : On crée deux instances distinctes
            Joueur j1 = new Joueur("Alexandre");
            Joueur j2 = new Joueur("Albin");

            // Act : On ajoute un mot uniquement au premier joueur
            j1.Add_Mot("PROGRAMMATION");

            // Assert
            Assert.AreEqual(1, j1.MotsTrouvees.Count, "Le joueur 1 devrait avoir 1 mot.");
            Assert.AreEqual(0, j2.MotsTrouvees.Count, "Le joueur 2 ne devrait avoir aucun mot.");
            Assert.IsFalse(j2.Contient("PROGRAMMATION"), "Le mot du joueur 1 ne doit pas apparaître chez le joueur 2.");
        }
    }
}
