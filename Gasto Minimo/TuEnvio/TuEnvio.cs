namespace Weboo.Examen
{
    public class TuEnvio
    {
        public static int CombustibleDiario(int[] pesos, int[,] combustible)
        {
        
            int cantDestinos = combustible.GetLength(0);
            List<int> rute = new List<int>();
            bool[] visited = new bool[cantDestinos];
            rute.Add(0);

            int sol = Solve(0, pesos, combustible, visited, rute, pesos[0], 0);

            return sol;
        }


        static int Solve( int cant, int[] pesos, int[,] combustible, bool[] visited, List<int> rute, int PM, int gasto ) {

            if( cant == combustible.GetLength(0) - 1 ) {
                if( rute.Last() != 0 )
                    gasto += combustible[ rute.Last(), 0 ];
                return gasto;
            }
            
            int result = int.MaxValue;
            int lastNode = rute.Last(); 

            if( lastNode != 0 ) {
                rute.Add(0);
                result = Math.Min(result, Solve( cant, pesos, combustible, visited, rute, pesos[0], gasto + combustible[lastNode, 0] ) );
                rute.Remove( rute.Count - 1 );
            }

            for(int i = 1; i < combustible.GetLength(0); i ++) {
                if( visited[i] || PM - pesos[i] < 0 ) continue;

                visited[i] = true;
                rute.Add(i);
                result = Math.Min(result, Solve( cant + 1, pesos, combustible, visited, rute, PM - pesos[i], gasto + combustible[ lastNode, i ] ) );

                visited[i] = false;
                rute.RemoveAt( rute.Count - 1 );
            }

            return result;
        }

    }
}
