namespace TuEnvio
{
    public static class TuEnvio
    {
        public static IShoppingCart<TProduct> GetShoppingCart<TProduct>(int capacity)
            where TProduct : IProduct
        {
            IShoppingCart<TProduct> MyShoppingCart = new ShoppingCart<TProduct> (capacity);

            return MyShoppingCart;
        }
    }


    //* My ShoppingCart Implementation
    public class ShoppingCart<TProduct> : IShoppingCart<TProduct>  where TProduct : IProduct {
        Dictionary<TProduct, int> Cart;
        List< IPromotion<TProduct> > Promotions;
        int Capacity;

        public ShoppingCart( int capacity ) {
            this.Cart = new Dictionary<TProduct, int>();
            this.Promotions = new List<IPromotion<TProduct>>();
            this.Capacity = capacity;
        }

        public double Cost {
            get{ 
                double cost = 0;
                foreach( var aux in Cart ) {
                    TProduct p =  aux.Key;
                    int count = aux.Value;
                    
                    double basePrice = (double)count * (double)p.Price;
                    double discount = 0; // Apply discount
                    foreach( IPromotion<TProduct> prom in this.Promotions ) {
                        discount += prom.Discount( p, count );
                    }
                    cost += ( basePrice - (basePrice * discount ));
                }
                
                return cost;
            }
        }
        public int Total {
            get { 
                return this.Cart.Count;
            }
        }
        public void Add(TProduct product, int count) {

            foreach( var aux in Cart ) {
                TProduct p = aux.Key;
                if( p.Equals( product ) ) {
                    this.Cart[ p ] += count;
                    return;
                }
            }

            this.Cart[ product ] = count;
        }
        public int Count(TProduct product)
        {
            foreach( var aux in this.Cart ) {
                if( !aux.Key.Equals( product ) ) continue;
                
                return aux.Value;
            }   

            return 0;
        }
        public int Count(IFilter<TProduct> filter)
        {
            int count = 0;
            foreach( var aux in Cart ) {
                if( filter.Apply( aux.Key ) )
                    count += aux.Value;
            }

            return count;
        }
        public bool Remove(TProduct product)
        {
            bool result = false;
            Dictionary<TProduct, int> extra = new Dictionary<TProduct, int>();
            foreach( var aux in this.Cart ) {
                if( !aux.Key.Equals( product ) ) {
                    extra[ aux.Key ] = aux.Value;
                } else {
                    result = true;
                }
            }

            this.Cart = extra;
            return result;
        }
        public void AddPromotion(IPromotion<TProduct> promotion)
        {
            this.Promotions.Add( promotion );
        }
    }

}