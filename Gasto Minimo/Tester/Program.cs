using Weboo.Examen;

public class Program
{
    public static void Main()
    {
        // Adicione aquí los tests que considere necesarios
        
        // Ejemplo1
        Test(
            // Pesos
            new[] { 10, 3, 3, 3, 3 },
            // Combustible
            new[,]
            {
                { 0, 2, 2, 3, 3 },
                { 2, 0, 1, 4, 4 },
                { 2, 1, 0, 4, 4 },
                { 3, 4, 4, 0, 2 },
                { 3, 4, 4, 2, 0 },
            },
            // Resultado esperado
            13
        );
        
        // Ejemplo2
        Test(
            // Pesos
            new[] { 20, 15, 10, 13, 17 },
            // Combustible
            new[,]
            {
                { 0, 4, 3, 1, 2 },
                { 4, 0, 3, 3, 4 },
                { 3, 3, 0, 2, 5 },
                { 1, 3, 2, 0, 3 },
                { 2, 4, 5, 3, 0 },
            },
            // Resultado esperado
            20
        );
    }

    public static void Test(int[] pesos, int[,] combustible, int esperado)
    {
        try
        {
            var resultado = TuEnvio.CombustibleDiario(pesos, combustible);

            if (resultado != esperado)
            {
                throw new Exception($"Se esperaba {esperado} pero se obtuvo {resultado}");
            }

            Console.WriteLine($"🟢 Resultado correcto: {resultado}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"🔴 {e}");
        }
    }
}
