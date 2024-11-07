namespace CatFact
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICatFactRepository catFactRepository = new CatFactRepository();

            CatFact catFact = catFactRepository.GetRandomCatFact();
            Console.WriteLine($"Kattefakta: {catFact.Fact} (Længde: {catFact.Length})");
        }
    }
}
