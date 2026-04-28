namespace Tests;

[TestClass]
public sealed class CardTesting
{
    [TestMethod]
    public void ReadCardList()
    {
        var cardNames = nac.CardImage.Card.GetCardList();

        Assert.IsTrue(cardNames.Count > 0);
    }
}
