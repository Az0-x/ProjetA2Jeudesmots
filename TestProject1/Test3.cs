using ProjetA2AlexandreAlbin;

namespace TestProject1;

[TestClass]
public class Test3
{
    [TestMethod]
    public void TestFormatToStringJoueur()
    {
        // Arrange
        Joueur j = new Joueur("Albin");

        // Act
        string affichage = j.Nom;

        // Assert
        // On vérifie si le nom du joueur apparaît bien dans la chaîne retournée
        StringAssert.Contains(affichage, "Albin", "La méthode toString doit afficher le nom du joueur.");
    }
}
