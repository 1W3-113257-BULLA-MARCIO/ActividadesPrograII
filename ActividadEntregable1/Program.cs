using ActividadEntregable1.Domain;
using ActividadEntregable1.Services;

ArticuloManager manager = new ArticuloManager();

//Create new product:
var oArticulo = new Articulo()
{
    Nombre = "PRODUCTO DE PRUEBA",
    PrecioUnitario = 4000
    
};

if (manager.SaveProduct(oArticulo))
    Console.WriteLine("PRODUCTO CREADO EXISTOSAMENTE!");

//List all product of store:
List<Articulo> lst = manager.GetProducts();
if (lst.Count == 0)
{
    Console.WriteLine("Sin productos en la base de datos");

}
else
{
    foreach (var oProducto in lst)
    {
        Console.WriteLine(oProducto);
    }
}

//Delete product cod = 1:
if (manager.DeleteProduct(1))
    Console.WriteLine("PRODUCTO ACTUALIZADO CON DATOS DE BAJA!");

FacturaManager facturaManager = new FacturaManager();
var bugdets = facturaManager.GetBudgets();
var budget01 = facturaManager.GetBudgetsById(1);

