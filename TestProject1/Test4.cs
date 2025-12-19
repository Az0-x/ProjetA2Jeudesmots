using ProjetA2AlexandreAlbin;

namespace TestProject1;

[TestClass]
public class Test4
{
    [TestMethod]
    public void TestCumulScore()
    {
        // Arrange
        Joueur j = new Joueur("Test");

        // Act
        j.Score += 10;
        j.Score += 25;

        // Assert
        Assert.AreEqual(35, j.Score, "Le score total devrait être de 35.");
    }
}
