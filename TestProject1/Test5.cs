using ProjetA2AlexandreAlbin;

namespace TestProject1;

[TestClass]
public class Test5
{
    [TestMethod]
    public void TestInitialisationJoueur()
    {
        // Arrange
        string nomAttendu = "Alex";

        // Act
        Joueur j = new Joueur(nomAttendu);

        // Assert
        Assert.AreEqual(nomAttendu, j.Nom, "Le nom du joueur est incorrect.");
        Assert.AreEqual(0, j.Score, "Le score initial doit être de 0.");
        Assert.IsNotNull(j.MotsTrouvees, "La liste des mots trouvés ne doit pas être nulle.");
    }
}
