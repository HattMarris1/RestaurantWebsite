public class CartItem
{
    //create empty object or add values on creation
    public CartItem() { }
    public CartItem(MenuItem product, int quantity)
    {
        this.menuItem = product;
        this.Quantity = quantity;
    }

    //public properties
    public MenuItem menuItem { get; set; }
    public int Quantity { get; set; }

    //add to quantity
    public void AddQuantity(int quantity)
    {
        this.Quantity += quantity;
    }

    //display item's property values
    public string Display()
    {        
        string display = string.Format("{0} ({1} at {2} each)",
            menuItem.Name, Quantity.ToString(),
            menuItem.UnitPrice.ToString("c"));
        return display;
    }
}