using System.Linq;
using TestingBasics.functions;
using Xunit;
using System;
using Moq;


namespace TestingBasics.Test;

public class item_Test
{
    public readonly Mock<IDbService> _dbServiceMock = new();

    [Fact]
    public void AddProduct_Success()
    {
        var product = new Product("item1", "cola", "resauramt1");
        _dbServiceMock.Setup(x => x.SaveChangeItem(product)).Returns(true);
        // Arrange
        ChangeItem ChangeItem = new(_dbServiceMock.Object);

        // Act
        var result = ChangeItem.AddProduct(product);

        // Assert 新增品項成功
        Assert.True(result);
        _dbServiceMock.Verify(x => x.SaveChangeItem(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public void AddProduct_Failure()
    {

        // Arrange
        ChangeItem ChangeItem = new(_dbServiceMock.Object);

        // Act
        var result = ChangeItem.AddProduct(null);

        // Assert 新增品項失敗
        Assert.False(result);
        _dbServiceMock.Verify(x => x.SaveChangeItem(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public void RemoveProduct_Success()
    {
        var product = new Product("item1", "cola", "resauramt1");
        _dbServiceMock.Setup(x => x.RemoveChangeItem(product.ItemId)).Returns(true);

        // Arrange
        ChangeItem ChangeItem = new(_dbServiceMock.Object);

        // Act
        var result = ChangeItem.DeleteProduct(product.ItemId);

        // Assert 刪除品項成功
        Assert.True(result);
        _dbServiceMock.Verify(x => x.RemoveChangeItem(It.IsAny<String>()), Times.Once);
    }

    [Fact]
    public void RemoveProduct_Failed()
    {
        _dbServiceMock.Setup(x => x.RemoveChangeItem(null)).Returns(false);

        // Arrange
        ChangeItem ChangeItem = new(_dbServiceMock.Object);

        // Act
        var result = ChangeItem.DeleteProduct(null);

        // Assert 刪除品項失敗
        Assert.False(result);
        _dbServiceMock.Verify(x => x.RemoveChangeItem(null), Times.Never);
    }

}
