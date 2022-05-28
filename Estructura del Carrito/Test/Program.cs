using TuEnvio;

IShoppingCart<SimpleProduct> cart = TuEnvio.TuEnvio.GetShoppingCart<SimpleProduct>(3);

var pollo = new SimpleProduct(100, "Pollo (1kg)");
var pescado = new SimpleProduct(150, "Pescado (1kg)");
var vino = new SimpleProduct(250, "Botella de Vino");

cart.Add(pollo, 5);     // Añadiendo 5 kilos de pollo
cart.Add(pescado, 3);   // Añadiendo 3 kilos de pescado
cart.Add(vino, 2);       // Añadiendo dos botellas de vino
cart.Add(pollo, 2);     // Añadiendo 2 kilos de pollo adicionales


int polloTotal = cart.Count(pollo);      // Devolvería 7 (5 + 2)
int pescadoTotal = cart.Count(pescado);  // Devolvería 3
int vinoTotal = cart.Count(vino);     // Devolvería 2

// System.Console.WriteLine(polloTotal);
// System.Console.WriteLine(pescadoTotal);
// System.Console.WriteLine(vinoTotal);

System.Console.WriteLine( cart.Total ); // 3
cart.Remove( pollo );
System.Console.WriteLine( cart.Total ); // 2
cart.Remove( pollo );

IFilter<SimpleProduct> filtroPrice = new MyFilterByPrice<SimpleProduct> (50);
    // (no importa como se implementa este filtro)

bool filterPollo = filtroPrice.Apply(pollo);   // Devolvería `false`
bool filterPescado = filtroPrice.Apply(pescado); // Devolvería `true`
bool filterVino = filtroPrice.Apply(vino);    // Devolvería `true`

// System.Console.WriteLine(filterPollo);
// System.Console.WriteLine(filterPescado);
// System.Console.WriteLine(filterVino);

int totalFiltro = cart.Count(filtroPrice);
// Devolvería 5
// System.Console.WriteLine(totalFiltro);


IPromotion<SimpleProduct> promocionCubaneo = new MyPromotion<SimpleProduct> ( filtroPrice, 3 );
    // (no importa como se implementa esta promoción)

double promoPollo = promocionCubaneo.Discount(pollo, 7);    // Devolvería 0.2
double promoPescado = promocionCubaneo.Discount(pescado, 3);  // Devolvería 0
double promoVino = promocionCubaneo.Discount(vino, 2);     // Devolvería 0

System.Console.WriteLine(promoPollo);
System.Console.WriteLine(promoPescado);
System.Console.WriteLine(promoVino);

cart.AddPromotion(promocionCubaneo);
double cost = cart.Cost;   // Devolvería 1510
System.Console.WriteLine(cost);




// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
public class SimpleProduct : IProduct {

    int price { get; }
    string description { get; }

    public SimpleProduct( int price, string product  ) {
        this.price = price;
        this.description = product;
    }

    public int Price {
        get{ return this.price; }
    }

    public string Description {
        get{ return this.description; }
    }

    public bool Equals( SimpleProduct product ) {
        return ( this.Description == product.Description );
    }

}


public class MyFilterByPrice<T> : IFilter<T> where T : IProduct
{
    double price;
    public MyFilterByPrice( double price ) {
        this.price = price;
    }

    public bool Apply(T item)
    {
        return ( item.Price > price );
    }
}

public class MyPromotion<TProduct> : IPromotion<TProduct> where TProduct : IProduct {
    int cantidad;
    IFilter<TProduct> filter;
    public MyPromotion( IFilter<TProduct> filter, int cantidad ) {
        this.cantidad = cantidad;
        this.filter = filter;
    }

    public double Discount(TProduct product, int count)
    {
        if( this.filter.Apply(product) && count > cantidad ) {
            return 1;
        }
        return 0;
    }
}