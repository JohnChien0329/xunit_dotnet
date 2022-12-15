using DynamoItemManager.Models;

namespace TestingBasics.functions;

// Defining a product
public record Product(string ItemId, string ItemName, string ResautantId);

// Db Service
public interface IDbService
{
    bool SaveChangeItem(Product prod);
    bool RemoveChangeItem(String? ItemId);
}

// Shopping Cart functionality
public class ChangeItem
{
    private IDbService _dbService;

    public ChangeItem(IDbService dbService)
    {
        _dbService = dbService;
    }

    public bool AddProduct(Product? product)
    {
        if (product == null)
            return false;

        if (product.ItemId == null)
            return false;

        _dbService.SaveChangeItem(product);
        return true;
    }

    public bool DeleteProduct(String? ItemId)
    {
        if (ItemId == null)
            return false;

        if (ItemId == "0")
            return false;

        _dbService.RemoveChangeItem(ItemId);
        return true;
    }
}
